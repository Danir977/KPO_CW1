using CW_1.DomainModelClasses;

namespace CW_1.Facades;

public interface IBankAccountFacade
{
    BankAccount Create(string name);
    BankAccount? GetById(Guid id);
    IEnumerable<BankAccount> GetAll();
    void RenameAccount(BankAccount account, string newName);
    void DeleteAccount(BankAccount account);
}