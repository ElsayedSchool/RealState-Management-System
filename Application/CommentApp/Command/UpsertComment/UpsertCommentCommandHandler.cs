using Application.Common.Interfaces;
using Application.Common.Mediatr;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommentApp.Command.UpsertComment
{
    public class UpsertCommentCommandHandler : IRequestHandler<UpsertCommentCommand, bool>
    {
        private readonly IApplicationRepo _repo;
        private readonly ICurrentUserService _currentUser;

        public UpsertCommentCommandHandler(IApplicationRepo repo,ICurrentUserService currentUser)
        {
            _repo = repo;
            _currentUser = currentUser;
        }

        public async Task<RespDto<bool>> Handle(UpsertCommentCommand request, CancellationToken cancellationToken)
        {
            Comment? comment;
            if(request.Id != 0)
            {
                var resp  = await _repo.CommentRepo.FindByIdAsync(request.Id);
                if(resp == null || resp.Error == true) return new RespDto<bool> { Error = true, Message = "حدث خطا اثناء استرجاع بيانات الملاحظه" };
                comment = resp.Data;
            }else
            {
                comment = new Comment();
                await _repo.CommentRepo.CreateAsync(comment);
                comment.OfferId = request.OfferId;
                comment.UserId = _currentUser.UserId;
            }

            comment.Detail = request.Details;
            comment.Date = DateTime.UtcNow;

            await _repo.Commit();

            return new RespDto<bool> { Data = true };
        }
    }
}
