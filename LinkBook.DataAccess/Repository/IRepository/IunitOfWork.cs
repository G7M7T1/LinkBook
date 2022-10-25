namespace LinkBook.DataAccess.Repository.IRepository;

public interface IunitOfWork
{
    IcategoryRepository Category { get; }
    IcoverTypeRepository CoverType { get; }
    IProductRepository Product { get; }

    void Save();
}