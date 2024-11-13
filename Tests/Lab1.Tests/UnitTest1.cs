using NUnit.Framework;
using System;

public class ProgramTests
{
    [Test]
    public void Test_CalculateResult_ValidInput()
    {
        // Arrange: valid input
        string input = "4"; // Test with N = 4
        string expected = "27"; // Expected result for N = 4

        // Act: calculate the result
        string result = CalculateResult(input);

        // Print the result to the console
        Console.WriteLine($"Input: {input}, Expected: {expected}, Result: {result}");

        // Assert: check if the result matches the expected value
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Test_CalculateResult_InputOutOfRange()
    {
        // Arrange: invalid input (out of range)
        string input = "100001"; // Out of range N value

        // Act: calculate the result
        string result = CalculateResult(input);

        // Print the result to the console
        Console.WriteLine($"Input: {input}, Result: {result}");

        // Assert: result should be null for out-of-range input
        Assert.IsNull(result);
    }

    [Test]
    public void Test_CalculateResult_InvalidInput()
    {
        // Arrange: invalid input (non-numeric)
        string input = "abc";

        // Act: calculate the result
        string result = CalculateResult(input);

        // Print the result to the console
        Console.WriteLine($"Input: {input}, Result: {result}");

        // Assert: result should be null for non-numeric input
        Assert.IsNull(result);
    }

    // A simplified version of the method to test the result calculation directly
    private string CalculateResult(string input)
    {
        if (long.TryParse(input, out long n))
        {
            // Check if the input is in the valid range
            if (n < 1 || n > 100000)
            {
                return null;
            }
            // Calculate the result
            long result = n * (n + 2) * (n * 2 + 1) / 8;
            return result.ToString();
        }
        return null;
    }
}
