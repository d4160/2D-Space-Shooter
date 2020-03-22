using System;
using d4160.GameFramework;
using UltEvents;

namespace GameFramework
{
	using Photon.Pun;
	using Photon.Realtime;
	using d4160.Networking;
	using UnityEngine;
	using d4160.UI.Progress;

#pragma warning disable 649

	/// <summary>
	/// Launch manager. Connect, join a random room or create one if none or all full.
	/// </summary>
	public class PUN2Launcher : MonoBehaviourPunCallbacks, INetworkingLauncher
    {
        [SerializeField] protected UltEvent _onConnectedToMaster;
        [SerializeField] protected UltEvent _onJoinedRoom;
		[SerializeField] protected UltEvent _onDisconnected;

		#region Protected Fields
		protected byte m_maxPlayersPerRoom;
		/// <summary>
		/// Keep track of the current process. Since connection is asynchronous and is based on several callbacks from Photon,
		/// we need to keep track of this to properly adjust the behavior when we receive call back by Photon.
		/// Typically this is used for the OnConnectedToMaster() callback.
		/// </summary>
		protected bool m_isConnecting;
		#endregion

		public Action OnConnected { get; set; }

		#region MonoBehaviour CallBacks

		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity during early initialization phase.
		/// </summary>
		public virtual void Awake()
		{
			Debug.Log("PUN2Launcher:Awake");

			// #Critical
			// this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
			PhotonNetwork.AutomaticallySyncScene = true;
		}

		#endregion

		#region Public Methods
		/// <summary>
		/// Start the connection process.
		/// - If already connected, we attempt joining a random room
		/// - if not yet connected, Connect this application instance to Photon Cloud Network
		/// </summary>
		public virtual void Connect(byte maxPlayersPerRoom, string version)
		{
			if (ProgressPrefabsManagerBase.Instanced)
                ProgressPrefabsManagerBase.Instance.InstancedMain?.StartLoop();

			m_maxPlayersPerRoom = maxPlayersPerRoom;

			// keep track of the will to join a room, because when we come back from the game we will get a callback that we are connected, so we need to know what to do then
			m_isConnecting = true;

			// we check if we are connected or not, we join if we are , else we initiate the connection to the server.
			if (PhotonNetwork.IsConnected)
			{
                m_isConnecting = false;

				LogFeedback("Joining Room...", 1.5f);
				// #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
				PhotonNetwork.JoinRandomRoom();
			}
			else
			{

				LogFeedback("Connecting...", 1.5f);

				// #Critical, we must first and foremost connect to Photon Online Server.
				PhotonNetwork.GameVersion = version;
				PhotonNetwork.ConnectUsingSettings();
			}
		}

		/// <summary>
		/// Logs the feedback in the UI view for the player, as opposed to inside the Unity Editor for the developer.
		/// </summary>
		/// <param name="message">Message.</param>
		protected void LogFeedback(string message, float duration)
		{
			if (NotificationPrefabsManager.Instance.InstancedMain)
				NotificationPrefabsManager.Instance.InstancedMain.Notify(message, duration);

			Debug.Log(message);
		}

		#endregion

		#region MonoBehaviourPunCallbacks CallBacks
		// below, we implement some callbacks of PUN
		// you can find PUN's callbacks in the class MonoBehaviourPunCallbacks

		/// <summary>
		/// Called after the connection to the master is established and authenticated
		/// </summary>
		public override void OnConnectedToMaster()
		{
			//if (IgnoreCallbacks) return;

			// we don't want to do anything if we are not attempting to join a room.
			// this case where isConnecting is false is typically when you lost or quit the game, when this level is loaded, OnConnectedToMaster will be called, in that case
			// we don't want to do anything.
			if (m_isConnecting)
			{
				LogFeedback("OnConnectedToMaster: Next -> try to Join Random Room", 1.5f);
				// #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
                PhotonNetwork.JoinOrCreateRoom("Global", new RoomOptions() {MaxPlayers = 20}, null);
                
                m_isConnecting = false;
			}

            _onConnectedToMaster?.Invoke();
            OnConnected?.Invoke();
		}

		/// <summary>
		/// Called when a JoinRandom() call failed. The parameter provides ErrorCode and message.
		/// </summary>
		/// <remarks>
		/// Most likely all rooms are full or no rooms are available. <br/>
		/// </remarks>
		public override void OnJoinRandomFailed(short returnCode, string message)
		{
			//if (IgnoreCallbacks) return;

			LogFeedback("<Color=Red>OnJoinRandomFailed</Color>: Next -> Create a new Room", 1.5f);

			// #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
			PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = this.m_maxPlayersPerRoom});
		}


		/// <summary>
		/// Called after disconnecting from the Photon server.
		/// </summary>
		public override void OnDisconnected(DisconnectCause cause)
		{
			LogFeedback("<Color=Red>OnDisconnected</Color> "+cause, 1.5f);

			// #Critical: we failed to connect or got disconnected. There is not much we can do. Typically, a UI system should be in place to let the user attemp to connect again.
            if (ProgressPrefabsManagerBase.Instanced)
            {
                ProgressPrefabsManagerBase.Instance.InstancedMain?.StopLoop();
			}

			m_isConnecting = false;

            _onDisconnected?.Invoke();
		}

		/// <summary>
		/// Called when entering a room (by creating or joining it). Called on all clients (including the Master Client).
		/// </summary>
		/// <remarks>
		/// This method is commonly used to instantiate player characters.
		/// If a match has to be started "actively", you can call an [PunRPC](@ref PhotonView.RPC) triggered by a user's button-press or a timer.
		///
		/// When this is called, you can usually already access the existing players in the room via PhotonNetwork.PlayerList.
		/// Also, all custom properties should be already available as Room.customProperties. Check Room..PlayerCount to find out if
		/// enough players are in the room to start playing.
		/// </remarks>
		public override void OnJoinedRoom()
		{
			//if (IgnoreCallbacks) return;

			LogFeedback("<Color=Green>OnJoinedRoom</Color> with "+PhotonNetwork.CurrentRoom.PlayerCount+" Player(s)", 1.5f);

			if(ProgressPrefabsManagerBase.Instanced)
			    ProgressPrefabsManagerBase.Instance.InstancedMain?.StopLoop();

			// #Critical: We only load if we are the first player, else we rely on  PhotonNetwork.AutomaticallySyncScene to sync our instance scene.
			if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
			{
				Debug.Log("Loading scene for 1 Player");
				// #Critical
				d4160.GameFramework.GameManager.Instance.UnloadAllStartedLevels(
					() =>
                    {
                        Debug.Log(($"After UnloadAllStartedLevels"));
						d4160.GameFramework.GameManager.Instance.LoadLevel(d4160.GameFramework.LevelType.GameMode, 1);
                    });
			}
            else
            {
                Debug.Log("Loading scene for more than 1 Player");
				d4160.GameFramework.GameManager.Instance.UnloadLevel(LevelType.General, 1);
            }

            _onJoinedRoom?.Invoke();
		}
		#endregion
	}
}