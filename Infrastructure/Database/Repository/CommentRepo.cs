using Application.CommentApp.Query.GetAllCommentsQuery;
using Application.common.Interfaces.EntityRepositories;
using Application.Common.Interfaces.EntityRepositories;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Repository
{
    public class CommentRepo : Repository<Comment>, ICommentRepo
    {
        private readonly ApplicationDbContext _applicationDb;

        public CommentRepo(ApplicationDbContext applicationDb) : base(applicationDb)
        {
            _applicationDb = applicationDb;
        }

        public async Task<RespDto<List<CommentDetailVm>>> GetAllOfferCommentsQuery(int offerId)
        {
            try
            {
                var resp = await _applicationDb.Comments
                    .Include(p => p.User)
                    .Where(p => p.OfferId == offerId)
                    .Select(c => 
                        new CommentDetailVm() 
                            { 
                                Id = c.Id, 
                                Date = c.Date, 
                                Detail = c.Detail, 
                                OfferId = c.OfferId, 
                                EmployeeId = c.UserId,
                                EmployeeName = c.User != null ? c.User.Name : "", 
                        }).ToListAsync();

                return new RespDto<List<CommentDetailVm>>() { Data = resp };
            }
            catch (Exception ex)
            {
                return new RespDto<List<CommentDetailVm>>().Get500Error("حدث خطا اثناء استرجاع الملاحظات على العرض");
            }
        }
    }
}
