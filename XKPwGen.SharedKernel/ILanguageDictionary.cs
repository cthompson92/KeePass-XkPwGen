using System.Collections.Generic;

namespace XKPwGen.SharedKernel
{
	public interface ILanguageDictionary
	{
		WordDictionary Language { get; }

		IDictionary<int, IWordDictionary> WordPages { get; }
	}
}