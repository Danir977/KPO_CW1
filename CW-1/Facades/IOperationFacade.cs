using CW_1.DomainModelClasses;

namespace CW_1.Facades;

public interface IOperationFacade
{
    Operation Create(Operation.OperationType type, BankAccount account, decimal amount,
        DateTime date, Category category, string description);
    IEnumerable<Operation> GetAll();
}