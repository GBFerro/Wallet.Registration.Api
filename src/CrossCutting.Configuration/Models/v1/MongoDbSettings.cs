namespace Wallet.Registration.CrossCutting.Configuration.Models.v1;


public partial class AppSettings
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ConnectionId { get; set; }
    }
}
