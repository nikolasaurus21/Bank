using Bank.Models;

namespace Bank.Data
{
    public class History
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public HType HType { get; set; }
        public int BalanceAccountId { get; set; }
    }
}
