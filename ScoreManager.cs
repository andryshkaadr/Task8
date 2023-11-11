namespace Task8
{
    using System.Collections.Generic;

    class ScoreManager
    {
        private Dictionary<string, Student> Students { get; set; }

        public ScoreManager()
        {
            Students = new Dictionary<string, Student>();
        }

        public void AddStudent(string name, Dictionary<string, int> scores)
        {
            if (!Students.ContainsKey(name))
            {
                var student = new Student(name);
                foreach (var subject in scores.Keys)
                {
                    student.AddScore(subject, scores[subject]);
                }
                Students[name] = student;
            }
        }

        public void RemoveStudent(string name)
        {
            if (Students.ContainsKey(name))
            {
                Students.Remove(name);
            }
        }

        public void AddScore(string studentName, string subject, int score)
        {
            if (Students.ContainsKey(studentName))
            {
                Students[studentName].AddScore(subject, score);
            }
        }

        public void RemoveScore(string studentName, string subject)
        {
            if (Students.ContainsKey(studentName))
            {
                Students[studentName].RemoveScore(subject);
            }
        }

        public int GetStudentScore(string studentName, string subject)
        {
            return Students.ContainsKey(studentName) ? Students[studentName].GetScore(subject) : -1;
        }

        public List<Student> GetStudentsWithScores()
        {
            return new List<Student>(Students.Values);
        }
    }
}
