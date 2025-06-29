namespace CW_1.BasicFunctional;

public class MainMenu
{
    private readonly AccountMenu _accountMenu;
    private readonly CategoryMenu _categoryMenu;
    private readonly OperationMenu _operationMenu;

    public MainMenu(
        AccountMenu accountMenu,
        CategoryMenu categoryMenu,
        OperationMenu operationMenu)
    {
        _accountMenu = accountMenu;
        _categoryMenu = categoryMenu;
        _operationMenu = operationMenu;
    }

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Учет финансов ===");
            Console.WriteLine("1. Управление счетами");
            Console.WriteLine("2. Управление категориями");
            Console.WriteLine("3. Управление операциями");
            Console.WriteLine("4. Выход");
            Console.Write("> ");

            switch (Console.ReadLine())
            {
                case "1":
                    _accountMenu.Show();
                    break;
                case "2":
                    _categoryMenu.Show();
                    break;
                case "3":
                    _operationMenu.Show();
                    break;
                case "4":
                    return;
            }
        }
    }
}