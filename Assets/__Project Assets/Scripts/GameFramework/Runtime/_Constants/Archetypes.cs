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
		public const int Laser = 8;
		public const int Enemy = 9;
		public const int PowerUp = 10;

		public const int Total = 11;

        internal static int GetFixed(int entity)
        {
            if (entity >= 0 && entity < 4)
            {
                return entity + 1;
            }

            if (entity == 7)
                return 5;

            return 0;
        }
	}
}