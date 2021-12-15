using Tournament.Common.Objects;
using Tournament.Common.Objects.QueryData;

namespace Tournament.Interfaces
{
    public interface ITournamentBusinessLogic
    {
        public Task saveTournamentResults(TournamentData tournament);

        public Task<TournamentData> getTournamentResults(Guid tournamentId);

        public Task<IEnumerable<Tuple<int, double>>> fetchSuccessPerQuestion(Guid tournamentId);

        public Task<IList<Tuple<Guid, char>>> fetchUsersScores(Guid tournamentId);

        public Task<TournamentStatistics> fetchTournamentStatistics(Guid tournamentId);
        
    }
}
