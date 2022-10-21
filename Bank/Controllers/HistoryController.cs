using Bank.Data;
using Bank.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Bank.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly DataContext _context;

        public HistoryController(DataContext context)
        {
            _context=context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountHistory>>> GetSalaryDates(int id)
        {
            var user = await _context.AccountHistories.Where(x => x.BankAccountId == id && x.HType == HType.Salary )
                .Select(x=> x.Date).ToListAsync();
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountHistory>>> GetExpensesDates(int id)
        {
            var user = await _context.AccountHistories.Where(x => x.BankAccountId == id && x.HType == HType.Expense)
                .Select(x => x.Date).ToArrayAsync();
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountHistory>>> GetSavingsDates(int id)
        {
            var user = await _context.AccountHistories.Where(x => x.BankAccountId == id && x.HType == HType.Saving)
                .Select(x => x.Date).ToListAsync();
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
