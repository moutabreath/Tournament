namespace Tournament.Common.Objects
{
    public class TournamentConfig
    {
        public const string Position = "MongoConfig";
        public string MongoConnectionString { get; set; }
        public string MongoDbName { get; set; }
        public string MongoDocumentName { get; set; }
    }
}
