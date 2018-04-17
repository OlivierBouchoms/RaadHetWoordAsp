using Models;

namespace Data
{
    public class TeamRepository
    {
        private readonly ITeamContext context;

        public TeamRepository(ITeamContext context)
        {
            this.context = context;
        }

        public bool CheckIfExists(Team team)
        {
            return context.CheckIfExists(team);
        }

        public bool AddTeam(Team team)
        {
            return context.AddTeam(team);
        }

        public Team FillWithData(Team team)
        {
            return context.FillWithData(team);
        }

        public bool IncreaseScore(Team team)
        {
            return context.IncreaseScore(team);
        }

        public bool DecreaseScore(Team team)
        {
            return context.DecreaseScore(team);
        }

        public bool InceaseTurns(Team team)
        {
            return context.IncreaseTurns(team);
        }
    }
}