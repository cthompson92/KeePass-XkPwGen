using System.Collections.Generic;
using System.Reflection;
using XKPwGen.SharedKernel;

namespace XKPwGen.Options.Dictionaries
{
	internal class GoogleTop10_000EnglishUsa : ILanguageDictionary
	{
		private const string DataFilePrefix = "google-10000-english-usa-no-swears";
		private static readonly Assembly _assembly = typeof(GoogleTop10_000EnglishUsa).Assembly;

		public WordDictionary Language
		{
			get
			{
				return WordDictionary.English;
			}
		}

		public IDictionary<int, IWordDictionary> WordPages { get; private set; }

		public GoogleTop10_000EnglishUsa()
		{
			WordPages = new Dictionary<int, IWordDictionary>();

			for (var i = 3; i <= 12; i++)
			{
				WordPages.Add(i, new FileBasedWordDictionary(i, GetDataFileName(i)));
			}
		}

		internal static string GetDataFileName(int length)
		{
			return DataFilePrefix + "_" + length + ".dictdata";
		}

		private static readonly GoogleTop10_000EnglishUsa _instance = new GoogleTop10_000EnglishUsa();
		public static GoogleTop10_000EnglishUsa Instance
		{
			get
			{
				return _instance;
			}
		}
	}
}
