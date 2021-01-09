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
        private readonly IStatusRepository _statusRepository;
        public SurveyFileController(ISurveyFileRepository surveyFileRepository, IStatusRepository statusRepository)
        {
            _surveyFileRepository = surveyFileRepository;
            _statusRepository = statusRepository;
        }
        [Authorize(Policy = "CanReadPolicy")]
        public IActionResult ListSurveyFiles()
        {
            var model =_surveyFileRepository.GetAllSurveyFile();                              
            return View(model);
        }
        [HttpGet]
        [Authorize(Policy = "CanWritePolicy")]
        public IActionResult CreateSurveyFile()
        {
            int fileIndex= _surveyFileRepository.GetAllSurveyFile().Where(x=>x.SurveyorID==1).Count()>0?_surveyFileRepository.GetAllSurveyFile().Where(x => x.SurveyorID == 1).OrderByDescending(x => x.Index).Select(x => x.Index).First():1;
            var statuses = _statusRepository.GetAllStatuses();
            SurveyFileViewModel model = new SurveyFileViewModel
            {
                Index = fileIndex+1,
                Statuses = statuses.Select(s => new KeyValuePair<int, string>(s.ID, s.Description)).ToList(),
            };
            return View(model);
        }        
        [HttpGet]
        [Authorize(Policy = "CanReadPolicy")]
        public IActionResult EditSurveyFile(int ID, string status)
        {
            var model = _surveyFileRepository.GetSurveyFile(ID);
            var statuses = _statusRepository.GetAllStatuses();
            SurveyFileViewModel surveyFileVM = new SurveyFileViewModel
            {
                ID = model.ID,
                Index = model.Index,
                Date = model.Date.ToString("dd-MMM-yyyy"),
                InsurerID = model.InsurerID,
                RepairerName = model.RepairerName,
                RepairerEmail = model.RepairerEmail,
                Insured = model.Insured,
                EstimateDate = model.EstimateDate,
                BillDate = model.BillDate,
                FollowUpDate = model.Date.ToString("dd-MMM-yyyy"),
                HasFile = model.HasFile,
                HasBill = model.HasBill,
                HasEstimate = model.HasEstimate,
                StatusID = model.StatusID,
                VechicleNo = model.VechicleNo,
                Statuses = statuses.Select(s => new KeyValuePair<int, string>(s.ID, s.Description)).ToList(),
            };
            ViewBag.ActionStatus = status;
            return View(surveyFileVM);
        }
        [HttpPost]
        [Authorize(Policy = "CanWritePolicy")]
        public IActionResult SaveSurveyFile(SurveyFileViewModel model)
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
                    StatusID = model.StatusID,
                    VechicleNo = model.VechicleNo,
                };

                var result = surveyFile.ID>0? _surveyFileRepository.Update(surveyFile) : _surveyFileRepository.Add(surveyFile);
                if (result != null)
                    return RedirectToAction("EditSurveyFile", "SurveyFile", new { ID = result.ID, status = "success" });
                else
                    return RedirectToAction("EditSurveyFile", "SurveyFile", new { ID = result.ID, status = "fail" });
            }
            return View();
        }
        [HttpGet]
        [Authorize(Policy = "CanDeletePolicy")]
        public IActionResult DeleteSurveyFile(int ID)
        {
            var surveyFile = _surveyFileRepository.Delete(ID);
            if (surveyFile == null)
            {
                return RedirectToAction("EditSurveyFile", "SurveyFile", new { ID = ID, status = "fail" });
            }
            else
            {
                return RedirectToAction("ListSurveyFiles", "SurveyFile");
            }
        }
    }
}