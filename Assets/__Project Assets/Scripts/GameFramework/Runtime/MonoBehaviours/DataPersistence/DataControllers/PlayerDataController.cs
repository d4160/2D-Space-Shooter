namespace GameFramework
{
    using d4160.GameFramework;
    using UnityEngine;

    public class PlayerDataController : DataControllerBase
    {
        public override T GetScriptable<T>(int dataIdx = 0)
        {
            var database = GameFrameworkSettings.PlayerDatabase;
            var scriptable = database.PlayerData[dataIdx];

            return scriptable as T;
        }

        public DefaultPlayTrial[] GetPlayTrials()
        {
            var scriptable = GetScriptable<PlayTrialsSO>(0);
            return scriptable.Elements.ToArray();
        }

        public void AddPlayTrial(DefaultPlayTrial playTrial)
        {
            var scriptable = GetScriptable<PlayTrialsSO>(0);
            scriptable.Add(playTrial);
        }
    }
}