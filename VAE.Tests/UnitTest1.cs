using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace VAE.Tests
{
    public class UnitTest1
    {
        [Fact(DisplayName = "BiGramParsingService should remove non alphanumeric characters from string")]
        public void BiGramParsingService_should_remove_non_alphanumeric_characters_from_string()
        {
            var input = ".`~!@#$%^&*()_-+=The quick brown fox and.`~!@#$%^&*()_-+= the quick blue hare.`~!@#$%^&*()_-+=";

            var expectedResults = "The quick brown fox and the quick blue hare";

            var sut = new BiGramParsingService();

            var actualResults = sut.RemoveNonAlphanumericCharacters(input);

            Assert.Equal(expectedResults, actualResults);
        }

        [Fact(DisplayName = "BiGramParsingService should tokenize string correctly")]
        public void BiGramParsingService_should_tokenize_string_correctly()
        {
            var input = "The quick brown fox and the quick blue hare";
            
            var expectedResults = new string[] 
            {
                "the",
                "quick",
                "brown",
                "fox",
                "and",
                "the",
                "quick",
                "blue",
                "hare"
            };

            var sut = new BiGramParsingService();

            var actualResults = sut.TokenizeInput(input);            

            var isEqual = expectedResults.SequenceEqual(actualResults);

            Assert.True(isEqual);
        }

        [Fact(DisplayName = "BiGramParsingService should return correct bigrams")]
        public void BiGramParsingService_should_return_correct_bigrams()
        {
            var tokens = new string[]
            {
                "the",
                "quick",
                "brown",
                "fox",
                "and",
                "the",
                "quick",
                "blue",
                "hare"
            };

            var expectedResults = new Dictionary<string, List<string>>
            {
                {"the", new List<string>{"quick", "quick"} },
                {"quick", new List<string>{"brown", "blue"} },
                {"brown", new List<string>{"fox"} },
                {"fox", new List<string>{"and"} },
                {"and", new List<string>{"the"} },
                {"blue", new List<string>{"hare"} }
            };

            var sut = new BiGramParsingService();

            var actualResults = sut.GetBiGrams(tokens);            

            var isKeysEqual = actualResults.Keys.SequenceEqual(expectedResults.Keys); 
            
            var isValuesEqual = actualResults.Keys
                .All(key => expectedResults.ContainsKey(key) && actualResults[key].SequenceEqual(expectedResults[key]));

            Assert.True(isKeysEqual);
            Assert.True(isValuesEqual);
        }

        [Fact(DisplayName = "BiGramParsingService should return correct histogram")]
        public void BiGramParsingService_should_return_correct_histogram()
        {
            var biGrams = new Dictionary<string, List<string>>
            {
                {"the", new List<string>{"quick", "quick"} },
                {"quick", new List<string>{"brown", "blue"} },
                {"brown", new List<string>{"fox"} },
                {"fox", new List<string>{"and"} },
                {"and", new List<string>{"the"} },
                {"blue", new List<string>{"hare"} }
            };

            var expectedResults = new Dictionary<string, int>
            {
                { "the quick", 2 },
                { "quick brown", 1 },
                { "brown fox", 1 },
                { "fox and", 1 },
                { "and the", 1 },
                { "quick blue", 1 },
                { "blue hare", 1 }
            };

            var sut = new BiGramParsingService();

            var actualResults = sut.GetHistogram(biGrams);

            var isEqual = actualResults.OrderBy(kvp => kvp.Key)
                .SequenceEqual(expectedResults.OrderBy(kvp => kvp.Key));

            Assert.True(isEqual);
        }
    }    
}
