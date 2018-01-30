namespace EntityLayer.Request
{
    public class AccountDepositOrWithdraw
    {
        public long AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
