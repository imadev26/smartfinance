using System.ComponentModel.DataAnnotations;

namespace SmartFinanceAPI.Models;

public class UserSettings
{
    [Key]
    public int AccountId { get; set; }
    public string Currency { get; set; }
    public string Language { get; set; }
    public string NotificationPreferences { get; set; }
    public string StyleMode { get; set; }
    public int UserId { get; set; }
    
    // Navigation property
    public User User { get; set; }
} 