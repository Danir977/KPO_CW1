using CW_1.DomainModelClasses;
using CW_1.Facades;

public class OperationMenu
{
    private readonly IOperationFacade _operationFacade;
    private readonly IBankAccountFacade _accountFacade;
    private readonly ICategoryFacade _categoryFacade;

    public OperationMenu(
        IOperationFacade operationFacade,
        IBankAccountFacade accountFacade,
        ICategoryFacade categoryFacade)
    {
        _operationFacade = operationFacade;
        _accountFacade = accountFacade;
        _categoryFacade = categoryFacade;
    }

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Управление операциями ===");
            Console.WriteLine("1. Добавить операцию");
            Console.WriteLine("2. Показать все операции");
            Console.WriteLine("3. Назад");
            Console.Write("> ");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    AddOperation();
                    break;
                case "2":
                    ShowAllOperations();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Неверный ввод! Используйте 1-3");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }

    private void AddOperation()
    {
        try
        {
            var account = SelectAccount();
            if (account == null) return;

            var category = SelectCategory();
            if (category == null) return;

            Console.Write("Сумма: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount == 0)
            {
                Console.WriteLine("Ошибка: введите корректную сумму (не ноль)");
                Console.ReadKey();
                return;
            }

            var type = category.Type == Category.CategoryType.Income 
                ? Operation.OperationType.Income 
                : Operation.OperationType.Expense;

            if (type == Operation.OperationType.Expense)
                amount = -Math.Abs(amount);

            Console.Write("Описание: ");
            var description = Console.ReadLine();

            _operationFacade.Create(type, account, Math.Abs(amount), DateTime.Now, category, description);
            
            Console.WriteLine("Операция успешно добавлена!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        Console.ReadKey();
    }

    private BankAccount SelectAccount()
    {
        var accounts = _accountFacade.GetAll().ToList();
        if (!accounts.Any())
        {
            Console.WriteLine("Нет доступных счетов. Сначала создайте счет.");
            Console.ReadKey();
            return null;
        }

        Console.WriteLine("\nВыберите счет:");
        for (int i = 0; i < accounts.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {accounts[i].Name} ({accounts[i].Balance:C})");
        }

        Console.Write("> ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= accounts.Count)
        {
            return accounts[index - 1];
        }

        Console.WriteLine("Неверный выбор счета!");
        Console.ReadKey();
        return null;
    }

    private Category SelectCategory()
    {
        var categories = _categoryFacade.GetAll().ToList();
        if (!categories.Any())
        {
            Console.WriteLine("Нет доступных категорий. Сначала создайте категории.");
            Console.ReadKey();
            return null;
        }

        Console.WriteLine("\nВыберите категорию:");
        for (int i = 0; i < categories.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {categories[i].Name} ({categories[i].Type})");
        }

        Console.Write("> ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= categories.Count)
        {
            return categories[index - 1];
        }

        Console.WriteLine("Неверный выбор категории!");
        Console.ReadKey();
        return null;
    }

    private void ShowAllOperations()
    {
        var operations = _operationFacade.GetAll().ToList();
        if (!operations.Any())
        {
            Console.WriteLine("Нет операций для отображения");
        }
        else
        {
            foreach (var op in operations.OrderBy(o => o.Date))
            {
                Console.WriteLine($"[{op.Date:dd.MM.yyyy}] {op.Category.Name}: " +
                                $"{(op.Type == Operation.OperationType.Income ? "+" : "-")}{op.Amount:C} " +
                                $"(Счет: {op.BankAccount.Name})");
                if (!string.IsNullOrEmpty(op.Description))
                    Console.WriteLine($"   Описание: {op.Description}");
            }
        }
        Console.ReadKey();
    }
}