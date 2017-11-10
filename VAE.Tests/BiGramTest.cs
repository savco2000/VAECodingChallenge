using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace VAE.Tests
{
    public class BiGramTest
    {
        [Fact]
        public void Test1()
        {
            var words = new List<string>()
            {
                "The",
                "quick",
                "brown",
                "fox",
                "and",
                "the",
                "quick",
                "blue",
                "hare"
            };

            var expectedResults = new Dictionary<string, int>
            {
                { "the quick", 2 },
                { "quick brown", 2 },
                { "brown fox", 2 },
                { "fox and", 2 },
                { "and the", 2 },
                { "quick blue", 2 },
                { "blue hare", 2 }
            };

            var sut = new BiGramService();

            var actualResults = sut.GetHistogram(words);

            var isEqual = actualResults.OrderBy(kvp => kvp.Key)
                .SequenceEqual(expectedResults.OrderBy(kvp => kvp.Key));

            Assert.True(isEqual);
        }
    }

    public interface IBiGramService
    {
        Dictionary<string, int> GetHistogram(List<string> words);
    }

    public class BiGramService : IBiGramService
    {
        public Dictionary<string, int> GetHistogram(List<string> words)
        {
            throw new NotImplementedException();
        }
    }
}
