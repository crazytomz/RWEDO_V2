using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RWEDO.ViewModels
{
    public class CreateSurveyorViewModel
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string IdentityNumber { get; set; }
    }
}
