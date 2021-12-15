namespace Tournament.Common.Objects
{
    public class TournamentData
    {
        public Guid tournamentId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDateTime { get; set; }
        public List<UserResult> results { get; set; } = new List<UserResult>();
    }

    public class UserResult
    {
        public Guid userId { get; set; }
        public List<int> correctQuestions { get; set; } = new List<int>();
        public List<int> incorrectQuestions { get; set; } = new List<int>();
    }

}