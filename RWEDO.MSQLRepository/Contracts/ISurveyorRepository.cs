﻿using RWEDO.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace RWEDO.MSQLRepository.Contracts
{
    public interface ISurveyorRepository
    {
        IEnumerable<Surveyor> GetAllSurveyor();
        Surveyor Add(Surveyor surveyor);
        Surveyor GetSurveyor(int ID);
    }
}
