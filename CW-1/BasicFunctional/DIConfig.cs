using Microsoft.Extensions.DependencyInjection;
using CW_1.DomainModelClasses;
using CW_1.Facades;

namespace CW_1.BasicFunctional;

public static class DIConfig
{
    public static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        
        services.AddSingleton<IBankAccountFacade, BankAccountFacade>();
        services.AddSingleton<ICategoryFacade, CategoryFacade>();
        services.AddSingleton<IOperationFacade, OperationFacade>();
        
        services.AddSingleton<MainMenu>();
        services.AddSingleton<AccountMenu>();
        services.AddSingleton<CategoryMenu>();
        services.AddSingleton<OperationMenu>();

        return services.BuildServiceProvider();
    }
}