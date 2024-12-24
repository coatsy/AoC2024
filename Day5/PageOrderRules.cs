using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5
{
    internal class PageOrderRules
    {
        private readonly List<Tuple<int, int>> rules = [];
        private readonly List<int> pageOrder = [];
        private bool orderCurrent = false;

        public void AddRule(int lower, int higher)
        {
            rules.Add(Tuple.Create(lower, higher));
            orderCurrent = false;
        }
        public void CalculateOrder()
        {
            if (orderCurrent) return;

            var set = rules.Where(r => !pageOrder.Contains(r.Item1)).ToList();
            while (set.Count() > 1)
            {
                var lowers = set.Select(l => l.Item1).ToList();
                var highers = set.Select(h => h.Item2).ToList();
                var lowest = lowers.First(l => !highers.Contains(l));
                pageOrder.Add(lowest);
                set = rules.Where(r => !pageOrder.Contains(r.Item1)).ToList();
            }
            pageOrder.Add(set.First().Item1);
            pageOrder.Add(set.First().Item2);

            orderCurrent = true;
        }

        public int[] GetValidOrder(int[] update)
        {
            if (!orderCurrent) CalculateOrder();
            return pageOrder.Where(p=> update.Contains(p)).ToArray();
        }

        public bool IsValidOrder(int[] update)
        {
            var validUpdate = GetValidOrder(update);
            for (int i = 0; i < update.Length; i++)
            {
                if (update[i] != validUpdate[i]) return false;
            }
            return true;
        }

    }
}
