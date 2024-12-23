namespace SmartFinanceAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class Category
{
    [Column("id")]
    public int Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; }

    public ICollection<Transaction> Transactions { get; set; }
} 