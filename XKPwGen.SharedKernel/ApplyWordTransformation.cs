using System;
using System.Collections.Generic;
using System.Linq;
using XKPwGen;
using XKPwGen.SharedKernel;

namespace XkPwGen
{
    public static class ApplyWordTransformation
    {
        public static IEnumerable<string> Transform(this IEnumerable<string> words, TransformationOptions options)
        {
            return Transform(options, words);
        }

        public static IEnumerable<string> Transform(TransformationOptions options, IEnumerable<string> words)
        {
            switch (options.CaseTransformation)
            {
                case CaseTransformationType.None:
                    return None(words);

                case CaseTransformationType.AlternatingWordCase:
                    return AlternatingWordCase(words, true);

                case CaseTransformationType.LowerCase:
                    return words.Select(x => x.ToLowerInvariant());

                case CaseTransformationType.UpperCase:
                    return words.Select(x => x.ToUpperInvariant());
                    
            }

            throw new ArgumentException("Unknown transformation type specified.", "options");
        }

        private static IEnumerable<string> None(IEnumerable<string> words)
        {
            return words;
        }

        private static IEnumerable<string> AlternatingWordCase(IEnumerable<string> words, bool startLowerCase)
        {
            var useLowerCase = startLowerCase;
            foreach (var word in words)
            {
                if (useLowerCase)
                {
                    yield return word.ToLowerInvariant();
                }
                else
                {
                    yield return word.ToUpperInvariant();
                }

                useLowerCase = !useLowerCase;
            }
        }
    }
}