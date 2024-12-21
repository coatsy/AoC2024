using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
    internal class ReportList
    {
        private List<Report> reports = new List<Report>();
        public int SafeCount => reports.Count(r => r.IsSafe);
        public int ReportCount => reports.Count;
        public int DampedSafeCount => reports.Count(r=>r.IsDampedSafe);

        public int OnlyDampedSafeCount => reports.Count(r=>r.onlyDampedSafe);

        public ReportList(IEnumerable<string> input)
        {
            foreach (var line in input)
            {
                var report = new Report(line);
                reports.Add(report);
            }
        }

    }

    internal class Report
    {
        private List<int> levels;
        public bool onlyDampedSafe = false;

        public Report(string line)
        {
            var levelStrings = line.Split(' ');
            levels = levelStrings.Select(int.Parse).ToList();
        }

        public bool IsDampedSafe
        {
            get
            {
                Console.Write($"{JsonConvert.SerializeObject(levels)} - ");
                var firstPass = checkReport(levels);

                if (firstPass)
                {
                    Console.WriteLine(" - Safe");
                    return true; 
                }

                for (int i = 0; i < levels.Count; i++)
                {
                    if (checkReport(RemoveItemFromList(levels, i)))
                    {
                        onlyDampedSafe = true;
                        Console.WriteLine(" - Damped Safe");
                        return true; 
                    }
                }


                Console.WriteLine(" - Not Safe");
                return false;

            }
        }

        private List<int> RemoveItemFromList(List<int> list, int itemToRemove)
        {
            var theList = new List<int>(list);
            theList.RemoveAt(itemToRemove);
            return theList;
        }

        public bool IsSafe
        {
            get
            {
                return checkReport(levels);
            }
        }

        private bool checkReport(List<int> lvls)
        {
            //if (lvls.Count < 2) return true;

            var isSafe = false;
            var thisNum = lvls[0];
            var direction = (lvls[0] > lvls[1] ? -1 : (lvls[0] == lvls[1] ? 0 : 1));
            if (direction == 0)
            {
                Console.Write("d");
                return false;
            }

            foreach (var level in lvls.Skip(1))
            {
                var newDirection = (thisNum > level ? -1 : (thisNum == level ? 0 : 1));
                if (newDirection != direction)
                {
                    isSafe = false;
                    Console.Write("c");
                    break;
                }

                isSafe = (Math.Abs(thisNum - level) <= 3);
                thisNum = level;

                if (!isSafe)
                {
                    Console.Write("g");
                    break;
                }
            }

            return isSafe;
        }
    }
}
