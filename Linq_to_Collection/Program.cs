﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Linq_to_Collection
{
    class Program
    {

        private static crcms db = new crcms();


        static void Main(string[] args)
        {
            // Exmpl01();
            // Exmpl02();
            //Exmpl03();
           // Exmpl04();
           // Exmpl05();
           // Exmpl06();
           // Exmpl07();
            Exmpl08();


        }
        //фильтрация  
        //where
        //take
        //Skip
        //TakeWhile
        //SkipWhile
        //Distinct

        static void Exmpl01()
        {
            //where
            var q1 = db.Area
                .Where(w => w.ParentId == 0)
                .Where(w=>!string.IsNullOrEmpty(w.IP))
                .ToList();
            PrintInfo(q1);


            var qq1 = (from a in db.Area
                       where a.ParentId == 0
                       && !string.IsNullOrEmpty(a.IP)
                       select a).ToList();

            PrintInfo(qq1);


            //Take возвращает первые n-элементов и игнорирует остальные
            var q2 = db.Area.Take(5);
            PrintInfo(q2);



            //Skip - игнорирует первые n-элементов
            Console.WriteLine("Skip");
            var q3 = db.Area
                 .Where(w => !string.IsNullOrEmpty(w.IP))
                 .ToList().Skip(3);
            PrintInfo(q3.ToList());


            var q4 = db.Timer.Where(w => w.DateFinish != null)
                .Take(10)
                .ToList()
                .Skip(10)
                .ToList();
           PrintInfo(q4);



            //TakeWhile
            Console.WriteLine("TakeWhile");
            //var q5 = db.Timer
            //    .ToList()
            //    .TakeWhile(s => s.DateFinish != null)
            //    .ToList();
            //   PrintInfo(q5);



            //SkipWhile

            Console.WriteLine("SkipWhile");

            //var q6 = db.Timer.ToList()
            //    .SkipWhile(s => s.DateFinish != null)
            //    .ToList();
            //    PrintInfo(q6);



            //Distinct
            Console.WriteLine("===================================================================");
            Console.WriteLine("Distinct");
           
            var q7 = db.Area.Select(s => new { s.IP }).Distinct();
            Console.WriteLine("Distinct : " + q7.Count());

            var q7_1 = db.Area.Select(s => new { s.IP });
            Console.WriteLine("Total : " + q7_1.Count());

        }


        //Проецирование

        //Select
        //SelectMany

        static void Exmpl02()
        {

            DirectoryInfo []dirs = new DirectoryInfo(@"\\dc\\Студенты\ПКО").GetDirectories();

            //Select

            var q1 = from d in dirs
                     where (d.Attributes & FileAttributes.System) == 0
                     select new
                     {
                         MyDirectoryName = d.FullName,
                         MyCreated = d.CreationTime
                     };


            foreach (var file in q1)
            {
                Console.WriteLine("{0, -40}\t {1}", file.MyDirectoryName, file.MyCreated);

            }


            var q2 = from d in dirs
                     where (d.Attributes & FileAttributes.System) == 0
                     select new
                     {
                         MyDirectoryName = d.FullName,
                         MyCreated = d.CreationTime,
                         Files = from f in d.GetFiles()
                                 select new
                                 {
                                     FileName = f.FullName,
                                     f.Length
                                 }
                     };


            foreach (var file in q2)
            {
                Console.WriteLine("{0, -40}\t {1}", file.MyDirectoryName, file.MyCreated);
                foreach (var f in file.Files)
                {
                    Console.WriteLine("\t-->{0} ({1})", f.FileName, f.Length);
                }
            }




            Console.WriteLine("====================================== var q3 ======================================================");
            var q3 = from d in dirs
                    select new SysFileName()
                     {
                         Directory = d.FullName,
                         Created = d.CreationTime,
                         Files = (from f in d.GetFiles()
                                  select f.Name).ToList()
                                                   };

         
            foreach (var file in q3)
            {
                Console.WriteLine("{0, -40}\t {1}", file.Directory, file.Created);
 
            }


            Console.WriteLine("====================================== var q4 ======================================================");

            List<SysFileName> q4 = (from d2 in dirs
                     select new SysFileName()
                     {
                         Directory = d2.FullName,
                         Created = d2.CreationTime,
                         Files = (from f2 in d2.GetFiles()
                                  select f2.Name).ToList()
                     }).ToList();


            foreach (var file in q4)
            {
                Console.WriteLine("{0, -40}\t {1}", file.Directory, file.Created);
            }



            Console.WriteLine("====================================== var q5 ======================================================");

            List<SysFileName> q5 = dirs.Select (s=>new SysFileName()
                                    {
                                        Directory = s.FullName,
                                        Created = s.CreationTime,
                                        Files = s.GetFiles().Select(f3=> f3.Name).ToList()
                                    }).ToList();


            foreach (var file in q5)
            {
                Console.WriteLine("{0, -40}\t {1}", file.Directory, file.Created);
            }






        }


        //Объединение
        //Join
        //GroupJoin

        public static void Exmpl03()
        {
            var q1 = db.Timer.Join(
                db.Area,
                t => t.AreaId,
                a => a.AreaId,

                (t, a) => new
                {
                    a.FullName,
                    t.DateStart,
                    t.DateFinish,
                    t.TimerId
                });

            foreach (var item in q1)
            {
                Console.WriteLine("{0}|{1} {2} |{3}", item.TimerId, item.FullName , item.DateStart, item.DateFinish);
            }



        }


        //Упорядочивание
        //Orderby
        //ThenBy
        //Reverse

        public static void Exmpl04()
        {

            var q1 = db.Document.OrderBy(o => o.DocumentCreateDate);
            PrintInfo(q1);

            var q2 = q1.ThenBy(t => t.CreatedBy);
            PrintInfo(q2);

            var q3 = db.Area.Reverse();
            PrintInfo(q3);
        }


        public static void Exmpl05()
        {
            string[] Files = Directory.GetFiles(@"\\dc\Студенты\ПКО\PDD 171\Фотография\Ли Анастасия\стекло вода");
            IEnumerable<IGrouping<string, string>> q = 
                Files.GroupBy(f => Path.GetExtension(f));
            foreach (var item in q)
            {
                Console.WriteLine(item.Key);
            }
        }


        //Методы преобразования
        //ofType
        //Cast
        //ToArray и тд

        public static void Exmpl06()
        {
            ArrayList classList = new ArrayList();
            classList.AddRange(new int[] { 3, 4, 5 });
            classList.AddRange(new string[] { "тест", "2" });

            //возбуждает ошибку
            IEnumerable<int> seq = classList.Cast<int>();
            //не возбуждает ошибку
            IEnumerable<int> seq2 = classList.OfType<int>();


            //-----------------------------------------------------------------------------------
            Dictionary<string,string> q2 = db.Area.ToDictionary(a => a.IP, a => a.Name);
            //----------------------------
            Dictionary<string, string> q3 = new Dictionary<string, string>();
            foreach (Area item in db.Area)
            {
                q3.Add(item.IP, item.Name);
            }

            //-----------------------------------------------------------------------------------

        }


        //Поэлементные операции
        //First
        //FirstOrDefault
        //Last
        //LastOrDefault
        //Single // проверяет на уникальность
        //SingleOrDefault 
        //ElementAt  //обращается к конкретному элементу
        //ElementAtOrDefault
        //DefaultIfEmpty //вернет либо пустую

        public static void Exmpl07()
        {
            var q1 = db.Area.First(f=>!string.IsNullOrEmpty(f.IP));
           // var q2 = db.Area.First(f => f.IP == "000"); // будет ошибка т.к. тут нет такого айпи
            var q3 = db.Area.FirstOrDefault(f => f.IP == "000"); // вернет NULL если не найдет

            var q4 = db.Area.ElementAt(3);

            //  Console.WriteLine(q1);

        }

        // Квантификаторы
        //Contains //try or false
        //Any
        //Exist
        //All
        //Equal
        //Sequence

        public static void Exmpl08()
        {
            int[] zone = new[] { 2, 4, 5, 6, 8, 9 };
            List<Area> q1 = db.Area
                .Where(w => zone.Contains(w.AreaId))
                .ToList(); //вытащит все зоны с айдишками { 2, 4, 5, 6, 8, 9 };

            bool q2 = db.Area
                .Select(s => s.AreaId).Contains(3); //Содержится ли зона с айди 3 в этом селекте




            bool q3 = db.Document.Any(d => d.CreatedBy == "Gertsen"); //есть ли хоть один документ созданный Герценом

            bool q4 = db.Timer.All(a => a.DateFinish != null); //все ли имеют дату завершения


            List<int> i = Enumerable.Range(1, 12).ToList();
        }


        static void PrintInfo(List<Area> areas)
        {
            foreach (Area area in areas)
            {
                Console.WriteLine("{0, -50}\t{1}", area.Name, area.IP);
            }
            Console.WriteLine("===================================================================");
        }

        static void PrintInfo(IQueryable<Area> areas) //передадим сам запрос (IQueryable хранит сам запрос)
        {
            foreach (Area area in areas) //тут срабботает запрос
            {
                Console.WriteLine("{0, -50}\t{1}", area.Name, area.IP);
            }
            Console.WriteLine("===================================================================");
        }

        static void PrintInfo(List<Timer> timers) //передадим сам запрос (IQueryable хранит сам запрос)
        {
            foreach (Timer timer in timers) //тут срабботает запрос
            {
                Console.WriteLine("{0, -50}\t{1}:{2}", timer.DocumentId, timer.DateStart, timer.DateFinish);
            }
            Console.WriteLine("===================================================================");
        }

        static void PrintInfo(IQueryable<Document> docs) //передадим сам запрос (IQueryable хранит сам запрос)
        {
            foreach (Document doc in docs) //тут срабботает запрос
            {
                Console.WriteLine("{0, -20}\t{1}:{2}", doc.DocumentId, doc.DocumentCreateDate, doc.CreatedBy);
            }
            Console.WriteLine("===================================================================");
        }

        public class SysFileName
        {
            public string Directory { get; set; }
            public int Size { get; set; }

            public List<string> Files { get; set; }
            public DateTime Created { get; set; }
        }


    }
}
