namespace GameFramework.DataPersistence
{
    using d4160.GameFramework;
    using d4160.DataPersistence;
    using System;
    using System.Linq;

    public class LocalAuthenticator : DefaultLocalAuthenticator
    {
        public LocalAuthenticator(string username,
            Action resultCallback,
            Action errorCallback) : base(username, resultCallback,  errorCallback)
        {
        }

        public override void Login()
        {
            var playersSO = GameFrameworkSettings.GameDatabase.GetGameData<PlayersSO>(Archetypes.GetFixed(Archetypes.Player));
            var player = playersSO.Elements.FirstOrDefault((x) => x.Name == m_username);

            if (player == null)
            {
                player = new DefaultPlayer()
                {
                    ID = playersSO.ElementsCount + 1,
                    Name = m_username
                };
                playersSO.Elements.Add(player);
            }

            m_id = player.ID.ToString();

            m_resultCallback?.Invoke();
        }
    }
}