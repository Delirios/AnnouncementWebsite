using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnnouncementWebsite.Models;

namespace AnnouncementWebsite.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AnnouncementContext _announcementContext;

        public CategoryRepository(AnnouncementContext announcementContext)
        {
            _announcementContext = announcementContext;
        }

        public IEnumerable<Category> AllCategories => _announcementContext.Categories;
    }
}
