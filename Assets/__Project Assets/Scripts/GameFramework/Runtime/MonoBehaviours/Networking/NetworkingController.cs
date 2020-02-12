namespace GameFramework
{
    using d4160.Core;
    using d4160.Networking;

    public class NetworkingController : DefaultNetworkingController
    {
        public override void CreateNetworkingLauncher()
        {
            switch(m_networkingType)
            {
                case NetworkingType.PhotonUnityNetworking:
                    m_networkingLauncher = this.GetComponent<PUN2Launcher>(true, true);
                break;
            }
        }
    }
}