namespace GameFramework
{
    using Photon.Pun;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

	/// <summary>
	/// Player name input field. Let the user input his name, will appear above the player in the game.
	/// </summary>
	[RequireComponent(typeof(TMP_InputField))]
	public class PlayerNameInputField : MonoBehaviour
	{
		#region Private Constants

		// Store the PlayerPref Key to avoid typos
		const string playerNamePrefKey = "PlayerName";

		#endregion

		#region MonoBehaviour CallBacks

		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity during initialization phase.
		/// </summary>
		void Awake () {

			string defaultName = string.Empty;
			TMP_InputField _inputField = this.GetComponent<TMP_InputField>();

			if (_inputField!=null)
			{
				if (PlayerPrefs.HasKey(playerNamePrefKey))
				{
					defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                }
				else
				{
					defaultName = System.Guid.NewGuid().ToString().Substring(0, 25);
                }

                _inputField.text = defaultName;
			}

			// As is the same authenticator for everything
			DataManager.Instance.GameData.Authenticator.Username = defaultName;
			NetworkingManager.Instance.Chat.Username = defaultName;
			PhotonNetwork.NickName = defaultName;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Sets the name of the player, and save it in the PlayerPrefs for future sessions.
		/// </summary>
		/// <param name="value">The name of the Player</param>
		public void SetPlayerName(string value)
		{
			// #Important
			if (string.IsNullOrEmpty(value))
			{
							Debug.LogError("Player Name is null or empty");
					return;
			}
			PhotonNetwork.NickName = value;
			DataManager.Instance.GameData.Authenticator.Username = value;
			NetworkingManager.Instance.Chat.Username = value;

			PlayerPrefs.SetString(playerNamePrefKey, value);
		}

		#endregion
	}
}
