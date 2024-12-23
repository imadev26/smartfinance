namespace SmartFinanceAPI.Models;

public class Transaction
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime Date { get; set; }
    public double Amount { get; set; }
    public string TransactionType { get; set; }
    public int CategoryId { get; set; }
    public string Note { get; set; }

    // Navigation properties
    public User User { get; set; }
    public Category Category { get; set; }
} 