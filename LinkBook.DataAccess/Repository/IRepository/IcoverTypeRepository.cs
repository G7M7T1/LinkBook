using LinkBook.Models;

namespace LinkBook.DataAccess.Repository.IRepository;

public interface IcoverTypeRepository: IRepository<CoverType>
{
    void Update(CoverType obj);
}