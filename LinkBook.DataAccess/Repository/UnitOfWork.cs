using LinkBook.DataAccess.Repository.IRepository;

namespace LinkBook.DataAccess.Repository;

public class UnitOfWork : IunitOfWork
{
    private ApplicationDbContext _db;

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Category = new CategoryRepository(_db);
        CoverType = new CoverTypeRepository(_db);
        Product = new ProductRepository(_db);
    }
    
    public IcategoryRepository Category { get; private set; }
    public IcoverTypeRepository CoverType { get; private set; }
    public IProductRepository Product { get; private set; }
    public void Save()
    {
        _db.SaveChanges();
    }
}