using Microsoft.Extensions.Logging;
using Tournament.Common.Objects;
using Tournament.Common.Objects.QueryData;
using Tournament.Interfaces;   


namespace Tournament.Logic
{
    public class TournamentBusinessLogic : ITournamentBusinessLogic
    {
        private readonly ITournamentRepository _tournamentRepository;
        private readonly ILogger<TournamentBusinessLogic> _logger;

        public TournamentBusinessLogic(ILogger<TournamentBusinessLogic> logger, ITournamentRepository tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
            _logger = logger;
        }


        public async Task<TournamentStatistics> fetchSuccessPerQuestion(Guid tournamentId)
        {
            _logger?.LogInformation($"fetchSuccessPerQuestion tournament: {tournamentId}");
            var result = await _tournamentRepository.getTournamentResults(tournamentId);
            return new TournamentStatistics();
        }

        public async Task<List<TournamentStatistics>> fetchTournamentStatistics(Guid tournamentId)
        {
            _logger?.LogInformation($"fetchTournamentStatistics tournament: {tournamentId}");
            var result = await _tournamentRepository.getTournamentResults(tournamentId);
            return null;

        }

        public async Task<List<UserScore>> fetchUsersScores(Guid tournamentId)
        {
            _logger?.LogInformation($"fetchUsersScores tournament: {tournamentId}");
            var result = await _tournamentRepository.getTournamentResults(tournamentId);
            return null;
        }

        public async Task<TournamentData> getTournamentResults(Guid tournamentId)
        {
            _logger?.LogInformation($"getTournamentResults tournament: {tournamentId}");
            var result = _tournamentRepository.getTournamentResults(tournamentId);
            return null;
        }

        public async Task saveTournamentResults(TournamentData tournament)
        {
            _logger?.LogInformation($"saveTournamentResults tournament: {tournament.tournamentId}");
            await _tournamentRepository.saveTournamentResults(tournament);
        }
    }
}