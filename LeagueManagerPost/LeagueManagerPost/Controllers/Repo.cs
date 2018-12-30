using LeagueManagerPost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace LeagueManagerPost.Controllers
{
    public class Repo
    {
        public List<Game> GetgamesWithTeams()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Games.AsNoTracking().Include(g => g.HomeTeam).Include(g => g.AwayTeam).ToList();
            }
        }

        public Team GetTeamWithPlayers(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Teams.AsNoTracking().Include(n => n.Players)
                  .FirstOrDefault(n => n.Id == id);
            }
        }
    }
}