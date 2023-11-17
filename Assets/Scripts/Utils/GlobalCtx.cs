using System;

namespace Fun2048
{
	public class GlobalCtx
	{
		public static GlobalCtx Instance;
		private System.Random _random;

        public GlobalCtx(int seed) 
		{
            _random = new System.Random((int)seed);
			Instance = this;
        }

		public System.Random GetRandom()
		{
			return _random;
        }

	}
}