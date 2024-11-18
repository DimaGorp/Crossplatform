namespace LabLibrary
{
    public static class LabRunner
    {
        public static void RunLab(string labName, string inputFile = null, string outputFile = null)
        {
            switch (labName.ToLower())
            {
                case "lab1":
                    Lab1.Run(inputFile, outputFile);
                    break;
                case "lab2":
                    Lab2.Run(inputFile, outputFile);
                    break;
                case "lab3":
                    Lab3.Run(inputFile, outputFile);
                    break;
                default:
                    Console.WriteLine("Unknown lab. Please choose lab1, lab2, or lab3.");
                    break;
            }
        }
    }
}
