using Tournament.Common.Objects;
using Tournament.Common.Objects.QueryData;

namespace Tournament.Interfaces
{
    public interface ITournamentBusinessLogic
    {
        public Task saveTournamentResults(TournamentData tournament);

        public Task<TournamentData> getTournamentResults(Guid tournamentId);


        public Task<IEnumerable<Tuple<int, double>>> fetchSuccessPerQuestion(Guid tournamentId);


        public Task<List<UserScore>> fetchUsersScores(Guid tournamentId);


        public Task<List<TournamentStatistics>> fetchTournamentStatistics(Guid tournamentId);
    }
}
