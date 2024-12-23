namespace SmartFinanceAPI.Models;

public class User
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Email { get; set; }
    public string Tele { get; set; }
    public string Adresse { get; set; }
    public int Age { get; set; }
    public string Password { get; set; }

    // Navigation properties
    public UserSettings UserSettings { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
    public ICollection<Budget> Budgets { get; set; }
    public ICollection<Goal> Goals { get; set; }
    public ICollection<Notification> Notifications { get; set; }
    public ICollection<Report> Reports { get; set; }
} 