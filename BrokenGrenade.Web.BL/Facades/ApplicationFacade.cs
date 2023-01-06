﻿using AutoMapper;
using BrokenGrenade.Common.Models;
using BrokenGrenade.Web.DAL.Entities;
using BrokenGrenade.Web.DAL.UnitOfWork;

namespace BrokenGrenade.Web.BL.Facades;

public class ApplicationFacade : CRUDFacade<ApplicationEntity, ApplicationModel>
{
    public ApplicationFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
    }
}