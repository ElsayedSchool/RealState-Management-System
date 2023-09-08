using Application.common.Interfaces.EntityRepositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Repository
{
    public class UserRepo : Repository<User>, IUserRepo
    {
        public UserRepo(ApplicationDbContext applicationDb) : base(applicationDb)
        {
        }
    }
  
}
