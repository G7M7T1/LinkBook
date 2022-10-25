using LinkBook.DataAccess.Repository.IRepository;
using LinkBook.Models;

namespace LinkBook.DataAccess.Repository;

public class CoverTypeRepository: Repository<CoverType>, IcoverTypeRepository
{
    private ApplicationDbContext _db;

    public CoverTypeRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(CoverType obj)
    {
        _db.CoverTypes.Update(obj);
    }
}