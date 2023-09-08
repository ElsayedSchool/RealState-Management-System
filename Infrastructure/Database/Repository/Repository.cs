using Application.Common.Interfaces.EntityRepositories;
using Application.Common.Models;
using Domain.Enums;
using Infrastructure.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDb;

        public Repository(ApplicationDbContext applicationDb)
        {
            _applicationDb = applicationDb;
        }


        // Get All entities
        public async Task<RespDto<List<T>>> GetAllAsync()
        {
            try
            {
                var requiredData = await _applicationDb.Set<T>().ToListAsync();
                return new RespDto<List<T>> { Data = requiredData };
            }
            catch (Exception)
            {
                return new RespDto<List<T>>().Get500Error($"There is an internal server error.");
            }
        }

        // Get By Id
        public async Task<RespDto<T>> FindByIdAsync(int id)
        {
            try
            {
                var requiredData = await _applicationDb.Set<T>().FindAsync(id);
                if (requiredData == null)
                    return new RespDto<T>() { Error = true, Message = $"{typeof(T).ToString()} Id is not exist." };
                return new RespDto<T> { Data = requiredData };
            }
            catch (Exception )
            {
                return new RespDto<T>().Get500Error($"There is an internal server error.");
            }
        }

        // Create new Entity
        public async Task<RespDto<bool>> CreateAsync(T entity)
        {
            try
            {
                if (entity == null) return new RespDto<bool>().Get400Error($"Data should be valid.");
                var resp = await _applicationDb.AddAsync(entity);
                //await _applicationDb.SaveChangesAsync();
                return new RespDto<bool>() { Data = true };
            }
            catch (Exception ex)
            {
                return new RespDto<bool>().Get500Error($"New {this.GetEntityName()} was not added,{ex.Message}");
            }
        }

        // Create List of entities
        public async Task<RespDto<bool>> CreateListAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null) return new RespDto<bool>().Get400Error($"{this.GetEntityName()} data list should be valid data.");
                await _applicationDb.AddRangeAsync(entities);
                await _applicationDb.SaveChangesAsync();
                return new RespDto<bool>() { Data = true };
            }
            catch (Exception ex)
            {
                
                return new RespDto<bool>().Get500Error($"Adding New list of {this.GetEntityName()} was failed ,{ex.Message}");
            }
        }

        // Update Entity
        public async Task<RespDto<bool>> UpdateAsync(object id, T entity)
        {
            try
            {
                var data = await _applicationDb.Set<T>().FindAsync(id);
                if (data == null) return new RespDto<bool>().GetNotFoundError($"{this.GetEntityName()}");
                _applicationDb.Update(entity);
                //await _applicationDb.SaveChangesAsync();
                return new RespDto<bool>() { Data = true };
            }
            catch (Exception)
            {
                return new RespDto<bool>().Get400Error($"Update Operation failed,This Data is not valid.");
            }
        }


        // Delete Entity
        public async Task<RespDto<bool>> DeleteAsync(int id)
        {
            try
            {
                var resp = await this.FindByIdAsync(id);
                if (resp != null && resp.Error == false)
                {
                    _applicationDb.Remove(resp.Data);
                    await _applicationDb.SaveChangesAsync();
                    return new RespDto<bool> { Data = true };
                }
                return new RespDto<bool> { Data = false, Error = true, Message = $"{this.GetEntityName()} is not exist" };
            }
            catch (Exception ex )
            {
                return new RespDto<bool>().Get500Error($"Delete Operation failed as Entity {this.GetEntityName()} is used in other entities, remove all related entities to complete this process.");

            }
        }

        // Delete List of Entities
        public async Task<RespDto<bool>> DeleteRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null || entities.Count() <= 0) return new RespDto<bool>().Get400Error($"{nameof(T)} data should be valid data.");
                _applicationDb.RemoveRange(entities);
                await _applicationDb.SaveChangesAsync();
                return new RespDto<bool> { Data = true };
            }
            catch (Exception ex)
            {
                //_logger.LogError($"DeleteRange {typeof(T)} Failed", ex.Message);
                return new RespDto<bool>().Get500Error($"List of {typeof(T)} was not removed,{ex.Message}");
            }
        }

        // Find All Entities with filteration
        public async Task<RespDto<IEnumerable<T>>> FindAllAsync(Expression<Func<T, bool>> filter)
        {
            try
            {
                IEnumerable<T> query = await _applicationDb.Set<T>().Where(filter).ToListAsync();
                return new RespDto<IEnumerable<T>> { Data = query };
            }
            catch (Exception)
            {
                return new RespDto<IEnumerable<T>>().Get500Error($"There is an internal server error.");
            }
        }

        // find all entites with respect to conditions and include required data
        public async Task<RespDto<IEnumerable<T>>> FindAllAsync(Expression<Func<T, bool>> filter, string[] includedList = null)
        {
            try
            {
                
                IQueryable<T> query = _applicationDb.Set<T>().Where(filter);
                if (includedList != null && query.Count() > 0)
                    foreach (var included in includedList)
                        query = query.Include(included);
                return new RespDto<IEnumerable<T>> { Data = await query.ToListAsync() };
            }
            catch (Exception ex)
            {
                return new RespDto<IEnumerable<T>>().Get500Error($"There is an internal server error.");
            }
        }


        // Add take and offset limitation
        public async Task<RespDto<IEnumerable<T>>> FindAllAsync(Expression<Func<T, bool>> filter, string[] includedList = null, int take = 1000, int offset = 0)
        {
            try
            {
                var requiredData = await this.FindAllAsync(filter, includedList);
                if (requiredData != null && requiredData.Data != null)
                {
                    var resp = requiredData.Data.Take(take).Skip(offset);
                    return new RespDto<IEnumerable<T>> { Data = resp };
                }
                return requiredData;
            }
            catch (Exception)
            {
                return new RespDto<IEnumerable<T>>().Get500Error($"There is an internal server error.");
            }
        }


        public async Task<T?> FindFirstOrDefaultAsync(Expression<Func<T, bool>> filter)
        {
            return await _applicationDb.Set<T>().FirstOrDefaultAsync(filter);
        }

        private string GetEntityName()
        {
            return typeof(T).ToString().Split('.').Last();
        }

        public async Task<bool> HasAny()
        {
            return await _applicationDb.Set<T>().AnyAsync();
        }

        public async Task<bool> HasAny(Expression<Func<T, bool>> filter)
        {
            return await _applicationDb.Set<T>().AnyAsync(filter);
        }
        public async Task<RespDto<bool>> HasAnyWithMessage(Expression<Func<T, bool>> filter)
        {
            var isExist = await _applicationDb.Set<T>().AnyAsync(filter);
            if (isExist)
            {
                return new RespDto<bool>() { Data = true };
            }else
            {
                return new RespDto<bool>().Get400Error($"Please send valid {GetEntityName()} data");
            }

        }
    }
}
