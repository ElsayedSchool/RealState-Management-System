using Application.Common.Mediatr;
using Domain.Entities;
using Domain.Enums;

namespace Application.CategoryApp{
    
    public class UpsertCategoryCommand : IRequestWrapper<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AdsCategories category { get; set; }
    }
}
