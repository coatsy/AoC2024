namespace Day5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, PageOrderRule> rules = [];
            List<int[]> updates = [];
            using (var sr = new StreamReader("./input/input.txt"))
            {
                string thisLine;
                while ((thisLine = sr.ReadLine()) != null)
                {
                    // page order rule
                    if (thisLine.Any(l => l == '|'))
                    {
                        var theRule = thisLine.Split('|');
                        if (!rules.ContainsKey(int.Parse(theRule[0])))
                        {
                            rules.Add(int.Parse(theRule[0]), new PageOrderRule());
                        }
                        rules[int.Parse(theRule[0])].AddHigher(int.Parse(theRule[1]));
                        if (!rules.ContainsKey(int.Parse(theRule[1])))
                        {
                            rules.Add(int.Parse(theRule[1]), new PageOrderRule());
                        }
                        rules[int.Parse(theRule[1])].AddLower(int.Parse(theRule[0]));
                    }

                    // update
                    if (thisLine.Any(l => l == ','))
                    {
                        updates.Add(thisLine.Split(',').Select(l => int.Parse(l)).ToArray());
                    }
                }
            }

            // for each update, check if the update is valid
            int middleSum = 0;
            foreach (var update in updates)
            {
                var valid = true;
                for (var i = 0; i < update.Length; i++)
                {
                    for (var j = i+1; j < update.Length; j++)
                    {
                        if (!rules.ContainsKey(update[i]) || !rules.ContainsKey(update[j]))
                        {
                            valid = false;
                        }
                        else
                        if (!rules[update[i]].IsInHigherList(update[j]) || !rules[update[j]].IsInLowerList(update[i]))
                        {
                            valid = false;
                        }
                        if (!valid) break;
                    }
                    if (!valid) break;
                }
                if (valid) middleSum += update.Skip((update.Length-1)/2).Take(1).First();
            }

            Console.WriteLine($"Sum of the middle numbers of the valid updates is {middleSum}");
        }
    }
}
