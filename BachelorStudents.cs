class BachelorStudents
    {
        const int CMaxS = 1000;
        const int CMaxD = 30;
        private Student[] Students;
        public int n { get; set; }
        private int[,] WWW;
        public int m { get; set; }

        public BachelorStudents()
        {
            n = 0;
            Students = new Student[CMaxS];
            m = 0;
            WWW = new int[CMaxS, CMaxD];
        }

        public Student Get(int nr) { return Students[nr]; }
        public void Add(Student ob) { Students[n++] = ob; }
        public void ChangeStudent(int i, Student stud) { Students[i] = stud; }
        public void SetWWW(int i, int j, int r) { WWW[i, j] = r; }
        public int GetWWW(int i, int j) { return WWW[i, j]; }

        public void SwapLinesWWW(int nr1, int nr2)
        {
            for (int j = 0; j < m; j++)
            {
                int d = WWW[nr1, j];
                WWW[nr1, j] = WWW[nr2, j];
                WWW[nr2, j] = d;
            }
        }

        public void UpdateStudentData()
        {
            int sum;
            Student stud;
            for(int i = 0; i < n; i++)
            {
                sum = 0;
                for (int j = 0; j < m; j++)
                    sum = sum + WWW[i, j];
                stud = Get(i);
                stud.SetOnlineTime(sum);
                ChangeStudent(i, stud);
            }
        }

        public void SortMinMax()
        {
            Student stud;
            for(int i = 0; i < n - 1; i++)
            {
                int minnr = i;
                for (int j = i + 1; j < n; j++)
                    if (Get(j) <= Get(minnr))
                        minnr = j;
                stud = Get(i);
                ChangeStudent(i, Get(minnr));
                ChangeStudent(minnr, stud);
                SwapLinesWWW(i, minnr);
            }
        }
    }
