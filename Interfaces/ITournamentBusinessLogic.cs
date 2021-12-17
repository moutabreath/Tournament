using Tournament.Common.Objects;
using Tournament.Common.Objects.QueryData;

namespace Tournament.Interfaces
{
    public interface ITournamentBusinessLogic
    {
        public Task saveTournamentResults(TournamentData tournament);

        public Task<TournamentData> getTournamentResults(int tournamentId);

        public Task<IEnumerable<Tuple<int, double>>> fetchSuccessPerQuestion(int tournamentId);

        public Task<IList<Tuple<int, char>>> fetchUsersScores(int tournamentId);

        public Task<TournamentStatistics> fetchTournamentStatistics(int tournamentId);
        
    }
}
