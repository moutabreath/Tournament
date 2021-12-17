namespace Tournament.Common.Objects.QueryData
{
    public class TournamentStatistics
    {
        public IEnumerable<Tuple<int, double>> successPerQuestion { get; set; }
        public IList<Tuple<int, char>> userScores { get; set; }
    }
}
