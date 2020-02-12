//This class is auto-generated do not modify
namespace GameFramework
{
	public static class Archetypes
	{
		public const int GameMode = 0;
		public const int World = 1;
		public const int Level = 2;
		public const int Player = 3;
		public const int Camera = 4;
		public const int Character = 5;
		public const int UI = 6;
		public const int Leaderboard = 7;

		public const int Total = 8;

		public static int GetFixed(int constant)
		{
			if (constant <= 3 && constant >= 0)
				return constant + 1;

			if (constant == Leaderboard)
				return 5;

			return -1;
		}
	}
}