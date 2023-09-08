using Application.Common.Interfaces.EntityRepositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.common.Interfaces.EntityRepositories
{
    public interface IPhotoRepo : IRepository<Photo>
    {
    }
}
