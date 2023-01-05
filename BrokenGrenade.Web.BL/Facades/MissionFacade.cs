using AutoMapper;
using BrokenGrenade.Common.Models;
using BrokenGrenade.Web.DAL.Entities;
using BrokenGrenade.Web.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace BrokenGrenade.Web.BL.Facades;

public class MissionFacade : CRUDFacade<MissionEntity, MissionModel>
{
    public MissionFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
    }

    public override async Task<MissionModel?> GetAsync(Guid id)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var query = uow
            .GetRepository<MissionEntity>()
            .Get()
            .Include(x => x.ModpackType)
            .Include(x => x.MissionType)
            .Include(x => x.Creator)
            .Where(e => e.Id == id);

        return await Mapper.ProjectTo<MissionModel>(query).SingleOrDefaultAsync().ConfigureAwait(false);
    }

    public override async Task<List<MissionModel>> GetAsync()
    {
        await using var uow = UnitOfWorkFactory.Create();
        var query = uow
            .GetRepository<MissionEntity>()
            .Get()
            .Include(x => x.ModpackType)
            .Include(x => x.MissionType)
            .Include(x => x.Creator);

        return await Mapper.ProjectTo<MissionModel>(query).ToListAsync().ConfigureAwait(false);
    }

    public async Task<List<MissionModel>> GetByUserAsync(Guid userId)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var query = uow
            .GetRepository<MissionEntity>()
            .Get()
            .Where(x => x.CreatorId == userId);

        return await Mapper.ProjectTo<MissionModel>(query).ToListAsync().ConfigureAwait(false);
    }
}