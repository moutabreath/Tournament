﻿using Microsoft.Extensions.Configuration;
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

        protected MongoClient Client { get; set; }
        protected IMongoDatabase Database { get; set; }
        protected IMongoCollection<TournamentData> MongoCollection { get; set; }

        public MongoRepository(ILogger<MongoRepository> logger, IOptions<TournamentConfig> options)
        {          
        
            _logger = logger;

            TournamentConfig configuration = options.Value;
            Client = new MongoClient(configuration.MongoConnectionString);
            Database = Client.GetDatabase(configuration.MongoDbName);
            MongoCollection = Database.GetCollection<TournamentData>(configuration.MongoDocumentName);
            InitializeClassMap();
        }

        private void InitializeClassMap()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(TournamentData)))
            {
                BsonClassMap.RegisterClassMap<TournamentData>(bsonClassMap =>
                {
                    bsonClassMap.AutoMap();
                    bsonClassMap.SetIgnoreExtraElements(true);
                    //bsonClassMap.MapProperty(tour => tour.Id).SetElementName("_id");
                    //bsonClassMap.SetIdMember(bsonClassMap.GetMemberMap(tournament => tournament.Id));
                });
            }
        }


        public async Task<TournamentData> getTournamentResults(int tournamentId)
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
                _logger?.LogInformation($"saveTournamentResults tournament: {tournament.tournamentId}");
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