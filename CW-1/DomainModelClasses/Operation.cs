using CW_1.Visitor;

namespace CW_1.DomainModelClasses;

public class Operation : IVisitable
{
    public enum OperationType { Income, Expense }
    
    public Guid Id { get; }
    public OperationType Type { get; }
    public BankAccount BankAccount { get; }
    public decimal Amount { get; }
    public DateTime Date { get; }
    public string Description { get; }
    public Category Category { get; }
    
    private Guid CategoryId => Category.Id;
    private Guid BankAccountId => BankAccount.Id;

    public Operation(
        OperationType type, 
        BankAccount bankAccount, 
        decimal amount, 
        DateTime date, 
        Category category,
        string description = null)
    {
        Id = Guid.NewGuid();
        Type = type;
        BankAccount = bankAccount;
        Amount = amount;
        Date = date;
        Category = category;
        Description = description;
    }

    public void ApplyToAccount()
    {
        BankAccount.UpdateBalance(
            Type == OperationType.Income ? Amount : -Amount
        );
    }

    public void PrintInfo()
    {
        Console.WriteLine($"[{Date:dd.MM.yyyy}] {Category.Name}: {Amount} " +
                          $"({(Type == OperationType.Income ? "+" : "-")})");
        if (!string.IsNullOrEmpty(Description))
            Console.WriteLine($"   Описание: {Description}");
    }
    
    public override string ToString()
    {
        return $"{Date:yyyy-MM-dd} {Type} {Amount} [{Category.Name}]";
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}