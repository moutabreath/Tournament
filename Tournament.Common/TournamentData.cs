namespace Tournament.Common.Objects
{
    public class TournamentData
    {
        public Guid tournamentId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDateTime { get; set; }
        public List<UserResult> results { get; set; }
    }

    public class UserResult
    {
        public Guid userId { get; set; }
        public List<int> correctQuestions { get; set; }
        public List<int> incorrectQuestions { get; set; }
    }

}