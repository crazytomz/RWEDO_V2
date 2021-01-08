using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RWEDO.DataTransferObject;
using RWEDO.MSQLRepository.Contracts;
using RWEDO.ViewModels;

namespace RWEDO.Controllers
{
    public class SurveyorController : Controller
    {
        private readonly ISurveyorRepository _surveyorRepository;
        public SurveyorController(ISurveyorRepository surveyorRepository)
        {
            _surveyorRepository = surveyorRepository;
        }
        public IActionResult ListSurveyors()
        {
            var surveyors = _surveyorRepository.GetAllSurveyor();
            return View(surveyors);
        }
        [HttpGet]
        public IActionResult CreateSurveyor()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateSurveyor(CreateSurveyorViewModel model)
        {
            if (ModelState.IsValid)
            {
                Surveyor surveyor = new Surveyor
                {
                    Name = model.Name,
                    IdentityNumber = model.IdentityNumber,
                    ISDeactivated = false,
                };

                var result = _surveyorRepository.Add(surveyor);
                if(result!=null)
                return RedirectToAction("ListSurveyors", "Surveyor");
            }
            return View(model);
        }
    }
}