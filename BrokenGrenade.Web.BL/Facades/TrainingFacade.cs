using AutoMapper;
using BrokenGrenade.Common.Models;
using BrokenGrenade.Web.DAL.Entities;
using BrokenGrenade.Web.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace BrokenGrenade.Web.BL.Facades;

public class TrainingFacade : CRUDFacade<TrainingEntity, TrainingModel>
{
    public TrainingFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
    }

    public async Task<List<TrainingModel>> GetUpcomingAsync()
    {
        await using var uow = UnitOfWorkFactory.Create();
        var query = uow.GetRepository<TrainingEntity>()
            .Get()
            .Include(i => i.Creator)
            .Where(x => x.Date.Date > DateTime.Today);

        return await Mapper.ProjectTo<TrainingModel>(query).ToListAsync().ConfigureAwait(false);
    }
}