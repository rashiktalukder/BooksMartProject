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
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public CoverTypeRepository(ApplicationDbContext db):base(db)
        {
            _db = db;   
        }

        public void Update(CoverType coverType)
        {
            var objFromDb=_db.CoverTypesModel.FirstOrDefault(s=>s.Id == coverType.Id);
            if(objFromDb!=null)
            {
                objFromDb.Name = coverType.Name;
                /*_db.SaveChanges();*/
            }
            
            
        }

        
    }
}
