using RWEDO.DataTransferObject;
using RWEDO.MSQLRepository.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace RWEDO.MSQLRepository.Services
{
    public class StatusRepository: IStatusRepository
    {
        private readonly RWEDODbContext context;
        public StatusRepository(RWEDODbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Status> GetAllStatuses()
        {
            return context.Status;
        }
    }
}
