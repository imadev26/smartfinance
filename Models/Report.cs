namespace SmartFinanceAPI.Models;

public class Report
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string ReportType { get; set; }
    public DateTime GeneratedDate { get; set; }

    // Navigation property
    public User User { get; set; }
} 