using CW_1.DomainModelClasses;
using CW_1.Facades;

namespace CW_1.Commands;

public class AddOperationCommand : ICommand
{
    private readonly OperationFacade _facade;
    private readonly Operation.OperationType _type;
    private readonly BankAccount _account;
    private readonly decimal _amount;
    private readonly DateTime _date;
    private readonly Category _category;
    private readonly string _description;

    public AddOperationCommand(OperationFacade facade, Operation.OperationType type, BankAccount account, decimal amount, DateTime date, Category category, string description)
    {
        _facade = facade;
        _type = type;
        _account = account;
        _amount = amount;
        _date = date;
        _category = category;
        _description = description;
    }

    public void Execute()
    {
        _facade.Create(_type, _account, _amount, _date, _category, _description);
    }
}