namespace Wallet.Registration.Domain.Entities.v1.MongoDB.SignUp.Models
{
    public record User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string BirthDate { get; set; }
        public string Phone { get; set; }
        public string GovId { get; set; }
    }
}
