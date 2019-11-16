using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class Trade
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        protected virtual List<Level> Levels { get; set; }
    }
}
