using System;
using System.IO;

namespace Lab2{
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

                if (long.TryParse(input, out long k))
                {
                    if (k < 1 || k > 100000)
                    {
                        Console.WriteLine("Error: The value of N must be in the range from 1 to 100000.");
                        return;
                    }

                    int result = CalculateResult(k); // Call the core logic method

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

        public static int CalculateResult(long k)
        {
            if (k <= 0)
            {
                throw new IndexOutOfRangeException("Input cannot be negative or zero.");
            }
            const int MOD = 1000000;
            int[] dp = new int[k + 1];
            dp[0] = 1;
            
            for (int h = 1; h <= k; h++)
            {
                if (h >= 10)
                {
                    dp[h] += dp[h - 10];
                }
                if (h >= 11)
                {
                    dp[h] += dp[h - 11];
                }
                if (h >= 12)
                {
                    dp[h] += dp[h - 12];
                }
                dp[h] %= MOD; // Keep values within bounds
            }

            return (dp[k] * 2) % MOD;
        }
    }
}