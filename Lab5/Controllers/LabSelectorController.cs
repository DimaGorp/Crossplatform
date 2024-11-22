using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using LabLibrary;

namespace WebApp.Controllers;

public class LabSelectorController : Controller
{

    public LabSelectorController()
    {
    }

    public IActionResult Lab1()
    {
        return View();
    }
    public IActionResult Lab2()
    {
        return View();
    }
    public IActionResult Lab3()
    {
        return View();
    }

    [HttpPost]
    public IActionResult RunLab(string labName, IFormFile inputFile, IFormFile outputFile)
    {
        if (inputFile == null || outputFile == null)
        {
            ViewBag.ErrorMessage = "Please select both input and output files.";
            return View(labName); // Return the respective view for the lab
        }

        // Define the server directory where uploaded files will be stored
        var serverDirectory = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

        // Ensure the directory exists or create it
        if (!Directory.Exists(serverDirectory))
        {
            Directory.CreateDirectory(serverDirectory);
        }

        // Generate full file paths to save the files on the server
        var inputFilePath = Path.Combine(serverDirectory, inputFile.FileName);
        var outputFilePath = Path.Combine(serverDirectory, outputFile.FileName);

        try
        {
            // Save the input file to the server
            using (var inputStream = new FileStream(inputFilePath, FileMode.Create))
            {
                inputFile.CopyTo(inputStream);
            }

            // Save the output file to the server
            using (var outputStream = new FileStream(outputFilePath, FileMode.Create))
            {
                outputFile.CopyTo(outputStream);
            }

            // Run the lab with the saved file paths (assuming LabRunner is implemented correctly)
            LabRunner.RunLab(labName, inputFilePath, outputFilePath);

            // After running the lab, read the content of the updated input and output files
            string inputFileContent = System.IO.File.ReadAllText(inputFilePath);
            string outputFileContent = System.IO.File.ReadAllText(outputFilePath);

            // Provide success feedback
            ViewBag.SuccessMessage = $"{labName} executed successfully!";
            ViewBag.InputFileContent = inputFileContent;  // Pass the input file content to the view
            ViewBag.OutputFileContent = outputFileContent;  // Pass the output file content to the view
        }
        catch (Exception ex)
        {
            // Handle errors during the process
            ViewBag.ErrorMessage = $"Error while running {labName}: {ex.Message}";
        }

        // Return to the lab view, now with the file content shown
        return View(labName);
    }



}
