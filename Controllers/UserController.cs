using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartFinanceAPI.Models;
using SmartFinanceAPI.Data;

namespace SmartFinanceAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly SmartFinanceContext _context;

    public UserController(SmartFinanceContext context)
    {
        _context = context;
    }

    // GET: api/User
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAll()
    {
        return await _context.Users.ToListAsync();
    }

    // GET: api/User/5
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetById(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();
        return user;
    }

    // POST: api/User
    [HttpPost]
    public async Task<ActionResult<User>> Add(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    // PUT: api/User/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, User user)
    {
        if (id != user.Id) return BadRequest();

        _context.Entry(user).State = EntityState.Modified;
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

    // DELETE: api/User/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool EntityExists(int id)
    {
        return _context.Users.Any(e => e.Id == id);
    }
} 