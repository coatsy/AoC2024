using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day1
{
    internal class LocationList
    {
        private List<int> _left = new List<int>(), _right = new List<int>();

        public void LoadLocations(string[] locations)
        {
            foreach (var location in locations)
            {
                int x, y;

                if (int.TryParse(location.Substring(0, location.IndexOf(' ')).Trim(), out x) && int.TryParse(location.Substring(location.IndexOf(' ') + 1).Trim(), out y))
                {
                    AddLocation(x, y);
                }
                else
                {
                    throw new ArgumentException("Invalid location format");
                }
            }
        }

        public int GetDifferenceSum()
        {
            List<Tuple<int, int>> _locations = _sortedLeft.Zip(_sortedRight, (x, y) => new Tuple<int, int>(x, y)).ToList();
            return _locations.Sum(location => Math.Abs(location.Item1 - location.Item2));
        }

        public int GetSimilarityScore()
        {
            return _left.Sum(l=>l * _right.Count(r=> r == l));
        }

        private void AddLocation(int x, int y)
        {
            _left.Add(x);
            _right.Add(y);
        }

        private IEnumerable<int> _sortedLeft => _left.OrderBy(x => x);
        private IEnumerable<int> _sortedRight => _right.OrderBy(y => y);
    }
}
