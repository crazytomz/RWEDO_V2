using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RWEDO.ViewModels
{
    public class SurveyFileViewModel
    {
        public SurveyFileViewModel()
        {
            Insurers = new List<KeyValuePair<int, string>>()
            {
                new KeyValuePair<int, string>(1, "New India Insurance"),
                new KeyValuePair<int, string>(2, "National Insurance"),
                new KeyValuePair<int, string>(3, "United India Insurance"),
                new KeyValuePair<int, string>(4, "Oriential Insurance"),
            };
            Statuses = new List<KeyValuePair<int, string>>();
        }
        public int ID { get; set; }
        [Required]
        public int Index { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        [Display(Name = "Insurer Name")]
        public int InsurerID { get; set; }
        [Required]
        [Display(Name ="File Status")]
        public int? StatusID { get; set; }
        [Display(Name ="Repairer Name")]
        public string RepairerName { get; set; }
        [Display(Name = "Repairer Email if any")]
        public string RepairerEmail { get; set; }
        [Required]
        [MaxLength(100)]
        public string Insured { get; set; }
        [Display(Name = "Estimate Date")]
        public string EstimateDate { get; set; }
        [Display(Name = "Billed Date")]
        public string BillDate { get; set; }
        [Display(Name = "Next Followup")]
        public string FollowUpDate { get; set; }
        [Display(Name ="Vehicle No(KL-XX-X-XXXX)")]
        public string VechicleNo { get; set; }
        [Required]
        [Display(Name = "File Received")]
        public bool HasFile { get; set; }
        [Required]
        [Display(Name = "Estimate Received")]
        public bool HasEstimate { get; set; }
        [Required]
        [Display(Name = "Bill Received")]
        public bool HasBill { get; set; }
        public List<KeyValuePair<int, string>> Insurers { get; set; }
        public List<KeyValuePair<int, string>> Statuses { get; set; }
    }
}
