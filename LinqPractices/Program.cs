﻿using System;
using System.Diagnostics;
using System.Linq;

namespace LinqPractices
{
    class Program
    {
        static void Main(string[] _6)
        {
            DataGenerator.Initialize();
            var context = new StudentDbContext();
            var students = context.Students.ToList<Student>();

            Console.WriteLine("**** ToList ****");
            var studentList = context.Students.Where(x => x.ClassId == 6).ToList();
            Console.WriteLine("Student List Count: " + studentList.Count);

            Console.WriteLine("\n\n **** Anonymous Object Result ****");
            var anonymousObject = context.Students.Where(x => x.ClassId == 2)
                .Select(x => new
                {
                    Id = x.Id,
                    FullName = x.Name + " " + x.Surname
                }).Take(6);
            foreach (var item in anonymousObject)
            {
                Console.WriteLine(item.Id + " " + item.FullName);
            }
        }

        private static void OrderByLINQSamples(StudentDbContext context)
        {
            Console.WriteLine("**** OrderBy ****");
            var studentsOrderByASC = context.Students.OrderBy(x => x.Name).Distinct<Student>().Take(6).ToList();
            foreach (var student in studentsOrderByASC)
            {
                Console.WriteLine(student.Id + " " + student.Name + " " + student.Surname + " " + student.ClassId);
            }

            Console.WriteLine("**** OrderByDescending ****");
            var studentsOrderByDESC = context.Students.OrderByDescending(x => x.Name).Distinct<Student>().Take(6).ToList();
            foreach (var student in studentsOrderByDESC)
            {
                Console.WriteLine(student.Id + " " + student.Name + " " + student.Surname + " " + student.ClassId);
            }
        }

        private static void ErrorCuriosity(StudentDbContext context)
        {
            var forError1 = context.Students.FirstOrDefault(x => x.Surname == "çalışkan");
            var forError3 = context.Students.SingleOrDefault(x => x.Surname == "çalışkan");
            var forError5 = context.Students.LastOrDefault(x => x.Surname == "çalışkan");

            Console.WriteLine(forError1); 
            Console.WriteLine(forError3); 
            Console.WriteLine(forError5);
        }

        private static void TimeCuriosity(StudentDbContext context)
        {
            var sw = Stopwatch.StartNew(); 
            var watch = Stopwatch.StartNew(); 

            Console.WriteLine("**** SingleOrDefault ****");
            var student1 = context.Students.SingleOrDefault<Student>(x => x.Id == 1);
            watch.Stop();
            Console.WriteLine("Time elapsed: {0} milliseconds", watch.ElapsedMilliseconds);
            watch.Reset();

            watch.Start();
            Console.WriteLine("**** Where + SingleOrDefault ****");
            var student2 = context.Students.Where<Student>(x => x.Id == 1).SingleOrDefault<Student>();
            watch.Stop();
            Console.WriteLine("Time elapsed: {0} milliseconds", watch.ElapsedMilliseconds);
            watch.Reset();

            watch.Start();
            Console.WriteLine("**** Single ****");
            var student3 = context.Students.Single<Student>(x => x.Id == 1);
            watch.Stop();
            Console.WriteLine("Time elapsed: {0} milliseconds", watch.ElapsedMilliseconds);
            watch.Reset();

            watch.Start();
            Console.WriteLine("**** Where + Single ****");
            var student4 = context.Students.Where<Student>(x => x.Id == 1).Single<Student>();
            watch.Stop();
            Console.WriteLine("Time elapsed: {0} milliseconds", watch.ElapsedMilliseconds);
            watch.Reset();

            watch.Start();
            Console.WriteLine("**** Find ****");
            var student5 = context.Students.Find(1);
            watch.Stop();
            Console.WriteLine("Time elapsed: {0} milliseconds", watch.ElapsedMilliseconds);
            watch.Reset();

            watch.Start();
            Console.WriteLine("**** FirstOrDefault ****");
            var student6 = context.Students.FirstOrDefault<Student>(x => x.Id == 1);
            watch.Stop();
            Console.WriteLine("Time elapsed: {0} milliseconds", watch.ElapsedMilliseconds);
            watch.Reset();

            watch.Start();
            Console.WriteLine("**** Where + FirstOrDefault ****");
            var student6_1 = context.Students.Where<Student>(x => x.Id == 1).FirstOrDefault<Student>();
            watch.Stop();
            Console.WriteLine("Time elapsed: {0} milliseconds", watch.ElapsedMilliseconds);
            watch.Reset();

            watch.Start();
            Console.WriteLine("**** First ****");
            var student7 = context.Students.First<Student>(x => x.Id == 1);
            watch.Stop();
            Console.WriteLine("Time elapsed: {0} milliseconds", watch.ElapsedMilliseconds);
            watch.Reset();

            watch.Start();
            Console.WriteLine("**** Where + First ****");
            var student7_1 = context.Students.Where<Student>(x => x.Id == 1).First<Student>();
            watch.Stop();
            Console.WriteLine("Time elapsed: {0} milliseconds", watch.ElapsedMilliseconds);
            watch.Reset();

            watch.Start();
            Console.WriteLine("**** Last ****");
            var student8 = context.Students.Last<Student>(x => x.Id == 1);
            watch.Stop();
            Console.WriteLine("Time elapsed: {0} milliseconds", watch.ElapsedMilliseconds);
            watch.Reset();

            watch.Start();
            Console.WriteLine("**** Where + Last ****");
            var student8_1 = context.Students.Where<Student>(x => x.Id == 1).Last<Student>();
            watch.Stop();
            Console.WriteLine("Time elapsed: {0} milliseconds", watch.ElapsedMilliseconds);
            watch.Reset();

            watch.Start();
            Console.WriteLine("**** LastOrDefault ****");
            var student9 = context.Students.LastOrDefault<Student>(x => x.Id == 1);
            watch.Stop();
            Console.WriteLine("Time elapsed: {0} milliseconds", watch.ElapsedMilliseconds);
            watch.Reset();

            watch.Start();
            Console.WriteLine("**** Where + LastOrDefault ****");
            var student9_1 = context.Students.Where<Student>(x => x.Id == 1).LastOrDefault<Student>();
            watch.Stop();
            Console.WriteLine("Time elapsed: {0} milliseconds", watch.ElapsedMilliseconds);
            watch.Reset();

            watch.Start();
            Console.WriteLine("**** Average ****");
            var student10 = context.Students.Average<Student>(x => x.Id);
            watch.Stop();
            Console.WriteLine("Time elapsed: {0} milliseconds", watch.ElapsedMilliseconds);
            watch.Reset();

            Console.WriteLine(student1.Name);
            Console.WriteLine(student2.Name);
            Console.WriteLine(student3.Name);
            Console.WriteLine(student3.Name);
            Console.WriteLine(student5.Name);
            Console.WriteLine(student6.Name);
            Console.WriteLine(student6_1.Name);
            Console.WriteLine(student7.Name);
            Console.WriteLine(student7_1.Name);
            Console.WriteLine(student8.Name);
            Console.WriteLine(student8_1.Name);
            Console.WriteLine(student9.Name);
            Console.WriteLine(student9_1.Name);
            Console.WriteLine(student10);

            var count = context.Students.Count<Student>();
            Console.WriteLine(count);

            sw.Stop();
            Console.WriteLine("Time elapsed over Method: {0} milliseconds", sw.ElapsedMilliseconds);
        }
    }
}
