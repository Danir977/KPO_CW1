using CW_1.DomainModelClasses;

namespace CW_1.Facades;

public class CategoryFacade : ICategoryFacade
{
    private readonly List<Category> _categories = new();

    public Category Create(Category.CategoryType type, string name)
    {
        var category = new Category(type, name);
        _categories.Add(category);
        return category;
    }

    public IEnumerable<Category> GetAll() => _categories;
    
    public Category? GetById(Guid id) => _categories.FirstOrDefault(c => c.Id == id);

    public void DeleteCategory(Category category)
    {
        _categories.Remove(category);
    }
}