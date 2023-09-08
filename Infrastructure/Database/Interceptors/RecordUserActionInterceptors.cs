using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.common.Interceptors
{
    public class MySaveChangesInterceptor : ISaveChangesInterceptor
    {
        private readonly ICurrentUserService _currentUserService;

        public MySaveChangesInterceptor(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public void BeforeSaveChanges(DbContext dbContext, CancellationToken cancellationToken = default)
        {
            var userId = _currentUserService.UserId;

            foreach (var entry in dbContext.ChangeTracker.Entries<Offer>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedById = userId;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property(x => x.ModifiedById).CurrentValue = userId;
                }
            }
        }

        public Task BeforeSaveChangesAsync(DbContext dbContext, CancellationToken cancellationToken = default)
        {
            BeforeSaveChanges(dbContext, cancellationToken);
            return Task.CompletedTask;
        }

        public void AfterSaveChanges(DbContext dbContext)
        {
            // do nothing
        }

        public Task AfterSaveChangesAsync(DbContext dbContext, CancellationToken cancellationToken = default)
        {
            // do nothing
            return Task.CompletedTask;
        }
    }

}
