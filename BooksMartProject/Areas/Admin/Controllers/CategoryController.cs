using BooksMartProject.DataAccess.Repository.IRepository;
using BooksMartProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace BooksMartProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Category category = new Category();
            if(id==null)
            {
                //For create...
                return View(category);
            }
            else
            {
                //For edit...
                category= _unitOfWork.Category.Get(id.GetValueOrDefault());
                if(category==null)
                {
                    NotFound();
                }
                return View(category);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
        {
            if(ModelState.IsValid)
            {
                if(category.Id == 0)
                {
                    //Create------------
                    _unitOfWork.Category.Add(category);
                    _unitOfWork.Save();
                }
                else
                {
                    //Update------------
                    _unitOfWork.Category.Update(category);  
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));   
            }
            return View(category);

        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj=_unitOfWork.Category.GetAll();
            return Json(new {data = allObj});
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objDeleteFromDb = _unitOfWork.Category.Get(id);
            if(objDeleteFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Category.Remove(objDeleteFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Successfully Deleted!" });
        }

        #endregion
    }
}
