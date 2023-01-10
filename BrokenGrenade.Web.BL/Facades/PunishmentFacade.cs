using AutoMapper;
using BrokenGrenade.Common.Models;
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
}
