using CW_1.Visitor;

namespace CW_1.DomainModelClasses;

public class BankAccount : IVisitable
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public decimal Balance { get; private set; }
    
    public BankAccount(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        Balance = 0;
    }
    
    public void UpdateBalance(decimal amount)
    {
        if (Balance + amount < 0)
            throw new InvalidOperationException("Недостаточно средств на счете");
        Balance += amount;
    }

    public void Rename(string newName)
    {
        Name = newName;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Информация о счете {Id}:\nНазвание - {Name}\nБаланс - {Balance}");
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}