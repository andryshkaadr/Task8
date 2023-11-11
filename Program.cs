namespace Task8
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static ScoreManager scoreManager;

        static Program()
        {
            AppDomain.CurrentDomain.ProcessExit += (sender, e) => SaveDataOnExit();
            scoreManager = LoadData();
        }

        static void SaveDataOnExit()
        {
            DataSerializer.SaveBinary(scoreManager.GetStudentsWithScores(), "students.bin");
            DataSerializer.SaveJson(scoreManager.GetStudentsWithScores(), "students.json");
            Console.WriteLine("Data saved on exit (Binary and JSON).");
        }

        static ScoreManager LoadData()
        {
            ScoreManager manager = DataSerializer.LoadJson<ScoreManager>("students.json");

            if (manager == null)
            {
                manager = new ScoreManager();

                List<Student> students = DataSerializer.LoadBinary<List<Student>>("students.bin");
                if (students != null && students.Count > 0)
                {
                    foreach (var student in students)
                    {
                        manager.AddStudent(student.Name, student.GetScores());
                    }
                }
            }

            return manager;
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Remove Student");
                Console.WriteLine("3. Add Score");
                Console.WriteLine("4. Remove Score");
                Console.WriteLine("5. Get Student Score");
                Console.WriteLine("6. Get All Students with Scores");
                Console.WriteLine("7. Exit");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Write("Enter student name: ");
                            string studentName = Console.ReadLine();
                            scoreManager.AddStudent(studentName, new Dictionary<string, int>());
                            break;

                        case 2:
                            Console.Write("Enter student name: ");
                            studentName = Console.ReadLine();
                            scoreManager.RemoveStudent(studentName);
                            break;

                        case 3:
                            Console.Write("Enter student name: ");
                            studentName = Console.ReadLine();
                            Console.Write("Enter subject: ");
                            string subject = Console.ReadLine();
                            if (int.TryParse(Console.ReadLine(), out int score))
                            {
                                scoreManager.AddScore(studentName, subject, score);
                            }
                            else
                            {
                                Console.WriteLine("Invalid score. Please enter a valid number.");
                            }
                            break;

                        case 4:
                            Console.Write("Enter student name: ");
                            studentName = Console.ReadLine();
                            Console.Write("Enter subject: ");
                            subject = Console.ReadLine();
                            scoreManager.RemoveScore(studentName, subject);
                            break;

                        case 5:
                            Console.Write("Enter student name: ");
                            studentName = Console.ReadLine();
                            Console.Write("Enter subject: ");
                            subject = Console.ReadLine();
                            int studentScore = scoreManager.GetStudentScore(studentName, subject);
                            if (studentScore != -1)
                            {
                                Console.WriteLine($"{studentName} scored {subject}: {studentScore}");
                            }
                            else
                            {
                                Console.WriteLine("Student not found or no score for the subject.");
                            }
                            break;

                        case 6:
                            List<Student> studentsWithScores = scoreManager.GetStudentsWithScores();
                            Console.WriteLine("List of Students with Scores:");
                            foreach (var student in studentsWithScores)
                            {
                                Console.WriteLine($"{student.Name}'s Scores:");
                                foreach (var item in student.GetScores())
                                {
                                    Console.WriteLine($"{item.Key}: {item.Value}");
                                }
                                Console.WriteLine();
                            }
                            break;

                        case 7:
                            Environment.Exit(0);
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please select the correct option.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }
        }
    }
}
