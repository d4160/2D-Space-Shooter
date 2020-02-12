// This class is auto-generated do not modify
namespace GameFramework
{
	public static class Layers
	{
		public const int Default = 0;
		public const int TransparentFX = 1;
		public const int IgnoreRaycast = 2;
		public const int Water = 4;
		public const int UI = 5;
		public const int PostProcessing = 8;


		public static int onlyIncluding( params int[] layers )
		{
			int mask = 0;
			for( var i = 0; i < layers.Length; i++ )
				mask |= ( 1 << layers[i] );

			return mask;
		}


		public static int everythingBut( params int[] layers )
		{
			return ~onlyIncluding( layers );
		}
	}
}