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
        private readonly Dictionary<Tuple<int, int>, int> singleDistanceAntiNodes = [];
        private readonly Dictionary<Tuple<int, int>, int> anyDistanceAntiNodes = [];
        private int xMax = 0, yMax = 0;

        public NodeMap(string input)
        {
            ReadInNodes(input);

            CalculateSingleDistanceAntiNodes();
            CalculateAnyDistanceAntiNodes();
        }

        public int SingleDistanceAntiNodeCount => singleDistanceAntiNodes.Keys.Count;
        public int AnyDistanceAntiNodeCount => anyDistanceAntiNodes.Keys.Count;

        private void CalculateAnyDistanceAntiNodes()
        {
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
                        var x = nodeLocations[ch][pt1].X;
                        var y = nodeLocations[ch][pt1].Y;

                        // 1 way
                        while (x >=0 && x <= xMax && y>=0 && y<= yMax)
                        {
                            var antiNode = new Tuple<int, int>(x, y);
                            if (!anyDistanceAntiNodes.ContainsKey(antiNode))
                            {
                                anyDistanceAntiNodes[antiNode] = 0;
                            }
                            anyDistanceAntiNodes[antiNode]++;

                            x += xDir * xDist;
                            y += yDir * yDist;

                        }
                        // and the other way
                        x = nodeLocations[ch][pt1].X - xDir * xDist;
                        y = nodeLocations[ch][pt1].Y - yDir * yDist;
                        while (x >= 0 && x <= xMax && y >= 0 && y <= yMax)
                        {
                            var antiNode = new Tuple<int, int>(x, y);
                            if (!anyDistanceAntiNodes.ContainsKey(antiNode))
                            {
                                anyDistanceAntiNodes[antiNode] = 0;
                            }
                            anyDistanceAntiNodes[antiNode]++;
                            x -= xDir * xDist;
                            y -= yDir * yDist;
                        }
                    }
                }
            }
        }

        private void CalculateSingleDistanceAntiNodes()
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
                            if (!singleDistanceAntiNodes.ContainsKey(antiNode))
                            {
                                singleDistanceAntiNodes[antiNode] = 0;
                            }
                            singleDistanceAntiNodes[antiNode]++;
                        }
                        if (nodeLocations[ch][pt2].X - (xDir * xDist) >= 0 &&
                            nodeLocations[ch][pt2].X - (xDir * xDist) <= xMax &&
                            nodeLocations[ch][pt2].Y - (yDir * yDist) >= 0 &&
                            nodeLocations[ch][pt2].Y - (yDir * yDist) <= yMax)
                        {
                            var antiNode = new Tuple<int, int>(nodeLocations[ch][pt2].X - (xDir * xDist), nodeLocations[ch][pt2].Y - (yDir * yDist));
                            if (!singleDistanceAntiNodes.ContainsKey(antiNode))
                            {
                                singleDistanceAntiNodes[antiNode] = 0;
                            }
                            singleDistanceAntiNodes[antiNode]++;
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
