namespace CW_1.Visitor;

public interface IVisitable
{
    void Accept(IVisitor visitor);
}