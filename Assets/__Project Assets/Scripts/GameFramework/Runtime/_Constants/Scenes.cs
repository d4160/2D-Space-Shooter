//This class is auto-generated do not modify
namespace GameFramework
{
	public static class Scenes
	{
		public const string BootLoader2D = "BootLoader - 2D";
		public const string LoadingScreen1 = "LoadingScreen 1";
		public const string Menu1 = "Menu 1";
		public const string Credits1 = "Credits 1";
		public const string World1MainScene = "World 1 - MainScene";
		public const string World1Zone0 = "World 1 - Zone 0";
		public const string Mode1Cinematics1 = "Mode 1 - Cinematics 1";
		public const string Mode1Cinematics2 = "Mode 1 - Cinematics 2";
		public const string Mode1Play1 = "Mode 1 - Play 1";
		public const string Mode1Play2 = "Mode 1 - Play 2";
		public const string BootLoader3D = "BootLoader - 3D";

		public const int Total = 11;


		public static int nextSceneIndex()
		{
			var currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
			if( currentSceneIndex + 1 == Total )
				return 0;
			return currentSceneIndex + 1;
		}
	}
}