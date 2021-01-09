using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RWEDO.DataTransferObject;
using RWEDO.MSQLRepository.Contracts;
using RWEDO.ViewModels;

namespace RWEDO.Controllers
{
    [Authorize]
    public class SurveyFileController : Controller
    {
        private readonly ISurveyFileRepository _surveyFileRepository;
        public SurveyFileController(ISurveyFileRepository surveyFileRepository)
        {
            _surveyFileRepository = surveyFileRepository;
        }
        public IActionResult ListSurveyFiles()
        {
            var model =_surveyFileRepository.GetAllSurveyFile();                              
            return View(model);
        }
        [HttpGet]
        public IActionResult CreateSurveyFile()
        {
            int fileIndex= _surveyFileRepository.GetAllSurveyFile().Where(x=>x.SurveyorID==1).Count()>0?_surveyFileRepository.GetAllSurveyFile().Where(x => x.SurveyorID == 1).OrderByDescending(x => x.Index).Select(x => x.Index).First():1;
            SurveyFileViewModel model = new SurveyFileViewModel { Index= fileIndex+1 };
            return View(model);
        }
        [HttpPost]
        public IActionResult CreateSurveyFile(SurveyFileViewModel model)
        {
            if (ModelState.IsValid)
            {
                SurveyFile surveyFile = new SurveyFile
                {
                    ID = model.ID,
                    SurveyorID = 1,
                    Index = model.Index,
                    Date = model.Date != "" ? DateTime.Parse(model.Date) : DateTime.Now,
                    InsurerID = model.InsurerID,
                    RepairerName = model.RepairerName,
                    RepairerEmail = model.RepairerEmail,
                    Insured = model.Insured,
                    EstimateDate = model.EstimateDate,
                    BillDate = model.BillDate,
                    FollowUpDate = model.Date != "" ? DateTime.Parse(model.Date) : DateTime.Now.AddDays(10),
                    HasFile = model.HasFile,
                    HasBill = model.HasBill,
                    HasEstimate = model.HasEstimate,
                    StatusID = 1,
                    VechicleNo = model.VechicleNo,
                };

                var result = _surveyFileRepository.Add(surveyFile);
                if (result != null)
                    return RedirectToAction("ListSurveyFiles", "SurveyFile");
            }
            return View();
        }
    }
}