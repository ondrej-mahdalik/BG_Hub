namespace BrokenGrenade.Web.DAL.UnitOfWork;

public interface IUnitOfWorkFactory
{
    IUnitOfWork Create();
}