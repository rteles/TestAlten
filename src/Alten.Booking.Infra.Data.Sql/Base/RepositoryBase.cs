﻿using Alten.Booking.Core.Mediator.Interfaces;
using Alten.Booking.Infra.Data.Sql.Base.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Alten.Booking.Infra.Data.Sql.Base;

public abstract class RepositoryBase<TDomainEntity, TEntityModel> :
    ReadOnlyRepositoryBase<TDomainEntity, TEntityModel>,
    IRepositoryBase<TDomainEntity>
    where TDomainEntity : class
    where TEntityModel : class
{
    private readonly DbContext _context;
    private readonly IMapper _mapper;

    protected RepositoryBase(DbContext context, IMapper mapper, IMediatorHandler mediatorHandler) 
        : base(context, mapper, mediatorHandler)
    {
        _context = context;
        _mapper = mapper;
    }

    public virtual async Task Add(TDomainEntity obj)
    {
        var model = _mapper.Map<TEntityModel>(obj);
        await _context.Set<TEntityModel>().AddAsync(model);
    }

    public virtual void Update(TDomainEntity obj)
    {
        var model = _mapper.Map<TEntityModel>(obj);
        if (DetachObj(model))
            _context.Entry(model).State = EntityState.Modified;
    }

    public virtual void Remove(TDomainEntity obj)
    {
        var model = _mapper.Map<TEntityModel>(obj);
        if (DetachObj(model))
            _context.Entry(model).State = EntityState.Deleted;
    }
    
    public virtual async Task<bool> Commit() => await _context.SaveChangesAsync() > 0;

    #region private

    private bool DetachObj(TEntityModel obj)
    {
        var propKey = (
            from p in typeof(TDomainEntity).GetProperties()
            where p.Name.ToUpper().Equals("ID")
                  && p.PropertyType == typeof(int)
            select p
        ).FirstOrDefault();

        if (propKey == default)
            return false;

        var entity = _context.Set<TDomainEntity>().Find(propKey.GetValue(obj));
        if (entity == default)
            return false;

        _context.Entry(entity).State = EntityState.Detached;
        return true;
    }

    #endregion private
}