namespace LinkBook.DataAccess.Repository.IRepository;

public interface IunitOfWork
{
    IcategoryRepository Category { get; }

    void Save();
}