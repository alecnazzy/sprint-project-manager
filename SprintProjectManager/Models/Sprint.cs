using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SprintProjectManager.Models
{
    public class Sprint
    {
        public int Id { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        [StringLength(60, MinimumLength = 6)]
        [Required]
        public string? Name { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        [StringLength (60, MinimumLength = 6)]
        [Required]
        public string? Goal {  get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [StringLength(15, MinimumLength = 6)]
        [Required]
        public string Status { get; set; }
    }
}
