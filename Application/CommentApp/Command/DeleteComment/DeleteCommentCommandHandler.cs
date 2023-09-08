using Application.Common.Interfaces;
using Application.Common.Mediatr;
using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommentApp.Command.DeleteComment
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, bool>
    {
        private readonly IApplicationRepo _repo;

        public DeleteCommentCommandHandler(IApplicationRepo repo)
        {
            _repo = repo;
        }
        public async Task<RespDto<bool>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            return await _repo.CommentRepo.DeleteAsync(request.Id);
        }
    }
}
