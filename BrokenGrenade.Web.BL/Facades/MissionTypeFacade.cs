using AutoMapper;
using BrokenGrenade.Common.Models;
using BrokenGrenade.Web.DAL.Entities;
using BrokenGrenade.Web.DAL.UnitOfWork;

namespace BrokenGrenade.Web.BL.Facades;

public class MissionTypeFacade : CRUDFacade<MissionTypeEntity, MissionTypeModel>
{
    public MissionTypeFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
    }
}