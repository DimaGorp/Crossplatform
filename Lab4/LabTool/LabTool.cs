using System;
using McMaster.Extensions.CommandLineUtils;
using LabLibrary;
using System.ComponentModel.DataAnnotations;
using System.IO;


[Command(Description = "tool for execute lab",Name ="LabTool")]
[Subcommand(typeof(Version))]
[Subcommand(typeof(Runner))]
[Subcommand(typeof(EnvPathSetter))]
class Program
{   
    public static int Main(string[] args){
    
        var app = new CommandLineApplication<Program>();
        app.Conventions.UseDefaultConventions();

            // Действие, если команда не указана.
        app.OnExecute(() => 
        {
        // Отображение справочной информации.
            app.ShowHelp(); 
            return 1;
        });

        // Указание, как обрабатывать нераспознанные аргументы: прекратить разбор и собрать их.
        app.UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.StopParsingAndCollect;

        return app.Execute(args);
    }
}
[Command(Name="version",Description="Show a version of a Library")]
class Version{
    private void OnExecute(){
        Console.WriteLine("Library - Version 1.0.0");
        Console.WriteLine("Author: Dmytro Gorpinuk");
    }
}
[Command(Name="run", Description="Lab Runner")]
class Runner
{
    [Argument(0)]
    [Required]
    public string Lab { get; set; }

    [Option("-I|--input", "Input file", CommandOptionType.SingleValue)]
    public string InputFile { get; set; }

    [Option("-o|--output", "Output file", CommandOptionType.SingleValue)]
    public string OutputFile { get; set; }

    private void OnExecute()
    {
         // Перевірка на параметри консолі
        if (string.IsNullOrEmpty(InputFile))
        {
            InputFile = "INPUT.txt";  // Значення за замовчуванням
        }
        if (string.IsNullOrEmpty(OutputFile))
        {
            OutputFile = "OUTPUT.txt";  // Значення за замовчуванням
        }

        string inputPath = string.Empty;
        string outputPath = string.Empty;

        // Якщо шлях до файлу задано параметром консолі, використовуємо його
        if (Path.IsPathRooted(InputFile) && Path.IsPathRooted(OutputFile))
        {
            inputPath = InputFile;
            outputPath = OutputFile;
        }
        else
        {
            // Якщо параметри не задано, перевіряємо змінну середовища LAB_PATH
            string labPath = Environment.GetEnvironmentVariable("LAB_PATH");
            Console.WriteLine($"Using Input File: {labPath}");
            if (!string.IsNullOrEmpty(labPath))
            {
                inputPath = Path.Combine(labPath, InputFile);
                outputPath = Path.Combine(labPath, OutputFile);
            }
            else
            {
                // Якщо змінна середовища LAB_PATH не встановлена, перевіряємо домашню директорію користувача
                string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                inputPath = Path.Combine(homeDirectory, InputFile);
                outputPath = Path.Combine(homeDirectory, OutputFile);
            }
        }
        // Перевірка чи існує файл, що був знайдений
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Error: The file {inputPath} was not found.");
            return;
        }

        // Виводимо знайдений шлях до файлів для перевірки
        Console.WriteLine($"Using Input File: {inputPath}");
        Console.WriteLine($"Using Output File: {outputPath}");


        // Обработка выбора лаборатории
        string CurrentLab = Lab.ToLower();
        switch (CurrentLab)
        {
            case "lab1":
                LabRunner.RunLab(CurrentLab, inputPath, outputPath);
                break;
            case "lab2":
                LabRunner.RunLab(CurrentLab, inputPath, outputPath);
                break;
            case "lab3":
                LabRunner.RunLab(CurrentLab, inputPath, outputPath);
                break;
            default:
                Console.WriteLine($"Unknown lab: {CurrentLab}");
                Console.WriteLine("Use [help] for more information.");
                break;
        }
    }
}

[Command(Name = "set-path", Description = "Set the LAB_PATH environment variable")]
class EnvPathSetter
{
    [Option("-p|--path", "Path to the folder containing input and output files", CommandOptionType.SingleValue)]
    [Required]
    public string Path { get; set; }

    private void OnExecute()
    {
        //Чи існує така директорія
        if (!Directory.Exists(Path))
        {
            Console.WriteLine($"Error: The directory {Path} does not exist.");
            return;
        }

        //Встановлення змінно LAB_PATH
        Environment.SetEnvironmentVariable("LAB_PATH", Path, EnvironmentVariableTarget.User);
        string labPath = Environment.GetEnvironmentVariable("LAB_PATH");
        Console.WriteLine($"LAB_PATH has been set to: {labPath}");
    }
}