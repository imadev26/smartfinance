namespace SmartFinanceAPI.Models;

public class Notification
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Message { get; set; }
    public DateTime Date { get; set; }
    public bool IsRead { get; set; }

    // Navigation property
    public User User { get; set; }
} 