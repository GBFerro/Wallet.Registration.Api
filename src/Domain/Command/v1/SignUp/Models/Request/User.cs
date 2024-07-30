using MongoDB.Bson.Serialization.Attributes;

namespace Wallet.Registration.Domain.Command.v1.SignUp.Models.Request
{
    public record User
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("birthDate")]
        public string BirthDate { get; set; }

        [BsonElement("phone")]
        public string Phone { get; set; }

        [BsonElement("govId")]
        public string GovId { get; set; }
    }
}
