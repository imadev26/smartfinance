namespace SmartFinanceAPI.Models;

public class Goal
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public double TargetAmount { get; set; }
    public double CurrentAmount { get; set; }
    public DateTime Deadline { get; set; }

    // Navigation property
    public User User { get; set; }
} 