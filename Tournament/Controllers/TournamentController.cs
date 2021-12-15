using Microsoft.AspNetCore.Mvc;
using Tournament.Common.Objects;
using Tournament.Interfaces;

namespace Tournament.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase 
    {
        private readonly ITournamentBusinessLogic _tournamentBusinessLogic;
        private readonly ILogger<TournamentController> _logger;

        public TournamentController(ILogger<TournamentController> logger, ITournamentBusinessLogic tournamentBusinessLogic)
        {
            _tournamentBusinessLogic = tournamentBusinessLogic;
            _logger = logger;
        }

        [HttpPost]
        public async Task saveTournamentResults([FromBody] TournamentData tournament)
        {
            _logger?.LogInformation($"saveTournamentResults tournament: {tournament.tournamentId}");
            await _tournamentBusinessLogic.saveTournamentResults(tournament);
        }

        [HttpPost]
        public async Task<JsonResult> getTournamentResults([FromBody] Guid tournamentId)
        {
            _logger?.LogInformation($"getTournamentResults tournament: {tournamentId}");
            var result = await _tournamentBusinessLogic.getTournamentResults(tournamentId);
            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<JsonResult> fetchSuccessPerQuestion([FromBody] Guid tournamentId)
        {
            _logger?.LogInformation($"fetchSuccessPerQuestion tournament: {tournamentId}");
            var result = await _tournamentBusinessLogic.fetchSuccessPerQuestion(tournamentId);
            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<JsonResult> fetchUsersScores([FromBody] Guid tournamentId)
        {
            _logger?.LogInformation($"fetchUsersScores tournament: {tournamentId}");
            var result = await _tournamentBusinessLogic.fetchUsersScores(tournamentId);
            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<JsonResult> fetchTournamentStatistics([FromBody] Guid tournamentId)
        {
            _logger?.LogInformation($"fetchTournamentStatistics tournament: {tournamentId}");
            var result = await _tournamentBusinessLogic.fetchTournamentStatistics(tournamentId);
            return new JsonResult(result);
        }
    }
}
