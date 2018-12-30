using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LeagueManagerPost.Models
{
    public class Player
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public int Number { get; set; }

        public bool Starter { get; set; }

        [Required]
        public Team Team { get; set; }


    }
}