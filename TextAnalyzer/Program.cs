using System;
using System.Diagnostics;

namespace CrossInformTextAnalyzer
{
    internal class Program
    {
        const int SUBSTRING_LENGTH = 3;
        const int SUBSTRINGS_COUNT= 10;
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к файлу, текст которого будет обрабатываться:");
            var filePath = Console.ReadLine();

            var analyzer = new TextAnalyzer();

            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var mostCommonSubstrings = analyzer.GetTheMostCommonWordsSubstringsFromFile(filePath,
                    SUBSTRING_LENGTH,
                    SUBSTRINGS_COUNT);

                stopwatch.Stop();

                Console.WriteLine("\nСамые частые подстроки слов:");
                Console.WriteLine(String.Join("\n", mostCommonSubstrings));

                Console.WriteLine($"\nВремя работы программы: {stopwatch.ElapsedMilliseconds} ms");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
