using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace LeagueManagerPost.Models
{
    public class Repository
    {
        public void SaveNewPlayer(Player player, int teamId)
        {
            //paying the price of not having a foreign key here. 
            //reason #857 why I prefer foreign keys!
            using (var context = new ApplicationDbContext())
            {
                var team = context.Teams.Find(teamId);
                team.Players.Add(player);

                context.SaveChanges();
            }
        }

        public Player GetPlayersById(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Players.Find(id);
            }
        }

        public void SaveUpdatedPlayer(Player player, int teamId)
        {
            //paying the price of not having a foreign key here. 
            //reason #858 why I prefer foreign keys!
            using (var context = new ApplicationDbContext())
            {
                var playerWithTeamFromDatabase =
                  context.Players.Include(n => n.Team).FirstOrDefault(e => e.Id == player.Id);

                context.Entry(playerWithTeamFromDatabase).CurrentValues.SetValues(player);

                context.SaveChanges();
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