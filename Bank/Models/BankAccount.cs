namespace Bank.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public decimal Saved { get; set; }
        public decimal Salary { get; set; }

        public ICollection<AccountHistory> AccountHistories { get; set; } = new List<AccountHistory>();
    }
}
