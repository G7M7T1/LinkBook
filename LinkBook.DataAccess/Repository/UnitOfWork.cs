using LinkBook.DataAccess.Repository.IRepository;

namespace LinkBook.DataAccess.Repository;

public class UnitOfWork : IunitOfWork
{
    private ApplicationDbContext _db;

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Category = new CategoryRepository(_db);
    }
    
    public IcategoryRepository Category { get; private set; }
    public void Save()
    {
        _db.SaveChanges();
    }
}