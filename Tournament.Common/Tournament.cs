namespace Tournament.Common.Objects
{
    public class Tournament
    {
        public Guid tournamentId;
        public DateTime startDate;
        public DateTime endDateTime;
        public List<UserResult> results = new();

    }

    public class UserResult
    {
        public Guid userId;
        public List<int> correctQuestions = new ();
        public List<int> incorrectQuestions = new ();
    }
}