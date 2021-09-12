using System.Collections.Generic;
using System.IO;
using KeePassLib.Cryptography;

namespace XKPwGen.SharedKernel
{
    public static class GetRandomWords
    {
        public static string GetDataFileName(WordDictionary dictionary, int length)
        {
            return Path.GetFullPath(
                OptionsManager.GetAppDataPathRoot()
              + string.Format("/{0}/words_alpha_{1}.txt", dictionary, length));
        }

        private static string GetWordOfLengthFromDictionary(WordDictionary dictionary, int length, CryptoRandomStream crsRandomSource)
        {
            var wordsOfLength = File.ReadAllLines(GetDataFileName(dictionary, length));

            var randomIndex = crsRandomSource.NextRandomIndex(wordsOfLength);

            return wordsOfLength[randomIndex];
        }

        public static IEnumerable<string> GetWords(int numberOfWords, int minLength, int maxLength, CryptoRandomStream crsRandomSource)
        {
            var dictionary = WordDictionary.English;

            var wordLengths = new ulong[numberOfWords];
            for (var i = 0; i < numberOfWords; i++) wordLengths[i] = crsRandomSource.NextRandom(minLength, maxLength);

            foreach (var wl in wordLengths)
            {
                yield return GetWordOfLengthFromDictionary(dictionary, (int)wl, crsRandomSource);
            }
        }
    }
}