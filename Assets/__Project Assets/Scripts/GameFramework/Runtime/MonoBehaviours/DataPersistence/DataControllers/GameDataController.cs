namespace GameFramework
{
    using d4160.GameFramework;
    using System.Linq;
    using UnityEngine;

    public class GameDataController : DataControllerBase
    {
        public override T GetScriptable<T>(int dataIdx = 0)
        {
            var database = GameFrameworkSettings.GameDatabase;
            var scriptable = database.GameData[dataIdx];

            return scriptable as T;
        }

        public DefaultPlayer[] GetPlayers()
        {
            var scriptable = GetScriptable<PlayersSO>(Archetypes.GetFixed(Archetypes.Player));
            return scriptable.Elements.ToArray();
        }

        public void AddPlayer(DefaultPlayer element)
        {
            var scriptable = GetScriptable<PlayersSO>(Archetypes.GetFixed(Archetypes.Player));
            scriptable.Add(element);
        }

        public DefaultLeaderboard[] GetLeaderboards()
        {
            var scriptable = GetScriptable<LeaderboardsSO>();
            return scriptable.Elements.ToArray();
        }

        public DefaultLeaderboard GetLeaderboard(int id)
        {
            var elements = GetScriptable<LeaderboardsSO>(Archetypes.GetFixed(Archetypes.Leaderboard)).Elements;
            return elements.FirstOrDefault((x) => x.ID == id );
        }

        public DefaultLeaderboard GetLeaderboardByStat(string statId)
        {
            return GetLeaderboardByStat(Animator.StringToHash(statId));
        }

        public DefaultLeaderboard GetLeaderboardByStat(int statHash)
        {
            var elements = GetScriptable<LeaderboardsSO>(Archetypes.GetFixed(Archetypes.Leaderboard)).Elements;
            return elements.FirstOrDefault((x) => x.Stat.idHash == statHash);
        }

        public bool SubmitStatToLeaderboard(string playerId, string statId, float statValue, out int climbedPositions)
        {
            return SubmitStatToLeaderboard(playerId, Animator.StringToHash(statId), statValue, out climbedPositions);
        }

        public bool SubmitStatToLeaderboard(string playerId, int statHash, float statValue, out int climbedPositions)
        {
            var leaderboard = GetLeaderboardByStat(statHash);
            if (leaderboard != null)
                return leaderboard.Submit(playerId, statValue, out climbedPositions);
            else
            {
                climbedPositions = 0;
                return false;
            }
        }

        public void AddLeaderboard(DefaultLeaderboard element)
        {
            var scriptable = GetScriptable<LeaderboardsSO>(Archetypes.GetFixed(Archetypes.Leaderboard));
            scriptable.Add(element);
        }
    }
}