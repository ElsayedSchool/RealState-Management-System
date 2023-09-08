using Application.common.Interfaces.EntityRepositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Repository
{
    public class PhotoRepo : Repository<Photo>, IPhotoRepo
    {
        public PhotoRepo(ApplicationDbContext applicationDb) : base(applicationDb)
        {
        }
    }
}
