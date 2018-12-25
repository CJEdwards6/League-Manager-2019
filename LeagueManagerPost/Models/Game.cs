using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeagueManagerPost.Models
{
    public class Game
    {
        public int Id { get; set; }

        public Team HomeTeam { get; set; }
        public int HomeTeamId { get; set; }

        public Team AwayTeam { get; set; }
        public int AwayTeamId { get; set; }
    }

    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}