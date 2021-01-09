using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RWEDO.ViewModels
{
    public class SurveyFileViewModel
    {
        public int ID { get; set; }
        [Required]
        public int Index { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public int InsurerID { get; set; }
        [Display(Name ="Repairer Name")]
        public string RepairerName { get; set; }
        [Display(Name = "Repairer Email if any")]
        public string RepairerEmail { get; set; }
        [Required]
        [MaxLength(100)]
        public string Insured { get; set; }
        public string EstimateDate { get; set; }
        public string BillDate { get; set; }
        public string FollowUpDate { get; set; }
        [Display(Name ="Vehicle No(KL-XX-X-XXXX)")]
        public string VechicleNo { get; set; }
        [Required]
        public bool HasFile { get; set; }
        [Required]
        public bool HasEstimate { get; set; }
        [Required]
        public bool HasBill { get; set; }
    }
}
