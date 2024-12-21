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

        public Report(string line)
        {
            var levelStrings = line.Split(' ');
            levels = levelStrings.Select(int.Parse).ToList();
        }

        public bool IsSafe
        {
            get
            {
                if (levels.Count < 2) return false;

                var isSafe = true;
                var thisNum = levels[0];
                var direction = (levels[0] > levels[1] ? -1 : (levels[0] == levels[1] ? 0 : 1));
                if (direction == 0) return false;

                foreach (var level in levels.Skip(1))
                {
                    var newDirection = (thisNum > level ? -1 : (thisNum == level ? 0 : 1));
                    if (newDirection != direction)
                    {
                        isSafe = false;
                        break;
                    }

                    isSafe = (Math.Abs(thisNum - level) <= 3);
                    thisNum = level;

                    if (!isSafe)
                    {
                        break;
                    }
                }

                return isSafe;
            }
        }
    }
}
