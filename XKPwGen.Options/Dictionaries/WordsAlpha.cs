using System.Collections.Generic;
using System.Reflection;
using XKPwGen.SharedKernel;

namespace XKPwGen.Options.Dictionaries
{
	public class WordsAlpha : ILanguageDictionary
	{
		private const string DataFilePrefix = "words_alpha";
		private static readonly Assembly _assembly = typeof(WordsAlpha).Assembly;

		public WordDictionary Language
		{
			get
			{
				return WordDictionary.English;
			}
		}

		public IDictionary<int, IWordDictionary> WordPages { get; private set; }

		public WordsAlpha()
		{
			WordPages = new Dictionary<int, IWordDictionary>();

			for (var i = 3; i <= 12; i++)
			{
				WordPages.Add(i, new FileBasedWordDictionary(i, GetDataFileName(i)));
			}
		}

		internal static string GetDataFileName(int length)
		{
			return DataFilePrefix + "_" + length + ".txt";
		}

		private static readonly WordsAlpha _instance = new WordsAlpha();
		public static WordsAlpha Instance
		{
			get
			{
				return _instance;
			}
		}
	}
}
