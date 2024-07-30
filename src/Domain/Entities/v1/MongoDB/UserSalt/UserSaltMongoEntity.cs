using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Wallet.Registration.Domain.Entities.v1.MongoDB.UserSalt
{
    public class UserSaltMongoEntity
    {
        [BsonElement("id")]
        public ObjectId Id { get; set; }

        [BsonElement("govId")]
        public string GovId { get; set; }

        [BsonElement("salt")]
        public string Salt { get; set; }
    }
}
