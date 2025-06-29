using CW_1.Visitor;

namespace CW_1.DomainModelClasses;

public class Category : IVisitable
{
    public Guid Id { get; }
    public CategoryType Type { get; }
    public string Name { get; }

    public enum CategoryType { Income, Expense }

    public Category(CategoryType type, string name)
    {
        Id = Guid.NewGuid();
        Type = type;
        Name = name;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Информация о категории {Id}:" +
                          $"\nТип: {(Type == CategoryType.Income ? "Доход" : "Расход")}" +
                          $"\nНазвание: {Name}");
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}