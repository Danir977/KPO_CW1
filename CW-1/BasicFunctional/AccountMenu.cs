using CW_1.Facades;

namespace CW_1.BasicFunctional;

public class AccountMenu
{
    private readonly IBankAccountFacade _facade;

    public AccountMenu(IBankAccountFacade facade)
    {
        _facade = facade;
    }

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Управление счетами ===");
            Console.WriteLine("1. Создать счет");
            Console.WriteLine("2. Список счетов");
            Console.WriteLine("3. Переименовать счет");
            Console.WriteLine("4. Удалить счет");
            Console.WriteLine("5. Назад");
            Console.Write("> ");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    CreateAccount();
                    break;
                case "2":
                    ShowAllAccounts();
                    break;
                case "3":
                    RenameAccount();
                    break;
                case "4":
                    DeleteAccount();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Неверный ввод! Используйте 1-5");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }

    private void CreateAccount()
    {
        Console.Write("Название счета: ");
        var account = _facade.Create(Console.ReadLine());
        Console.WriteLine($"Создан счет: {account.Name} [ID: {account.Id}]");
        Console.ReadKey();
    }

    private void ShowAllAccounts()
    {
        var accounts = _facade.GetAll().ToList();
        if (!accounts.Any())
        {
            Console.WriteLine("Нет доступных счетов");
        }
        else
        {
            foreach (var acc in accounts)
            {
                Console.WriteLine($"[{acc.Id}] {acc.Name} - {acc.Balance:C}");
            }
        }
        Console.ReadKey();
    }

    private void RenameAccount()
    {
        try
        {
            ShowAllAccounts();
            Console.Write("\nВведите ID счета для переименования: ");
            var id = Guid.Parse(Console.ReadLine());
            
            var account = _facade.GetAll().FirstOrDefault(a => a.Id == id);
            if (account == null)
            {
                Console.WriteLine("Счет не найден!");
                return;
            }

            Console.Write("Новое название: ");
            var newName = Console.ReadLine();
            
            _facade.RenameAccount(account, newName);
            Console.WriteLine("Счет успешно переименован");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        Console.ReadKey();
    }

    private void DeleteAccount()
    {
        try
        {
            ShowAllAccounts();
            Console.Write("\nВведите ID счета для удаления: ");
            var id = Guid.Parse(Console.ReadLine());
            
            var account = _facade.GetAll().FirstOrDefault(a => a.Id == id);
            if (account == null)
            {
                Console.WriteLine("Счет не найден!");
                return;
            }

            _facade.DeleteAccount(account);
            Console.WriteLine("Счет успешно удален");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        Console.ReadKey();
    }
}