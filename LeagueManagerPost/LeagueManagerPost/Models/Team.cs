using System.ComponentModel.DataAnnotations;

namespace LeagueManagerPost.Models
{
    public class Team
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The team name must be between 5 and 50 characters",
            MinimumLength = 5)]
        [Display(Name = "Team Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The coach name must be between 5 and 30 characters",
            MinimumLength = 5)]
        public string Coach { get; set; }
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