using BooksMartProject.DataAccess.Repository.IRepository;
using BooksMartProject.Models;
using BooksMartProject.Utility;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace BooksMartProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            CoverType coverType = new CoverType();
            if(id==null)
            {
                //For create...
                return View(coverType);
            }
            else
            {
                //For edit...
                //coverType= _unitOfWork.CoverType.Get(id.GetValueOrDefault());

                var parameter = new DynamicParameters();
                parameter.Add("@Id", id);
                coverType = _unitOfWork.SP_Call.OneRecord<CoverType>(StaticDetails.Proc_CoverType_Get, parameter);

                if (coverType==null)
                {
                    NotFound();
                }
                return View(coverType);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType coverType)
        {
            if(ModelState.IsValid)
            {
                var parameter=new DynamicParameters();
                parameter.Add("@Name",coverType.Name);
                if(coverType.Id == 0)
                {
                    //Create------------
                    //_unitOfWork.CoverType.Add(coverType);
                    //_unitOfWork.Save();
                    _unitOfWork.SP_Call.Execute(StaticDetails.Proc_CoverType_Create, parameter);
                }
                else
                {
                    //Update------------
                    //_unitOfWork.CoverType.Update(coverType);  
                    parameter.Add("@Id", coverType.Id);
                    _unitOfWork.SP_Call.Execute(StaticDetails.Proc_CoverType_Update, parameter);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));   
            }
            return View(coverType);

        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            // var allObj=_unitOfWork.CoverType.GetAll();
            var allObj = _unitOfWork.SP_Call.List<CoverType>(StaticDetails.Proc_CoverType_GetAll,null);
            return Json(new {data = allObj});
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            //var objDeleteFromDb = _unitOfWork.CoverType.Get(id);
            var objDeleteFromDb = _unitOfWork.SP_Call.OneRecord<CoverType>(StaticDetails.Proc_CoverType_Get,parameter);
            if (objDeleteFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            //_unitOfWork.CoverType.Remove(objDeleteFromDb);
            _unitOfWork.SP_Call.Execute(StaticDetails.Proc_CoverType_Delete,parameter);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Successfully Deleted!" });
        }

        #endregion
    }
}
