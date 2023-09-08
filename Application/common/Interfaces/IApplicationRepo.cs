using Application.common.Interfaces.EntityRepositories;
using Application.Common.Interfaces.EntityRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationRepo : IDisposable
    {
        ICategoryRepo CategoryRepo { get; }
        IUserRepo UserRepo { get; }
        IOfferRepo OfferRepo { get; }
        IFavoriteRepo FavoriteRepo { get; }
        IShareRepo ShareRepo { get; }
        ICommentRepo CommentRepo { get; }
        IPhotoRepo PhotoRepo { get; }
        Task<int> Commit();
    }
}
