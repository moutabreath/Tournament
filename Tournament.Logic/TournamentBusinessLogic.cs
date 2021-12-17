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

        /**
         * 
         * Success Per Question: The percentage of users who answered it correctly
           Note: This will be a list of questions with a percentage attached to each one
         */
        public async Task<IEnumerable<Tuple<int, double>>> fetchSuccessPerQuestion(int tournamentId)
        {
            _logger?.LogInformation($"fetchSuccessPerQuestion tournament: {tournamentId}");
            var tournamentResult = await _tournamentRepository.getTournamentResults(tournamentId);
            IDictionary<int,int> questionsSuccesses = new Dictionary<int, int>();// Save the number of times a question has been answered correctly by a user
            IList<Tuple<int,double>> questionsPercentages = new List<Tuple<int,double>>();
            // First find all the questions in the tournament
            List<int> firstSucesses = tournamentResult.results.First().correctQuestions;
            foreach(var question in firstSucesses)
            {
                questionsSuccesses.Add(question, 1);
            }
            List<int> firstFailures = tournamentResult.results.First().incorrectQuestions;
            foreach(var question in firstFailures)
            {
                questionsSuccesses.Add(question, 0);
            }
            // Second, add 1 to each question,everytime it appears on a success of a user
            foreach(var userResult in tournamentResult.results.TakeLast(tournamentResult.results.Count - 1))// Take all items except first, which has already been calculated
            {
                List<int> userSuccesses = userResult.correctQuestions;
                foreach (var question in userSuccesses)
                {
                    questionsSuccesses.TryGetValue(question, out int currentSuccess);
                    currentSuccess++;
                    questionsSuccesses.Remove(question);
                    questionsSuccesses.Add(question, currentSuccess);
                }
            }
            foreach(var question in questionsSuccesses){
                questionsPercentages.Add(new Tuple<int, double> (question.Key, (double)((double)question.Value / (double)tournamentResult.results.Count)));
            }
            return questionsPercentages;
        }

        /**
          User Score: Calculate a user's score based on the following formula:
        A: If the user got more than 90% of their answers correct throughout the whole tournament
        B: If the user got 75%-90% of their answers correct throughout the whole tournament
        C: If the user got 60%-75% of their answers correct throughout the whole tournament
        F: If the user got less than 60% of their answers correct throughout the whole tournament
        Note: This will be a list of users with a score letter attached to each one.
          */
        public async Task<IList<Tuple<int, char>>> fetchUsersScores(int tournamentId)
        {
            _logger?.LogInformation($"fetchUsersScores tournament: {tournamentId}");
            var result = await _tournamentRepository.getTournamentResults(tournamentId);
            IList<Tuple<int, char>> usersScore = new List<Tuple<int, char>>();
            foreach(var userScore in result.results)
            {
                int numOfQuestions = userScore.incorrectQuestions.Count + userScore.correctQuestions.Count;
                double successRate = userScore.correctQuestions.Count / numOfQuestions;
                usersScore.Add(new Tuple<int, char> (userScore.userId, ConvertPercentageToScore(successRate)));
            }
            return usersScore;
        }

        private char ConvertPercentageToScore(double successRate)
        {
            if (successRate > 90)
            {
                return 'A';
            }
            if (75 < successRate && successRate <= 90)
            {
                return 'B';
            }
            if (60 < successRate && successRate <= 75)
            {
                return 'C';
            }

            return 'F';
        }

        /**
         * fetchTournamentStatistics: Return a JSON of both Success Per Question and User Score statistics.
        */
        public async Task<TournamentStatistics> fetchTournamentStatistics(int tournamentId)
        {
            _logger?.LogInformation($"fetchTournamentStatistics tournament: {tournamentId}");
            IEnumerable<Tuple<int, double>> currrentSuccessPerQuestion = await fetchSuccessPerQuestion(tournamentId);
            IList<Tuple<int, char>> currentUserScores = await fetchUsersScores(tournamentId);
            return new TournamentStatistics()
            {
                successPerQuestion = currrentSuccessPerQuestion,
                userScores = currentUserScores
            };
        }

        public async Task<TournamentData> getTournamentResults(int tournamentId)
        {
            _logger?.LogInformation($"getTournamentResults tournament: {tournamentId}");
            var result = await _tournamentRepository.getTournamentResults(tournamentId);
            return result;
        }

        public async Task saveTournamentResults(TournamentData tournament)
        {
            _logger?.LogInformation(message: $"saveTournamentResults tournament: {tournament.tournamentId}");
            await _tournamentRepository.saveTournamentResults(tournament);
        }
    }
}