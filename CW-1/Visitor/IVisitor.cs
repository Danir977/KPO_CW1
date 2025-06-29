using CW_1.DomainModelClasses;

namespace CW_1.Visitor;

public interface IVisitor
{
        public void Visit(BankAccount account);
        public void Visit(Operation operation);
        public void Visit(Category category);
}