namespace Tournament.Common.Objects
{
    public class TournamentConfig
    {
        public string MongoConnectionString { get; set; }
        public string MongoCatanGameDbName { get; set; }
        public string MongoCatanActivePlayerDbName { get; set; }
    }
}
