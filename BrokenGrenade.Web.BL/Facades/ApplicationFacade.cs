using AutoMapper;
using BrokenGrenade.Common.Enums;
using BrokenGrenade.Common.Models;
using BrokenGrenade.Web.DAL.Entities;
using BrokenGrenade.Web.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace BrokenGrenade.Web.BL.Facades;

public class ApplicationFacade : CRUDFacade<ApplicationEntity, ApplicationModel>
{
    public ApplicationFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
    }

    public async Task<int> GetUnhanledApplicationsCountAsync()
    {
        await using var uow = UnitOfWorkFactory.Create();
        return await uow.GetRepository<ApplicationEntity>()
            .Get()
            .CountAsync(x => x.Status == ApplicationStatus.AwaitingDecision);
    }
}