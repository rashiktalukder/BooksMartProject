using BooksMartProject.DataAccess.Data;
using BooksMartProject.DataAccess.Repository.IRepository;
using BooksMartProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksMartProject.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db):base(db)
        {
            _db = db;   
        }

        public void Update(Category category)
        {
            var objFromDb=_db.CategoryModel.FirstOrDefault(s=>s.Id == category.Id);
            if(objFromDb!=null)
            {
                objFromDb.Name = category.Name;
                _db.SaveChanges();
            }
            
            
        }
    }
}
