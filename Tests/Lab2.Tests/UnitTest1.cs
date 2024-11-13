using Lab2;
namespace Lab2.Tests;

[TestFixture]
public class Tests
{
    private const int ModValue = 1000000; // Example of shared setup data (constant for modulus)
    private long input;
    private int expectedOutput;

    // This runs before each test
    [SetUp]
    public void SetUp()
    {
        // Reset input and expected output for each test
        input = 0;
        expectedOutput = 0;
    }
     [TestCase(10, 2)]       // Small value divisible by 10
    [TestCase(11, 2)]       // Small value divisible by 11
    [TestCase(12, 2)]  
    [TestCase(22, 6)]      
    [TestCase(32, 12)]      
    [TestCase(1, 0)] 
    public void CalculateResult_ValidInput_ReturnsExpectedResult(long input, int expectedOutput)
    {
        // Arrange: Set the input for the current test case
        this.input = input;
        this.expectedOutput = expectedOutput;

        // Act: Call the function
        int result = Program.CalculateResult(input);

        // Assert: Verify the result
        Assert.AreEqual(expectedOutput, result);
    }

    [TestCase(-1)]          // Negative input
    [TestCase(-10)]  
    [TestCase(-100000000000000)]   // Another negative input
    [TestCase(0)]         
    public void CalculateResult_NegativeOrZeroInput_ThrowsException(long input)
    {
        // Arrange: Set the input for the current test case
        this.input = input;

        // Act & Assert: Check for expected exception
        Assert.Throws<IndexOutOfRangeException>(() => Program.CalculateResult(input));
    }
}