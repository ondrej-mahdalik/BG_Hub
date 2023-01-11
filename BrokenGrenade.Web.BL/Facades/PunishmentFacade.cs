using AutoMapper;
using BrokenGrenade.Common.Models;
using BrokenGrenade.Common.Models.Filters;
using BrokenGrenade.Web.DAL.Entities;
using BrokenGrenade.Web.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
namespace BrokenGrenade.Web.BL.Facades;

public class PunishmentFacade : CRUDFacade<PunishmentEntity, PunishmentModel>
{

    public PunishmentFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
    }

    public async Task<List<PunishmentModel>> GetActiveAsync()
    {
        await using var uow = UnitOfWorkFactory.Create();
        var query = uow.GetRepository<PunishmentEntity>()
            .Get()
            .Include(i => i.User)
            .Include(i => i.IssuedBy)
            .Where(x => !x.ExpiresOn.HasValue || x.ExpiresOn.Value > DateTime.Now);

        return await Mapper.ProjectTo<PunishmentModel>(query).ToListAsync().ConfigureAwait(false);
    }

    public async Task<List<PunishmentModel>> GetAsync(PunishmentFilterModel filter)
    {
        await using var uow = UnitOfWorkFactory.Create();
        var query = uow.GetRepository<PunishmentEntity>()
            .Get()
            .Include(i => i.User)
            .Include(i => i.IssuedBy)
            .Where(x => true);
        
        if (filter.UserNickname is not null)
            query = query.Where(x => x.User != null && x.User.Nickname.Contains(filter.UserNickname));
        
        if (filter.IsActive)
            query = query.Where(x => !x.ExpiresOn.HasValue || x.ExpiresOn.Value > DateTime.Now);
        
        if (filter.PunishmentName is not null)
            query = query.Where(x => x.Punishment.Contains(filter.PunishmentName));
        
        if (filter.IssuedByNickname is not null)
            query = query.Where(x => x.IssuedBy != null && x.IssuedBy.Nickname.Contains(filter.IssuedByNickname));

        return await Mapper.ProjectTo<PunishmentModel>(query).ToListAsync().ConfigureAwait(false);
    }
}
