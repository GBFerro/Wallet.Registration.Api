using static Wallet.Registration.CrossCutting.Configuration.Models.v1.AppSettings;

namespace Wallet.Registration.CrossCutting.Configuration
{
    public partial class AppSettings
    {
        public MongoDbSettings MongoDbSettings { get; set; }

    }
}
