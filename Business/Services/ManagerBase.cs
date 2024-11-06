using AutoMapper;
using Core.DataAccess;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public abstract class ManagerBase<TEntity, TMapTo> : IServiceBase<TEntity, TMapTo> 
        where TEntity : class, IEntity, new() 
        where TMapTo : class, IDto, new()
    {
        protected readonly IEntityRepository<TEntity> _dal;
        protected IMapper _mapper;
                            
        public ManagerBase(IEntityRepository<TEntity> dal, IMapper mapper)
        {
            _dal = dal;
            _mapper = mapper;
        }
        public virtual async Task<TMapTo?> AddOrUpdateAsync(TMapTo model)
        {
            try
            {
                if (_mapper != null)
                {
                    if (model.Id == Guid.Empty)
                    {
                        var entity = await _dal.AddAsync(_mapper.Map<TEntity>(model));
                        return _mapper.Map<TMapTo>(entity);
                    }
                    else
                    {
                        await _dal.UpdateAsync(_mapper.Map<TEntity>(model));
                        return model;
                    }


                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task DeleteAsync(TMapTo model)
        {
            if (_mapper != null)
            {
                await _dal.DeleteAsync(_mapper.Map<TEntity>(model));
            }
        }

        public virtual async Task<TMapTo?> GetByIdAsync(Guid id)
        {
            TMapTo? res = null;
            var entity = await _dal.GetAsync(c => c.Id == id);
            if(entity == null && id != Guid.Empty)
            {
                throw new Exception("Kayıt bulunamadı");
            }
            if(_mapper != null)
            {
                res = _mapper.Map<TMapTo>(entity);
            }
            return res;
        }

        public virtual async Task<List<TMapTo>> GetListAsync(Expression<Func<TEntity, bool>>? filter = null, Expression<Func<TEntity, object>>[]? includes = null)
        {
            List<TMapTo>? res = null;
            var list = await _dal.GetListAsync(filter, includes);
            if(_mapper != null)
            {
                res = _mapper.Map<List<TMapTo>>(list);
            }
            return res ?? new List<TMapTo>();
        }
    }
    public interface IServiceBase<TEntity,TMapTo>
        where TEntity : class,IEntity,new()
        where TMapTo : class, IDto, new()
    {
        Task<List<TMapTo>> GetListAsync(Expression<Func<TEntity, bool>>? filter = null, Expression<Func<TEntity, object>>[]? includes = null);
        Task<TMapTo?> GetByIdAsync(Guid id);
        Task<TMapTo?> AddOrUpdateAsync(TMapTo model);
        Task DeleteAsync(TMapTo model);
    }
}
