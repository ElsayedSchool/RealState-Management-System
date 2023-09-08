using Application.CommentApp.Query.GetAllCommentsQuery;
using Application.Common.Interfaces.EntityRepositories;
using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.common.Interfaces.EntityRepositories
{
    public interface ICommentRepo : IRepository<Comment>
    {
        Task<RespDto<List<CommentDetailVm>>> GetAllOfferCommentsQuery(int offerId);
    }
}
