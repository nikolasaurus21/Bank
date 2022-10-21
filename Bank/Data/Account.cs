namespace Bank.Data
{
    public class Account
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public decimal Balance { get; set; }
        public decimal Saved { get; set; }
    }
}
