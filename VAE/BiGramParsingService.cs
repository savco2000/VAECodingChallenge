using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VAE
{
    public interface IBiGramParsingService
    {
        string ReadTextFile(string filePath);
        string RemoveNonAlphanumericCharacters(string input);
        string[] TokenizeInput(string input);
        Dictionary<string, List<string>> GetBiGrams(string[] tokens);
        Dictionary<string, int> GetHistogram(Dictionary<string, List<string>> biGrams);
    }

    public class BiGramParsingService : IBiGramParsingService
    {
        public string ReadTextFile(string filePath)
            => File.ReadAllText(filePath);       

        public string RemoveNonAlphanumericCharacters(string input)
            => new string(input.Where(c => (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c))).ToArray());

        public string[] TokenizeInput(string input)
            => input.ToLower()
                .Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);       

        public Dictionary<string, List<string>> GetBiGrams(string[] tokens)
        {
            var dictionary = new Dictionary<string, List<string>>();

            for (int i = 0; i < tokens.Count() - 1; i++)
            {
                var currentWord = tokens[i];
                var nextWord = tokens[i + 1];

                var isCurrentWordAKey = dictionary.TryGetValue(currentWord, out List<string> listOfNextWords);

                if (isCurrentWordAKey)
                    listOfNextWords.Add(nextWord);
                else
                    dictionary.Add(currentWord, new List<string> { nextWord });
            }

            return dictionary;
        }

        public Dictionary<string, int> GetHistogram(Dictionary<string, List<string>> biGrams)
        {        
            return biGrams.SelectMany(kvp => 
            {
                var firstWord = kvp.Key;
                var listOfNextWords = kvp.Value;

                return listOfNextWords.Distinct()
                    .Select(nextWord => 
                    {
                        var nextWordFrequency = listOfNextWords.Count(x => x == nextWord);
                        return new KeyValuePair<string, int>($"{firstWord} {nextWord}", nextWordFrequency);
                    });
            }).ToDictionary(x => x.Key, x => x.Value);            
        }
    }
}
