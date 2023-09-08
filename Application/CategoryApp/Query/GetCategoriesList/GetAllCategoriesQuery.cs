using Application.Common.Mediatr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CategoryApp
{
    public class GetAllCategoriesQuery : IRequestWrapper<List<CategoriesListVm>>
    {
    }
}
