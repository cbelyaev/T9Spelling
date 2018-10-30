using System;
using System.Collections.Generic;
using System.IO;

namespace T9Spelling
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: T9Spelling <input file> <output file>");
                return;
            }

            // load input
            string[] input;
            try
            {
                input = LoadInput(args[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading input: {ex.Message}");
                return;
            }

            // spell
            var output = new string[input.Length];
            try
            {
                var speller = new Speller();
                output = speller.Spell(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error spelling: {ex.Message}");
            }

            // save output
            try
            {
                SaveOutput(args[1], output);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving output: {ex.Message}");
            }

            Console.WriteLine($"{output.Length} case(s) spelled successfully");
        }

        private static string[] LoadInput(string filename)
        {
            using (var reader = new StreamReader(filename))
            {
                var line = reader.ReadLine();
                if (!int.TryParse(line, out var numberOfCases) || numberOfCases < 1)
                    throw new ApplicationException("Invalid number of cases");

                var result = new List<string>(numberOfCases);
                while (result.Count < numberOfCases && null != (line = reader.ReadLine()))
                    result.Add(line);

                if (result.Count < numberOfCases)
                        throw new ApplicationException("Invalid input file");
                    return result.ToArray();
            }
        }

        private static void SaveOutput(string filename, string[] output)
        {
            using (var writer = new StreamWriter(filename))
            {
                for(var i = 0; i < output.Length; i++)
                writer.WriteLine($"Case #{i+1}: {output[i]}");
            }
        }
    }
}