using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult Index()
        {
            var model = new List<SurveyFileViewModel>();
            foreach (var surveyFile in _surveyFileRepository.GetAllSurveyFile())
            {
                var surveyFileViewModel = new SurveyFileViewModel
                {
                    ID = surveyFile.ID,
                    Index = surveyFile.Index,
                    Date = surveyFile.Date.ToString("dd-MMM-yyyy"),
                    InsurerID = surveyFile.InsurerID,
                    RepairerName = surveyFile.RepairerName,
                    RepairerEmail = surveyFile.RepairerEmail,
                };

                model.Add(surveyFileViewModel);
            }                          
            return View(model);
        }
    }
}