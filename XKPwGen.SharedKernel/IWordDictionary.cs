namespace XKPwGen.SharedKernel
{
	public interface IWordDictionary
	{
		int WordLength { get; }

		string[] GetWords();
	}
}
