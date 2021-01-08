using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RWEDO.DataTransferObject
{
    public class ApplicationUser : IdentityUser
    {
        [ForeignKey("Surveyor")]
        public int? SurveyorID { get; set; }
        public Surveyor Surveyor { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
    }
}
