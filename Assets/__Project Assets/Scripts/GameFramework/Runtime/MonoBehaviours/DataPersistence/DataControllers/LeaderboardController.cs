namespace GameFramework
{
    using System.Collections.Generic;
    using d4160.DataPersistence;
#if PLAYFAB
    using PlayFab;
    using PlayFab.ClientModels;
#endif
    using UnityEngine;

    public class LeaderboardController : DefaultLeaderboardController
    {
        public override bool SubmitStat(string statDefinitionId, int value)
        {
            int placeholder;
            return SubmitStat(statDefinitionId, value, out placeholder);
        }

        public override bool SubmitStat(string statDefinitionId, int value, out int climbedPositions)
        {
            if(!m_authenticator)
            {
                climbedPositions = 0;
                return false;
            }

            if (m_authenticator.AuthenticationType == AuthenticationType.Local)
            {
                bool newRecord = DataManager.Instance.GameData.SubmitStatToLeaderboard(m_authenticator.AuthenticationId, statDefinitionId, value, out climbedPositions);

                Debug.Log($"New record: {newRecord} - {statDefinitionId}: {value}. Climbed: {climbedPositions} positions");
                if (newRecord)
                    DataManager.Instance.GameFoundationData.SetMainItemIntStat(PlayerStatsItem.id, statDefinitionId, value);
            }
            else
            {
                climbedPositions = 0;

                switch(m_authenticator.RemotePersistenceType)
                {
                    case RemotePersistenceType.PlayFab:
#if PLAYFAB
                        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest {
                            Statistics = new List<StatisticUpdate> {
                                new StatisticUpdate {
                                    StatisticName = statDefinitionId,
                                    Value = value
                                }
                            }
                        }, result => OnStatisticsUpdated(result), FailureCallback);
#endif
                    break;
                }
            }

            return false;
        }

#if PLAYFAB
        private void OnStatisticsUpdated(UpdatePlayerStatisticsResult updateResult) {
            Debug.Log("Successfully submitted high score");
        }

        private void FailureCallback(PlayFabError error){
            Debug.LogWarning("Something went wrong with your API call. Here's some debug information:");
            Debug.LogError(error.GenerateErrorReport());
        }
#endif

        public override bool SubmitStat(int statDefinitionId, int value, out int climbedPositions)
        {
            climbedPositions = 0;
            return false;
        }

        public override bool SubmitStat(int statDefinitionHash, int value)
        {
            return false;
        }
    }
}