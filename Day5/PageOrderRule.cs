using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5
{
    internal class PageOrderRule
    {
        private List<int> lower = [];
        private List<int> higher = [];

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
    }
}
