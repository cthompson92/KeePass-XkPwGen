using System;
using System.IO;
using System.Linq;
using System.Reflection;
using XKPwGen.SharedKernel;

namespace XKPwGen.Options.Dictionaries
{
	internal class FileBasedWordDictionary : IWordDictionary
	{
		private static readonly Assembly _assembly = typeof(FileBasedWordDictionary).Assembly;

		private readonly string _fileName;
		public int WordLength { get; private set; }
		
		public FileBasedWordDictionary(int wordLength, string fileName)
		{
			WordLength = wordLength;
			_fileName = fileName;
		}

		public string[] GetWords()
		{
			var resources = _assembly.GetManifestResourceNames();
			var resourceName = resources.Single(x => x.EndsWith(_fileName));
			using (var stream = _assembly.GetManifestResourceStream(resourceName))
			using (var reader = new StreamReader(stream))
			{
				var result = reader.ReadToEnd();
				return result.Split(new [] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			}
		}
	}
}
