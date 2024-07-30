namespace Wallet.Registration.Domain.Command.v1.SignUp
{
    public record SignUpCommandResponse
    {
        public string Username { get; set; }
        public string GovId { get; set; }
        public string Email { get; set; }
    }
}
