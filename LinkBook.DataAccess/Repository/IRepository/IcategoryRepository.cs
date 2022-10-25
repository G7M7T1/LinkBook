using LinkBook.Models;

namespace LinkBook.DataAccess.Repository.IRepository;

public interface IcategoryRepository: IRepository<Category>
{
    void Update(Category obj);
}