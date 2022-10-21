using Bank.Data;
using Bank.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace Bank.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DataContext _context;

        public AccountController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankAccount>>> GetById(int id)
        {
            var userid = await _context.BankAccounts.Include(x => x.AccountHistories).FirstOrDefaultAsync(x => x.Id == id);
            if (userid == null)
            {
                return NotFound();
            }

            return Ok(userid);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankAccount>>> GetAll()
        {
            return Ok(await _context.BankAccounts.Include(x=> x.AccountHistories).ToListAsync());
        }
        [HttpGet]
        public async Task<ActionResult<BankAccount>> GetSavedAmount(int id)
        {
            var user = await _context.BankAccounts.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.Saved);
        }

        [HttpGet]
        public async Task<ActionResult<BankAccount>> GetBalance(int id)
        {
            var user = await _context.BankAccounts.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.Balance);
        }


        [HttpPost]
        public async Task<ActionResult> NewAccount (Account account)
        {
            var acc = await _context.BankAccounts.FirstOrDefaultAsync(x => x.Id == account.Id);
            if(acc != null)
            {
                return BadRequest("account already exists");
            }
            var bacc = new BankAccount()
            {
                Id = account.Id,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Salary = account.Salary,
                Saved = account.Saved,
                Balance = account.Balance,

            };
            _context.BankAccounts.Add(bacc);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Salary(int id)
        {
            var usersalary = await _context.BankAccounts.FirstOrDefaultAsync(x => x.Id == id);

            if (usersalary == null)
            {
                return NotFound();
            }

            else
            {
                var acchistory = new AccountHistory()
                {
                    Amount = usersalary.Salary,
                    Date = DateTime.UtcNow,
                    Description = "Salary",
                    HType = HType.Salary,
                    BankAccountId = usersalary.Id

                };
                _context.AccountHistories.Add(acchistory);

                usersalary.Balance += usersalary.Salary;
                await _context.SaveChangesAsync();
                return Ok();
            }
        }
        [HttpPost]
        public async Task<ActionResult> Savings(int id, decimal amount)
        {
            var usersaving = await _context.BankAccounts.FirstOrDefaultAsync(x => x.Id == id);
            if (usersaving == null)
            {
                return NotFound();
            }

            else
            {

                if (amount < 0 || amount >= usersaving.Balance / 2)
                {
                    return BadRequest("Amount must be > 0 and amount cant be greater than half of your balance");
                }


                var acchistory = new AccountHistory()
                {
                    Amount = amount,
                    Date = DateTime.UtcNow,
                    Description = "Saving",
                    HType = HType.Saving,
                    BankAccountId = usersaving.Id
                };
                _context.AccountHistories.Add(acchistory);

                usersaving.Balance -= amount;
                usersaving.Saved += amount;

                await _context.SaveChangesAsync();

                return Ok();
            }


        }

        [HttpPost]

        public async Task<ActionResult> SavingsToBalance(int id, decimal amount)
        {
            var savingstobalance = await _context.BankAccounts.FirstOrDefaultAsync(x => x.Id == id);
            if (savingstobalance == null)
            {
                return BadRequest();
            }

            else
            {
                if (amount < 0 || amount > savingstobalance.Saved)
                {
                    return BadRequest();
                }

                var acchistory = new AccountHistory()
                {
                    Amount = amount,
                    Date = DateTime.UtcNow,
                    Description = "From Saved To Balance",
                    HType = HType.Saving,
                    BankAccountId = savingstobalance.Id
                };
                _context.AccountHistories.Add(acchistory);

                savingstobalance.Saved -= amount;
                savingstobalance.Balance += amount;

                await _context.SaveChangesAsync();
                return Ok();
            }
        }

        [HttpPost]

        public async Task<ActionResult> Buy(int id, string desc, decimal amount)
        {
            var userbuy = await _context.BankAccounts.FirstOrDefaultAsync(x => x.Id == id);
            if (userbuy == null)
            {
                return NotFound();
            }

            else
            {
                if (amount < 0 || amount > userbuy.Balance)
                {
                    return BadRequest("You dont have enough money for that");
                }

                var moneyhis = new AccountHistory()
                {
                    Amount = amount,
                    Date = DateTime.UtcNow,
                    Description = desc,
                    HType = HType.Expense,
                    BankAccountId = userbuy.Id
                };
                _context.AccountHistories.Add(moneyhis);

                userbuy.Balance -= amount;
                
                await _context.SaveChangesAsync();
                return Ok();
            }
        }
    }
}
