using Application.common.Interfaces.EntityRepositories;
using Application.Common.Interfaces;
using Application.Common.Interfaces.EntityRepositories;
using Infrastructure.Database.Repository;
using Infrastructure.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.UnitOfWork
{
    public class ApplicationRepo : IApplicationRepo
    {
        private readonly ApplicationDbContext _appDb;

        public ApplicationRepo(
            ApplicationDbContext appDb
            )
        {
            _appDb = appDb;
        }

        public ICategoryRepo CategoryRepo
        {
            get => new CategoryRepo(_appDb);
        }

        public IUserRepo UserRepo
        {
            get => new UserRepo(_appDb);
        }

        public IOfferRepo OfferRepo
        {
            get => new OfferRepo(_appDb);
        }

        public IFavoriteRepo FavoriteRepo
        {
            get => new FavoriteRepo(_appDb);
        }

        public IShareRepo ShareRepo
        {
            get => new ShareRepo(_appDb);
        }

        public ICommentRepo CommentRepo
        {
            get => new CommentRepo(_appDb);
        }

        public IPhotoRepo PhotoRepo
        {
            get => new PhotoRepo(_appDb);
        }

        public async Task<int> Commit()
        {
            return await _appDb.SaveChangesAsync();
        }
        public void Dispose()
        {
            _appDb.Dispose();
        }
    }
}
