using CW_1.DomainModelClasses;

namespace CW_1.Facades;

public interface ICategoryFacade
{
    Category Create(Category.CategoryType type, string name);
    IEnumerable<Category> GetAll();
    Category? GetById(Guid id);
    void DeleteCategory(Category category);
}