using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartFinanceAPI.Models;
using SmartFinanceAPI.Data;

namespace SmartFinanceAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly SmartFinanceContext _context;

    public TransactionController(SmartFinanceContext context)
    {
        _context = context;
    }

    // GET: api/Transaction
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetAll()
    {
        return await _context.Transactions.ToListAsync();
    }

    // GET: api/Transaction/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Transaction>> GetById(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction == null) return NotFound();
        return transaction;
    }

    // POST: api/Transaction
    [HttpPost]
    public async Task<ActionResult<Transaction>> Add(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = transaction.Id }, transaction);
    }

    // PUT: api/Transaction/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Transaction transaction)
    {
        if (id != transaction.Id) return BadRequest();

        _context.Entry(transaction).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EntityExists(id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    // DELETE: api/Transaction/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction == null) return NotFound();

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool EntityExists(int id)
    {
        return _context.Transactions.Any(e => e.Id == id);
    }
} 