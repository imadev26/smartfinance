using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartFinanceAPI.Models;
using SmartFinanceAPI.Data;

namespace SmartFinanceAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GoalController : ControllerBase
{
    private readonly SmartFinanceContext _context;

    public GoalController(SmartFinanceContext context)
    {
        _context = context;
    }

    // GET: api/Goal
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Goal>>> GetAll()
    {
        return await _context.Goals.ToListAsync();
    }

    // GET: api/Goal/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Goal>> GetById(int id)
    {
        var goal = await _context.Goals.FindAsync(id);
        if (goal == null) return NotFound();
        return goal;
    }

    // POST: api/Goal
    [HttpPost]
    public async Task<ActionResult<Goal>> Add(Goal goal)
    {
        _context.Goals.Add(goal);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = goal.Id }, goal);
    }

    // PUT: api/Goal/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Goal goal)
    {
        if (id != goal.Id) return BadRequest();

        _context.Entry(goal).State = EntityState.Modified;
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

    // DELETE: api/Goal/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        var goal = await _context.Goals.FindAsync(id);
        if (goal == null) return NotFound();

        _context.Goals.Remove(goal);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool EntityExists(int id)
    {
        return _context.Goals.Any(e => e.Id == id);
    }
} 