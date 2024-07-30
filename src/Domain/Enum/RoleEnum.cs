using System.Text.Json.Serialization;

namespace Wallet.Registration.Domain.Enum
{
    [JsonConverter(typeof(string))]
    public enum RoleEnum
    {
        None = 0,
        Admin,
        Free,
        Premium,
        Removed
    }
}
