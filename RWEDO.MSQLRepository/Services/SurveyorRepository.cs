using RWEDO.DataTransferObject;
using RWEDO.MSQLRepository.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace RWEDO.MSQLRepository.Services
{
    public class SurveyorRepository : ISurveyorRepository
    {
        private readonly RWEDODbContext context;
        public SurveyorRepository(RWEDODbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Surveyor> GetAllSurveyor()
        {
            return context.Surveyors;
        }
        public Surveyor Add(Surveyor surveyor)
        {
            context.Surveyors.Add(surveyor);
            context.SaveChanges();
            return surveyor;
        }
        public Surveyor GetSurveyor(int ID)
        {
            return context.Surveyors.Find(ID);
        }
    }
}
