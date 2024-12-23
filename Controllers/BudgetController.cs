using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartFinanceAPI.Models;
using SmartFinanceAPI.Data;

namespace SmartFinanceAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BudgetController : ControllerBase
{
    private readonly SmartFinanceContext _context;

    public BudgetController(SmartFinanceContext context)
    {
        _context = context;
    }

    // GET: api/Budget
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Budget>>> GetAll()
    {
        return await _context.Budgets.ToListAsync();
    }

    // GET: api/Budget/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Budget>> GetById(int id)
    {
        var budget = await _context.Budgets.FindAsync(id);
        if (budget == null) return NotFound();
        return budget;
    }

    // POST: api/Budget
    [HttpPost]
    public async Task<ActionResult<Budget>> Add(Budget budget)
    {
        _context.Budgets.Add(budget);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = budget.Id }, budget);
    }

    // PUT: api/Budget/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Budget budget)
    {
        if (id != budget.Id) return BadRequest();

        _context.Entry(budget).State = EntityState.Modified;
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

    // DELETE: api/Budget/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        var budget = await _context.Budgets.FindAsync(id);
        if (budget == null) return NotFound();

        _context.Budgets.Remove(budget);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool EntityExists(int id)
    {
        return _context.Budgets.Any(e => e.Id == id);
    }
} 