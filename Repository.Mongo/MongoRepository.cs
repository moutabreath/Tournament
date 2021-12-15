using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Tournament.Common.Objects;
using Tournament.Interfaces;

namespace Tournamnent.Repository.Mongo
{
    public class MongoRepository : ITournamentRepository
    {

        protected readonly ILogger<MongoRepository> _logger;

        protected readonly string _documentName;
        protected MongoClient Client { get; set; }
        protected IMongoDatabase Database { get; set; }
        protected IMongoCollection<TournamentData> MongoCollection { get; set; }

        public MongoRepository(ILogger<MongoRepository> logger, IOptions<TournamentConfig> options, string documentName)
        {
            _logger = logger;

            _documentName = documentName;
            TournamentConfig configuration = options.Value;
            Client = new MongoClient(configuration.MongoConnectionString);
            Database = Client.GetDatabase(configuration.MongoCatanGameDbName);
            MongoCollection = Database.GetCollection<TournamentData>(_documentName);
            InitializeClassMap();
        }

        private void InitializeClassMap()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(TournamentData)))
            {
                BsonClassMap.RegisterClassMap<TournamentData>(bsonClassMap =>
                {
                    bsonClassMap.AutoMap();
                    bsonClassMap.SetIdMember(bsonClassMap.GetMemberMap(tournament => tournament.tournamentId));
                    bsonClassMap.SetIgnoreExtraElements(true);
                });
            }
        }


        public async Task<TournamentData> getTournamentResults(Guid tournamentId)
        {
            _logger?.LogInformation($"getTournamentResults tournament: {tournamentId}");
            try
            {
                var result = await MongoCollection.FindAsync(tournament => tournament.tournamentId == tournamentId);
                return result.FirstOrDefault();
            }
            catch(Exception ex)
            {
                _logger?.LogError($"getTournamentResults Error when getting tournament {tournamentId}", ex);
                return null;
            }            
        }

        public async Task saveTournamentResults(TournamentData tournament)
        {
            try
            {
                if (tournament.tournamentId == Guid.Empty) tournament.tournamentId = Guid.NewGuid();
                _logger?.LogInformation($"saveTournamentResults tournament: {tournament.tournamentId}");
                IMongoCollection<TournamentData> gameCollection = Database.GetCollection<TournamentData>(_documentName);
                FilterDefinition<TournamentData> filter = Builders<TournamentData>.Filter.Where(tour => tour.tournamentId == tournament.tournamentId);
                await MongoCollection.ReplaceOneAsync(filter, tournament, new ReplaceOptions { IsUpsert = true });
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdateEntity Error when updating entity {tournament.tournamentId}", ex);
            }
        }
    }
}