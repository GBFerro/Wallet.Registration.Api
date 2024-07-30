using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Wallet.Registration.Domain.Entities.v1.MongoDB.SignUp.Models;
using Wallet.Registration.Domain.Enum;

namespace Wallet.Registration.Domain.Entities.v1.MongoDB.SignUp
{
    public record SignUpMongoEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("user")]
        public User User { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("role")]
        public RoleEnum Role { get; set; }
    }
}
