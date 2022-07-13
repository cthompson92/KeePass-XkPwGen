using System.Collections.Generic;
using KeePassLib.Cryptography;

namespace XKPwGen.SharedKernel
{
    public static class GetRandomWords
    {
	    private static string GetWordOfLengthFromDictionary(
		    ILanguageDictionary dictionary, int length, CryptoRandomStream crsRandomSource)
        {
            var wordsOfLength = dictionary.WordPages[length].GetWords();

            var randomIndex = crsRandomSource.NextRandomIndex(wordsOfLength);

            return wordsOfLength[randomIndex];
        }

        public static IEnumerable<string> GetWords(
	        int numberOfWords, int minLength, int maxLength, CryptoRandomStream crsRandomSource, ILanguageDictionary dictionary)
        {
			// need to randomly determine the length of each word
			// so we can generate a random word of the correct length
	        var wordLengths = GetWordLengths(numberOfWords, minLength, maxLength, crsRandomSource);
	        foreach (var wl in wordLengths)
            {
                yield return GetWordOfLengthFromDictionary(dictionary, (int)wl, crsRandomSource);
            }
        }

        private static ulong[] GetWordLengths(
	        int numberOfWords, int minLength, int maxLength, CryptoRandomStream crsRandomSource)
        {
	        var wordLengths = new ulong[numberOfWords];
	        if (minLength == maxLength)
	        {
		        //prevent generating random values when it will always be the same thing (NextRandom throws when the range has no variance)
		        for (var i = 0; i < wordLengths.Length; i++)
		        {
			        wordLengths[i] = (ulong)minLength;
		        }
	        }
	        else
	        {
		        for (var i = 0; i < numberOfWords; i++) wordLengths[i] = crsRandomSource.NextRandom(minLength, maxLength);
	        }

	        return wordLengths;
        }
    }
}
