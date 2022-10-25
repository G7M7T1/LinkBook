using LinkBook.Models;

namespace LinkBook.DataAccess.Repository.IRepository;

public interface IProductRepository: IRepository<Product>
{
    void Update(Product obj);
}