using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SprintProjectManager.Models
{
    public class Sprint
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public string? Goal {  get; set; }
        public string Status { get; set; }
    }
}
