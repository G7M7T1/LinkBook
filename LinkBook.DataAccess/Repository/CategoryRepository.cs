using LinkBook.DataAccess.Repository.IRepository;
using LinkBook.Models;

namespace LinkBook.DataAccess.Repository;

public class CategoryRepository: Repository<Category>, IcategoryRepository
{
    private ApplicationDbContext _db;

    public CategoryRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Category obj)
    {
        _db.Categories.Update(obj);
    }
}