using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Text.Json.Serialization;

namespace Bank.Models
{
    public class AccountHistory
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public HType HType { get; set; }
        [JsonIgnore]
        public BankAccount? BankAccount { get; set; }
        public int BankAccountId { get; set; }
    }
}
