using Tournament.Common.Objects;

namespace Tournament.Interfaces
{
    public interface ITournamentRepository
    {

        public Task saveTournamentResults(TournamentData tournament);

        public Task<TournamentData> getTournamentResults(Guid tournamentId);
    }
}