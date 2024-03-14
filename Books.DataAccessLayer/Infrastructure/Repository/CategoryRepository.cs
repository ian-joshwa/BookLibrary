using Books.DataAccessLayer.Data;
using Books.DataAccessLayer.Infrastructure.IRepository;
using Books.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.DataAccessLayer.Infrastructure.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Category category)
        {
            var cat = _context.Categories.Where(x => x.CategoryId == category.CategoryId).FirstOrDefault();
            if(cat != null)
            {
                cat.Name = category.Name;
                _context.Categories.Update(cat);
            }
        }
    }
}
