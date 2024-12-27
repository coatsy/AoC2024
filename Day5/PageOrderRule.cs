using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5
{
    // class to hold the information aboud a particular page order - what pagse should appear before and after it
    internal class PageOrderRule
    {
        private readonly List<int> lower = [];
        private readonly List<int> higher = [];

        public bool IsInLowerList(int check) => lower.Contains(check);
        public bool IsInHigherList(int check) => higher.Contains(check);
        public void AddHigher(int noToAdd)
        {
            if (!IsInHigherList(noToAdd)) higher.Add(noToAdd);
        }
        public void AddLower(int noToAdd)
        {
            if (!IsInLowerList(noToAdd)) lower.Add(noToAdd);
        }

        internal IEnumerable<int> GetHigherList() => higher;
        internal IEnumerable<int> GetLowerList() => lower;
    }
}
