using AutoMapper;
using BrokenGrenade.Common.Models;
using BrokenGrenade.Web.DAL.Entities;
using BrokenGrenade.Web.DAL.UnitOfWork;

namespace BrokenGrenade.Web.BL.Facades;

public class UserIsParticipatingTrainingFacade : CRUDFacade<UserIsParticipatingTrainingEntity, UserIsParticipatingTrainingModel>
{
    public UserIsParticipatingTrainingFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
    }
}