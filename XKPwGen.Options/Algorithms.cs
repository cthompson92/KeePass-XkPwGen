using XKPwGen.Options.Dictionaries;
using XKPwGen.SharedKernel;

namespace XKPwGen.Options
{
	public class Algorithms
	{
		private static readonly Algorithm _google = new Algorithm(new GoogleTop10_000EnglishUsa());
		public static Algorithm Google
		{
			get
			{
				return _google;
			}
		}

		private static readonly Algorithm _wordsAlpha = new Algorithm(new WordsAlpha());
		public static Algorithm WordsAlpha
		{
			get
			{
				return _wordsAlpha;
			}
		}
	}
}
