class Student
    {
        private string name,
                       surname;
        private int course,
                    spentTime;
        private double average;
        
        public Student()
        {
            name = "";
            surname = "";
            course = 0;
            spentTime = 0;
            average = 0.0;
        }

        public void Set(string name, string surname, int course, double average)
        {
            this.name = name;
            this.surname = surname;
            this.course = course;
            this.average = average;
        }

        public void SetOnlineTime(int time) { this.spentTime = time; }
        public string GetName() { return name; }
        public string GetSurname() { return surname; }
        public int GetCourse() { return course; }
        public double GetLearningAverage() { return average; }
        public int GetOnlineTime() { return spentTime; }

        public override string ToString()
        {
            string line;
            line = string.Format("{0,-8} {1, -10} {2,2:d} {3, 12:f2}", surname, name, course, spentTime);
            return line;
        }

        public static bool operator <=(Student first, Student second)
        {
            return first.course < second.course ||
                first.course == second.course && first.spentTime < second.spentTime;
        }

        public static bool operator >=(Student first, Student second)
        {
            return first.course > second.course ||
                first.course == second.course && first.spentTime > second.spentTime;
        }
    }
