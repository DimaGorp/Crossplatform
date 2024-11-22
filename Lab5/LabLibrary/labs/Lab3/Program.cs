﻿using System;
using System.IO;
using System.Collections.Generic;

namespace Lab3Main{
    public class Program
    {
        static List<int>[] adj; // Список смежности
        static int[] visited; // Массив для пометок: 0 - не посещена, 1 - в процессе, 2 - посещена

        // Функция для выполнения DFS и проверки на цикл
        static bool DFS(int v)
        {
            if (visited[v] == 1) return true; // Если вершина в процессе, то это цикл
            if (visited[v] == 2) return false; // Если вершина уже посещена, пропускаем

            visited[v] = 1; // В процессе обработки вершины

            // Рекурсивно обрабатываем все соседние вершины
            foreach (int u in adj[v])
            {
                if (DFS(u)) return true;
            }

            visited[v] = 2; // Вершина полностью обработана
            return false;
        }

        public static void Main(){
            Run("INPUT.txt","OUTPUT.txt");
        }


       public  static void Run(string inputFile, string outputFile)
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

                string[] inputLines = File.ReadAllLines(inputFile);
                string[] firstLine = inputLines[0].Split();
                int N = int.Parse(firstLine[0]); // Количество солдат
                int M = int.Parse(firstLine[1]); // Количество пар

                adj = new List<int>[N + 1]; // +1 для удобства работы с индексацией с 1
                visited = new int[N + 1]; // Массив для пометок

                for (int i = 0; i <= N; i++)
                {
                    adj[i] = new List<int>(); // Инициализация списка смежности
                }

                // Чтение пар и построение графа
                for (int i = 1; i <= M; i++)
                {
                    string[] pair = inputLines[i].Split();
                    int A = int.Parse(pair[0]);
                    int B = int.Parse(pair[1]);
                    adj[A].Add(B); // Добавляем ребро от A к B
                }

                // Проверка на наличие цикла
                for (int i = 1; i <= N; i++)
                {
                    if (visited[i] == 0) // Если вершина еще не посещена
                    {
                        if (DFS(i))
                        {
                            File.WriteAllText(outputFile, "No");
                            Console.WriteLine("The result was successfully written to OUTPUT.txt");
                            return;
                        }
                    }
                }

                // Если цикла нет, выводим "Yes"
                File.WriteAllText(outputFile, "Yes");
                Console.WriteLine("The result was successfully written to OUTPUT.txt");
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