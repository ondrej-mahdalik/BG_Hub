using AutoMapper;
using BrokenGrenade.Common.Models;
using BrokenGrenade.Web.DAL.Entities;
using BrokenGrenade.Web.DAL.UnitOfWork;

namespace BrokenGrenade.Web.BL.Facades;

public class ModpackTypeFacade : CRUDFacade<ModpackTypeEntity, ModpackTypeModel>
{
    public ModpackTypeFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
    }
}