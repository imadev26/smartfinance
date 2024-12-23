using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartFinanceAPI.Data;

namespace SmartFinanceAPI.Controllers.Base;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController<T> : ControllerBase where T : class
{
    protected readonly SmartFinanceContext _context;
    protected readonly DbSet<T> _dbSet;

    protected BaseApiController(SmartFinanceContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<T>>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<T>> GetById(int id)
    {
        var entity = await FindByIdAsync(id);
        if (entity == null) return NotFound();
        return entity;
    }

    [HttpPost]
    public virtual async Task<ActionResult<T>> Add(T entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = GetId(entity) }, entity);
    }

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Update(int id, T entity)
    {
        if (id != GetId(entity)) return BadRequest();

        _context.Entry(entity).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await EntityExists(id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Remove(int id)
    {
        var entity = await FindByIdAsync(id);
        if (entity == null) return NotFound();

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    protected virtual async Task<bool> EntityExists(int id)
    {
        return await _dbSet.AnyAsync(e => GetId(e) == id);
    }

    protected abstract int GetId(T entity);
    protected abstract Task<T> FindByIdAsync(int id);
} 