using CW_1.Facades;

namespace CW_1.Commands;

public class CreateAccountCommand : ICommand
{
    private readonly BankAccountFacade _facade;
    private readonly string _accountName;

    public CreateAccountCommand(BankAccountFacade facade, string accountName)
    {
        _facade = facade;
        _accountName = accountName;
    }

    public void Execute()
    {
        _facade.Create(_accountName);
    }
}