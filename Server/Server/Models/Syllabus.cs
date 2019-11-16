using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;

namespace Server.Models
{
    public class Syllabus
    {
        public int Id { get; set; }
     
        [Required]
        public string SyllabusName { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Trade is Required")]
        public int TradeId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Level is Required")]
        public int LevelId { get; set; }
        
        public string UploadedSyllabus { get; set; }
        public string UploadedTestPlan { get; set; }
        public string Languages { get; set; }

        [Required]
        public string DevelopmentOfficer { get; set; }

        [Required]
        public string Manager { get; set; }

        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; } = DateTime.Now;
        public DateTime? ActiveDate { get; set; }
        public string Status { get; set; }
    }
}
