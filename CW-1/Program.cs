using CW_1.BasicFunctional;
using Microsoft.Extensions.DependencyInjection;

var provider = DIConfig.ConfigureServices();
var mainMenu = provider.GetRequiredService<MainMenu>();
mainMenu.Show();