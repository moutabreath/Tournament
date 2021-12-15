using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Tournament.Common.Objects;

namespace Tournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase 
    { 

        [HttpPost]
        public async Task saveTournamentResults([FromBody] Common.Objects.TournamentData tournament)
        {

        }

        [HttpPost]
        public async Task<JsonResult> getTournamentResults([FromBody] Guid tournamentId)
        {

        }

        [HttpPost]
        public async Task<JsonResult> fetchSuccessPerQuestion([FromBody] Guid tournamentId)
        {

        }

        [HttpPost]
        public async Task<List<UserScoreResponse>> fetchUsersScores([FromBody] Guid tournamentId)
        {

        }

        [HttpPost]
        public async Task<List<TournamentStatisticsResponse>> fetchTournamentStatistics([FromBody] Guid tournamentId)
        {

        }
    }
}
