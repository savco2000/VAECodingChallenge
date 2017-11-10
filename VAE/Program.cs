using System;
using System.Collections.Generic;

namespace VAE
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var service = new BiGramParsingService();

                var rawText = service.ReadTextFile("Input.txt");

                var cleanText = service.RemoveNonAlphanumericCharacters(rawText);

                var tokens = service.TokenizeInput(cleanText);

                var biGrams = service.GetBiGrams(tokens);

                var histogram = service.GetHistogram(biGrams);

                PrintHistogram(histogram);
            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }

            Console.WriteLine("");
            Console.WriteLine("Press any key to continue . . .");
            Console.ReadLine();
        }

        private static void PrintHistogram(Dictionary<string, int> histogram)
        {
            if(histogram.Count == 0)
            {
                Console.WriteLine("No bigrams were found in the text.");
                return;
            }

            foreach (var entry in histogram)
            {
                Console.WriteLine($"\"{entry.Key}\" {entry.Value}");
            }
        }
    }
}
