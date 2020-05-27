using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqConsoleApp
{
    public class LinqSamples
    {
        public static IEnumerable<Emp> Emps { get; set; }
        public static IEnumerable<Dept> Depts { get; set; }

        public LinqSamples()
        {
            LoadData();
            Przyklad12();
        }

        public void LoadData()
        {
            var empsCol = new List<Emp>();
            var deptsCol = new List<Dept>();

            #region Load depts
            var d1 = new Dept
            {
                Deptno = 1,
                Dname = "Research",
                Loc = "Warsaw"
            };

            var d2 = new Dept
            {
                Deptno = 2,
                Dname = "Human Resources",
                Loc = "New York"
            };

            var d3 = new Dept
            {
                Deptno = 3,
                Dname = "IT",
                Loc = "Los Angeles"
            };

            deptsCol.Add(d1);
            deptsCol.Add(d2);
            deptsCol.Add(d3);
            Depts = deptsCol;
            #endregion

            #region Load emps
            var e1 = new Emp
            {
                Deptno = 1,
                Empno = 1,
                Ename = "Jan Kowalski",
                HireDate = DateTime.Now.AddMonths(-5),
                Job = "Backend programmer",
                Mgr = null,
                Salary = 2000
            };

            var e2 = new Emp
            {
                Deptno = 1,
                Empno = 20,
                Ename = "Anna Malewska",
                HireDate = DateTime.Now.AddMonths(-7),
                Job = "Frontend programmer",
                Mgr = e1,
                Salary = 4000
            };

            var e3 = new Emp
            {
                Deptno = 1,
                Empno = 2,
                Ename = "Marcin Korewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Frontend programmer",
                Mgr = null,
                Salary = 5000
            };

            var e4 = new Emp
            {
                Deptno = 2,
                Empno = 3,
                Ename = "Paweł Latowski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Frontend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e5 = new Emp
            {
                Deptno = 2,
                Empno = 4,
                Ename = "Michał Kowalski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Backend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e6 = new Emp
            {
                Deptno = 2,
                Empno = 5,
                Ename = "Katarzyna Malewska",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Manager",
                Mgr = null,
                Salary = 8000
            };

            var e7 = new Emp
            {
                Deptno = null,
                Empno = 6,
                Ename = "Andrzej Kwiatkowski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "System administrator",
                Mgr = null,
                Salary = 7500
            };

            var e8 = new Emp
            {
                Deptno = 2,
                Empno = 7,
                Ename = "Marcin Polewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Mobile developer",
                Mgr = null,
                Salary = 4000
            };

            var e9 = new Emp
            {
                Deptno = 2,
                Empno = 8,
                Ename = "Władysław Torzewski",
                HireDate = DateTime.Now.AddMonths(-9),
                Job = "CTO",
                Mgr = null,
                Salary = 12000
            };

            var e10 = new Emp
            {
                Deptno = 2,
                Empno = 9,
                Ename = "Andrzej Dalewski",
                HireDate = DateTime.Now.AddMonths(-4),
                Job = "Database administrator",
                Mgr = null,
                Salary = 9000
            };

            empsCol.Add(e1);
            empsCol.Add(e2);
            empsCol.Add(e3);
            empsCol.Add(e4);
            empsCol.Add(e5);
            empsCol.Add(e6);
            empsCol.Add(e7);
            empsCol.Add(e8);
            empsCol.Add(e9);
            empsCol.Add(e10);
            Emps = empsCol;

            #endregion

        }


        /*
            Celem ćwiczenia jest uzupełnienie poniższych metod.
         *  Każda metoda powinna zawierać kod C#, który z pomocą LINQ'a będzie realizować
         *  zapytania opisane za pomocą SQL'a.
         *  Rezultat zapytania powinien zostać wyświetlony za pomocą kontrolki DataGrid.
         *  W tym celu końcowy wynik należy rzutować do Listy (metoda ToList()).
         *  Jeśli dane zapytanie zwraca pojedynczy wynik możemy je wyświetlić w kontrolce
         *  TextBox WynikTextBox.
        */

        /// <summary>
        /// SELECT * FROM Emps WHERE Job = "Backend programmer";
        /// </summary>
        public IEnumerable<object> Przyklad1()
        {
            var result = Emps.Where(emp => emp.Job == "Backend programmer").ToList();
            return result;
        }

        /// <summary>
        /// SELECT * FROM Emps Job = "Frontend programmer" AND Salary>1000 ORDER BY Ename DESC;
        /// </summary>
        public IEnumerable<object> Przyklad2()
        {
            var result = Emps.Where(emp => emp.Job == "Backend programmer" && emp.Salary > 1000).OrderByDescending(emp => emp.Ename).ToList();
            return result;
        }

        /// <summary>
        /// SELECT MAX(Salary) FROM Emps;
        /// </summary>
        public int Przyklad3()
        {
            var result = Emps.Max(emp => emp.Salary);
            return result;
        }

        /// <summary>
        /// SELECT * FROM Emps WHERE Salary=(SELECT MAX(Salary) FROM Emps);
        /// </summary>
        public IEnumerable<object> Przyklad4()
        {
            var result = Emps.Where(emp => emp.Salary == Emps.Max(e => e.Salary)).ToList();
            return result;
        }

        /// <summary>
        /// SELECT ename AS Nazwisko, job AS Praca FROM Emps;
        /// </summary>
        public IEnumerable<object> Przyklad5()
        {
            var result = Emps.Select(emp => new
            {
                Nazwisko = emp.Ename,
                Praca = emp.Job,
            }).ToList();
            return result;
        }

        /// <summary>
        /// SELECT Emps.Ename, Emps.Job, Depts.Dname FROM Emps
        /// INNER JOIN Depts ON Emps.Deptno=Depts.Deptno
        /// Rezultat: Złączenie kolekcji Emps i Depts.
        /// </summary>
        public IEnumerable<object> Przyklad6()
        {
            var lambdaResult = Emps.Join(Depts, emp => emp.Deptno,
                dept => dept.Deptno,
                (emp, dept) => new
                {
                    Ename = emp.Ename,
                    Job = emp.Job,
                    Dname = dept.Dname
                }
            ).ToList();
            return lambdaResult;
        }

        /// <summary>
        /// SELECT Job AS Praca, COUNT(1) LiczbaPracownikow FROM Emps GROUP BY Job;
        /// </summary>
        public IEnumerable<object> Przyklad7()
        {
            var lambdaResult = Emps.GroupBy(
                emp => emp.Job,
                (key, g) => new
                {
                    Praca = key,
                    LiczbaPracownikow = g.Count()
                }).ToList();
            return lambdaResult;
        }

        /// <summary>
        /// Zwróć wartość "true" jeśli choć jeden
        /// z elementów kolekcji pracuje jako "Backend programmer".
        /// </summary>
        public bool Przyklad8()
        {
            var result = Emps.Any(emp => emp.Job == "Backend programmer");
            return result;

        }

        /// <summary>
        /// SELECT TOP 1 * FROM Emp WHERE Job="Frontend programmer"
        /// ORDER BY HireDate DESC;
        /// </summary>
        public Emp Przyklad9()
        {
            var result = Emps.Where(emp => emp.Job == "Frontend Programmer")
                .OrderByDescending(emp => emp.HireDate).First();

            return result;
        }

        /// <summary>
        /// SELECT Ename, Job, Hiredate FROM Emps
        /// UNION
        /// SELECT "Brak wartości", null, null;
        /// </summary>
        public IEnumerable<object> Przyklad10Button_Click()
        {
            Emp select = new Emp { Ename = "Brak wartości", Job = null, HireDate = null };
            var selectList = new List<Emp> { select };

            var result = Emps.Union(selectList).Select(emp => new
            {
                Ename = emp.Ename,
                Job = emp.Job,
                Hiredate = emp.HireDate
            }).ToList();
            return result;
        }

        //Znajdź pracownika z najwyższą pensją wykorzystując metodę Aggregate()
        public Emp Przyklad11()
        {
            var result = Emps.Aggregate((emp1, emp2) => emp1.Salary < emp2.Salary ? emp2 : emp1);

            return result;
        }

        //Z pomocą języka LINQ i metody SelectMany wykonaj złączenie
        //typu CROSS JOIN
        public IEnumerable<object> Przyklad12()
        {
            var result = Emps.SelectMany(emp => Depts, (emp, dept) => new 
            {
                emp.Empno,
                emp.Ename,
                emp.HireDate,
                emp.Job,
                emp.Mgr,
                emp.Salary,
                emp.Deptno,
                dept.Dname,
                dept.Loc,
            }).ToList();
            return result;
        }
    }
}
