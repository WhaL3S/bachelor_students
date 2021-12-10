class Program
    {
        static void ReadStudents(string fd, ref BachelorStudents BS)
        {
            string surname, name;
            int course, nn;
            double average;
            string line;
            using (StreamReader reader = new StreamReader(fd))
            {
                line = reader.ReadLine();
                string[] parts;
                nn = int.Parse(line);
                for (int i = 0; i < nn; i++)
                {
                    line = reader.ReadLine();
                    parts = line.Split(';');
                    surname = parts[0];
                    name = parts[1];
                    course = int.Parse(parts[2]);
                    average = double.Parse(parts[3]);
                    Student stud;
                    stud = new Student();
                    stud.Set(surname, name, course, average);
                    BS.Add(stud);
                }
            }
        }

        static void ReadTimes(string fd, ref BachelorStudents BS)
        {
            int time, nn, mm;
            string line;
            using(StreamReader reader = new StreamReader(fd))
            {
                line = reader.ReadLine();
                string[] parts;
                nn = int.Parse(line);
                line = reader.ReadLine();
                mm = int.Parse(line);
                BS.m = mm;
                for(int i = 0; i < BS.n; i++)
                {
                    line = reader.ReadLine();
                    parts = line.Split(' ');
                    for(int j = 0; j < BS.m; j++)
                    {
                        time = int.Parse(parts[j]);
                        BS.SetWWW(i, j, time);
                    }
                }
            }
        }

        static void PrintStudents(string fv, BachelorStudents BS, string header)
        {
            using(StreamWriter writer = new StreamWriter(fv, true))
            {
                string line = new string('-', 46);
                writer.WriteLine(header);
                writer.WriteLine();
                writer.WriteLine(line);
                writer.WriteLine("No. Name     Surname  Course Online time (min.) ");
                writer.WriteLine(line);
                for (int i = 0; i < BS.n; i++)
                    writer.WriteLine(" {0}. {1} ", i + 1, BS.Get(i).ToString());
                writer.WriteLine(line);
                writer.WriteLine();
            }
        }

        static void PrintTimes(string fv, BachelorStudents BS, string comment)
        {
            using(StreamWriter writer = new StreamWriter(fv, true))
            {
                writer.WriteLine("{0} in {1} days.", comment, BS.m);
                writer.WriteLine();
                for(int i = 0; i < BS.n; i++)
                {
                    writer.Write("{0,4:d}. ", i + 1);
                    for (int j = 0; j < BS.m; j++)
                        writer.Write("{0,3:d} ", BS.GetWWW(i, j));
                    writer.WriteLine();
                }
            }
        }

        static double AverageOnlineTimeForCourse(BachelorStudents BS, int C)
        {
            double sum = 0;
            int count = 0;
            for(int i = 0; i < BS.n; i++)
                if(BS.Get(i).GetCourse() == C)
                {
                    count++;
                    sum = sum + BS.Get(i).GetOnlineTime();
                }
            if (count != 0)
                return sum / count;
            else
                return 0;
        }

        static int DidNotSpendAnyTime(BachelorStudents BS)
        {
            int counter = 0;
            for(int i = 0; i < BS.n; i++)
            {
                if(BS.Get(i).GetOnlineTime() == 0)
                {
                    counter++;
                }
            }
            return counter;
        }

        const string CFd1 = "Students.txt";
        const string CFd2 = "Time.txt";
        const string CFr = "Results.txt";

        static void Main(string[] args)
        {
            BachelorStudents BS = new BachelorStudents();
            ReadStudents(CFd1, ref BS);
            ReadTimes(CFd2, ref BS);
            using(StreamWriter writer = new StreamWriter(CFr, false))
            {
                writer.WriteLine(" Initial data");
                writer.WriteLine();
                writer.WriteLine("Number of students: {0}", BS.n);
                writer.WriteLine("Number of days: {0}", BS.m);
            }
            PrintStudents(CFr, BS, "Bachelor students (online time = 0)");
            PrintTimes(CFr, BS, "Students time spent online");
            using(StreamWriter writer = new StreamWriter(CFr, true))
            {
                writer.WriteLine();
                writer.WriteLine(" Results");
            }
            BS.UpdateStudentData();
            PrintStudents(CFr, BS, " Bachelor students (updated time != 0)");
            BS.SortMinMax();
            PrintStudents(CFr, BS, " Bachelor students (sorted by course and online time)");
            PrintTimes(CFr, BS, "Student time spent online (sorted)");
            using(StreamWriter writer = new StreamWriter(CFr, true))
            {
                int C = 1;
                writer.WriteLine();
                if (AverageOnlineTimeForCourse(BS, C) != 0)
                    writer.WriteLine("{0} course students spent " + "{1,6:f2} minutes online on average.",
                                    C, AverageOnlineTimeForCourse(BS, C));
                else
                    writer.WriteLine("not students in course. {0}", C);
                writer.WriteLine("{0} course students did not spend any time online", DidNotSpendAnyTime(BS));
            }
        }
    }
