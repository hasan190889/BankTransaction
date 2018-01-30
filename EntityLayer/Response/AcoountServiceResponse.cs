namespace EntityLayer.Response
{
    public class AcoountServiceResponse
    {
        public long AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public bool Successful { get; set; }
        public string Currency { get; set; }
        public string Message { get; set; }
    }
}
