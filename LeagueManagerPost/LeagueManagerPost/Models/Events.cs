using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LeagueManagerPost.Models
{
    public class Events
    {
        [Key]
        public int EventId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string ThemeColor { get; set; }
        public bool IsFullDay { get; set; }
        public int Event { get; set; }
    }
}