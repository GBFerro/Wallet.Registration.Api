namespace Wallet.Registration.Domain.Entities.v1.MongoDB.SignUp
{
    public record User
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
