using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SprintProjectManager.Models
{
    public class SprintStatusViewModel
    {
        public List<Sprint>? Sprints { get; set; }
        public SelectList? Statuses { get; set; }
        public string? SprintStatus {  get; set; }
        public string? SearchString { get; set; }
    }
}
