using CW_1.DomainModelClasses;
using CW_1.Facades;

namespace CW_1.BasicFunctional;

public class CategoryMenu
{
    private readonly ICategoryFacade _facade;

    public CategoryMenu(ICategoryFacade facade)
    {
        _facade = facade;
    }

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Управление категориями ===");
            Console.WriteLine("1. Создать категорию");
            Console.WriteLine("2. Список категорий");
            Console.WriteLine("3. Удалить категорию");
            Console.WriteLine("4. Назад");
            Console.Write("> ");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    CreateCategory();
                    break;
                case "2":
                    ShowAllCategories();
                    break;
                case "3":
                    DeleteCategory();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Неверный ввод!");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }

    private void CreateCategory()
    {
        try
        {
            Console.Write("Тип (1-Доход, 2-Расход): ");
            var type = Console.ReadLine() == "1" 
                ? Category.CategoryType.Income 
                : Category.CategoryType.Expense;

            Console.Write("Название: ");
            var name = Console.ReadLine();

            var category = _facade.Create(type, name);
            Console.WriteLine($"Создана категория: {category.Name} ({category.Type})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        Console.ReadKey();
    }

    private void ShowAllCategories()
    {
        Console.WriteLine("\nСписок категорий:");
        var categories = _facade.GetAll().ToList();
        
        if (!categories.Any())
        {
            Console.WriteLine("Категории не найдены");
        }
        else
        {
            foreach (var cat in categories)
            {
                Console.WriteLine($"[{cat.Id}] {cat.Name} ({cat.Type})");
            }
        }
        Console.ReadKey();
    }

    private void DeleteCategory()
    {
        try
        {
            ShowAllCategories();
            Console.Write("\nВведите ID категории для удаления: ");
            var id = Guid.Parse(Console.ReadLine());
            
            var category = _facade.GetAll().FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                _facade.DeleteCategory(category);
                Console.WriteLine("Категория удалена");
            }
            else
            {
                Console.WriteLine("Категория не найдена");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        Console.ReadKey();
    }
}