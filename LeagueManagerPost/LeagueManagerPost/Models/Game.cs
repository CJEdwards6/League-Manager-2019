using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LeagueManagerPost.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The location name must be between 5 and 50 characters",
            MinimumLength = 5)]
        public string Location { get; set; }


        [Required]
        [Display(Name = "Home Team")]
        public Team HomeTeam { get; set; }
        public int HomeTeamId { get; set; }

        [Required]
        [Display(Name = "Away Team")]
        public Team AwayTeam { get; set; }
        public int AwayTeamId { get; set; }
    }

    public class Team
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The coach name must be between 5 and 30 characters",
            MinimumLength = 5)]
        public string Coach { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The team name must be between 5 and 50 characters",
            MinimumLength = 5)]
        [Display(Name = "Team Name")]
        public string Name { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }


        [Display(Name = "Win %")]
        public float WinPercentage
        {
            get
            {
                if ((Wins + Losses) != 0)
                {
                    return (float)Wins / (Wins + Losses);
                }
                else
                {
                    return 0;
                }
            }


        }
    }
}