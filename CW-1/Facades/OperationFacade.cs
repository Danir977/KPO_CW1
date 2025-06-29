using CW_1.DomainModelClasses;

namespace CW_1.Facades;

public class OperationFacade : IOperationFacade
{
    private readonly List<Operation> _operations = new();

    public Operation Create(Operation.OperationType type, BankAccount account, decimal amount,
                            DateTime date, Category category, string description)
    {
        var operation = new Operation(type, account, amount, date, category, description);
        _operations.Add(operation);
        operation.ApplyToAccount();
        return operation;
    }

    public IEnumerable<Operation> GetAll() => _operations;
}