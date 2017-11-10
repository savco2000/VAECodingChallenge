using System;
using System.Collections.Generic;

namespace VAE
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = GetUserSuppliedFilePath();

            while (string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine("You did not enter a valid filePath.");
                filePath = GetUserSuppliedFilePath();
            }                

            try
            {
                var service = new BiGramParsingService();

                var rawText = service.ReadTextFile(filePath);

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
            Console.WriteLine("Press any key to exit . . .");
            Console.ReadLine();
        }

        private static void PrintHistogram(Dictionary<string, int> histogram)
        {
            if(histogram.Count == 0)
            {
                Console.WriteLine("No bigrams were found in the text.");
                return;
            }

            Console.WriteLine("The histogram of the bigrams in the text file are:");

            foreach (var entry in histogram)
            {
                Console.WriteLine($"\"{entry.Key}\" {entry.Value}");
            }
        }

        private static string GetUserSuppliedFilePath()
        {
            Console.WriteLine("Please enter the fully qualified path for the input text file e.g. \"C:\\Projects\\VAE\\VAE\\Input.txt\"");
            var filePath = Console.ReadLine();
            Console.WriteLine("");
            return filePath;
        }
    }
}
