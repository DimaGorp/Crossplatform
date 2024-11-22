using System;
using System.IO;
namespace Lab1Main{
    public class Program
    {
        public static void Main(){
            Run("INPUT.txt","OUTPUT.txt");
        }
        public static void Run(string inputFile, string outputFile)
        {
            try
            {
                Console.WriteLine($"Looking for INPUT file: {inputFile}");
                Console.WriteLine($"Looking for OUTPUT file: {outputFile}");

                if (!File.Exists(inputFile))
                {
                    Console.WriteLine($"Error: The file {inputFile} was not found.");
                    return;
                }

                string input = File.ReadAllText(inputFile);

                if (long.TryParse(input, out long n))
                {
                    // Check range 1 ≤ N ≤ 10^5
                    if (n < 1 || n > 100000)
                    {
                        Console.WriteLine("Error: The value of N must be in the range from 1 to 100000.");
                        return;
                    }

                    // Calculate the result
                    long result = n * (n + 2) * (n * 2 + 1) / 8;
                    File.WriteAllText(outputFile, result.ToString());

                    Console.WriteLine("The result was successfully written to OUTPUT.txt");
                }
                else
                {
                    Console.WriteLine("Error: Invalid data in INPUT.txt.");
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error: No access to the file. Please check the file permissions.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"I/O Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}