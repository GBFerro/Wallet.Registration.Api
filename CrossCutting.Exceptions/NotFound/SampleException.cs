namespace Wallet.Register.CrossCutting.Exceptions.NotFound
{
    public class SampleException : Exception
    {
        public SampleException() : base() { }
        public SampleException(string message) : base(message) { }
        public SampleException(string message, Exception innerException) : base(message, innerException) { }

        public override string Message => $"This is an exception sample.";

    }
}
