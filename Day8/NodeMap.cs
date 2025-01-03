using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day8
{
    internal class NodeMap
    {
        private readonly Dictionary<char, List<Point>> nodeLocations = [];
        private readonly Dictionary<Tuple<int, int>, int> antiNodes = [];
        private int xMax = 0, yMax = 0;

        public NodeMap(string input)
        {
            ReadInNodes(input);

            CalculateAntiNodes();
        }

        public int AntiNodeCount => antiNodes.Keys.Count;

        private void CalculateAntiNodes()
        {
            // for each character
            foreach (var ch in nodeLocations.Keys)
            {
                for (var pt1 = 0; pt1 < nodeLocations[ch].Count - 1; pt1++)
                {
                    for (var pt2 = pt1 + 1; pt2 < nodeLocations[ch].Count; pt2++)
                    {
                        var xDir = (nodeLocations[ch][pt1].X < nodeLocations[ch][pt2].X) ? -1 : 1;
                        var yDir = (nodeLocations[ch][pt1].Y < nodeLocations[ch][pt2].Y) ? -1 : 1;
                        var xDist = Math.Abs(nodeLocations[ch][pt1].X - nodeLocations[ch][pt2].X);
                        var yDist = Math.Abs(nodeLocations[ch][pt1].Y - nodeLocations[ch][pt2].Y);
                        // Check the 2 AntiNodes are in range
                        if (nodeLocations[ch][pt1].X + (xDir * xDist) >= 0 &&
                            nodeLocations[ch][pt1].X + (xDir * xDist) <= xMax &&
                            nodeLocations[ch][pt1].Y + (yDir * yDist) >= 0 &&
                            nodeLocations[ch][pt1].Y + (yDir * yDist) <= yMax)
                        {
                            var antiNode = new Tuple<int, int>(nodeLocations[ch][pt1].X + (xDir * xDist), nodeLocations[ch][pt1].Y + (yDir * yDist));
                            if (!antiNodes.ContainsKey(antiNode))
                            {
                                antiNodes[antiNode] = 0;
                            }
                            antiNodes[antiNode]++;
                        }
                        if (nodeLocations[ch][pt2].X - (xDir * xDist) >= 0 &&
                            nodeLocations[ch][pt2].X - (xDir * xDist) <= xMax &&
                            nodeLocations[ch][pt2].Y - (yDir * yDist) >= 0 &&
                            nodeLocations[ch][pt2].Y - (yDir * yDist) <= yMax)
                        {
                            var antiNode = new Tuple<int, int>(nodeLocations[ch][pt2].X - (xDir * xDist), nodeLocations[ch][pt2].Y - (yDir * yDist));
                            if (!antiNodes.ContainsKey(antiNode))
                            {
                                antiNodes[antiNode] = 0;
                            }
                            antiNodes[antiNode]++;
                        }
                    }
                }
            }
        }

        private void ReadInNodes(string input)
        {
            // read in all the points
            using (var reader = new StringReader(input))
            {
                var lineNo = 0;
                var line = reader.ReadLine();
                while (line != null)
                {
                    for (var c = 0; c < line.Length; c++)
                    {
                        if (line[c] != '.')
                        {
                            if (!nodeLocations.TryGetValue(line[c], out List<Point>? value))
                            {
                                value = new List<Point>();
                                nodeLocations[line[c]] = value;
                            }

                            value.Add(new Point(c, lineNo));
                        }
                    }
                    xMax = Math.Max(xMax, line.Length - 1);
                    yMax = Math.Max(yMax, lineNo);
                    line = reader.ReadLine();
                    lineNo++;
                }
            }
        }
    }
}
