using CW_1.DomainModelClasses;

namespace CW_1.Facades;

public class BankAccountFacade : IBankAccountFacade
{
    private readonly List<BankAccount> _accounts = new();

    public BankAccount Create(string name)
    {
        var account = new BankAccount(name);
        _accounts.Add(account);
        return account;
    }

    public IEnumerable<BankAccount> GetAll() => _accounts;

    public void RenameAccount(BankAccount account, string newName)
    {
        account.Rename(newName);
    }

    public void DeleteAccount(BankAccount account)
    {
        _accounts.Remove(account);
    }
    
    public BankAccount? GetById(Guid id) => _accounts.FirstOrDefault(a => a.Id == id);
}