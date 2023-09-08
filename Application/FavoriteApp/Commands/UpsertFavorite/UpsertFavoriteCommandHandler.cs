using Application.Common.Interfaces;
using Application.Common.Mediatr;
using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FavoriteApp.Commands.UpsertFavorite
{
    public class UpsertFavoriteCommandHandler : IRequestHandler<UpsertFavoriteCommand, bool>
    {
        private readonly IApplicationRepo _repo;
        private readonly ICurrentUserService _currentUser;

        public UpsertFavoriteCommandHandler(IApplicationRepo repo, ICurrentUserService currentUser)
        {
            _repo = repo;
            _currentUser = currentUser;
        }
        public async Task<RespDto<bool>> Handle(UpsertFavoriteCommand request, CancellationToken cancellationToken)
        {
            
            Favorite? favorite;
            if (request.IsFavorite) {
                var favorites = await _repo.FavoriteRepo.FindAllAsync(p => p.OfferId == request.OfferId && p.UserId == _currentUser.UserId);
                if(favorites == null || favorites.Error == true || favorites.Data == null || favorites.Data.Count() == 0) return new RespDto<bool>() { Data = true};
                return  await _repo.FavoriteRepo.DeleteRangeAsync(favorites.Data);
            }
            favorite = new Favorite { Id = request.Id, UserId = _currentUser.UserId, OfferId = request.OfferId };
            var resp = await _repo.FavoriteRepo.CreateAsync(favorite);
            await _repo.Commit();
            return resp;
        }
    }
}
