using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartFinanceAPI.Controllers.Base;
using SmartFinanceAPI.Models;
using SmartFinanceAPI.Data;

namespace SmartFinanceAPI.Controllers;

public class CategoryController : BaseApiController<Category>
{
    public CategoryController(SmartFinanceContext context) : base(context) { }

    public override async Task<ActionResult<IEnumerable<Category>>> GetAll()
    {
        return await _dbSet
            .Include(c => c.Transactions)
            .ToListAsync();
    }

    protected override int GetId(Category entity) => entity.Id;

    protected override async Task<Category> FindByIdAsync(int id)
    {
        return await _dbSet
            .Include(c => c.Transactions)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
} 