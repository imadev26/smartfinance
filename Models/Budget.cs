namespace SmartFinanceAPI.Models;

public class Budget
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public double Amount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    // Navigation property
    public User User { get; set; }
} 