namespace Day5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, PageOrderRule> rules = [];
            List<int[]> updates = [];
            List<int> lowers = [];
            List<int> highers = [];
            PageOrderRules pageOrderRules = new();

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
                        highers.Add(int.Parse(theRule[1]));
                        if (!rules.ContainsKey(int.Parse(theRule[1])))
                        {
                            rules.Add(int.Parse(theRule[1]), new PageOrderRule());
                        }
                        rules[int.Parse(theRule[1])].AddLower(int.Parse(theRule[0]));
                        lowers.Add(int.Parse(theRule[0]));

                        pageOrderRules.AddRule(int.Parse(theRule[0]), int.Parse(theRule[1]));
                    }

                    // update
                    if (thisLine.Any(l => l == ','))
                    {
                        updates.Add(thisLine.Split(',').Select(l => int.Parse(l)).ToArray());
                    }
                }
            }

            var validUpdates = updates.Where(u => pageOrderRules.IsValidOrder(u));
            var validSum = validUpdates.Select(u => u.Skip((u.Length - 1) / 2).Take(1).First()).Sum();

            var invalidUpdates = updates.Where(u => !pageOrderRules.IsValidOrder(u));
            var fixedUpdates = invalidUpdates.Select(u => pageOrderRules.GetValidOrder(u));
            var fixedSum = fixedUpdates.Select(u => u.Skip((u.Length - 1) / 2).Take(1).First()).Sum();

            Console.WriteLine($"Sum of the middle numbers of the valid updates is {validSum}");
            Console.WriteLine($"Sum of the middle numbers of the fixed updates is {fixedSum}");

/*
            // for each update, check if the update is valid
            int middleSum = 0;
            List<int[]> invalidUpdates = [];

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
                else invalidUpdates.Add(update);
            }

            // build the ordered list of pages
            List<int> orderedPages = [];
            // get the first page (does not appear in uppers)
            var firstPage = lowers.Where(l => !highers.Contains(l)).First();
            orderedPages.Add(firstPage);
            lowers.RemoveAll(l => l == firstPage);
            // now loop through lowers and get the rest of the pages
            while (lowers.Count > 0)
            {
                var nextPage = lowers.Where(l => !rules[l].IsInHigherList(orderedPages.Last())).First();
                orderedPages.Add(nextPage);
                lowers.RemoveAll(l => l == nextPage);
            }
            //while (orderedPages.Count < rules.Count)
            //{
            //    var p = rules.Keys.Where(ky=>!orderedPages.Contains(ky)).Select(ky =>
            //        rules.Where(r => !r.Value.IsInHigherList(ky)).First().Key).First();
            //    orderedPages.Add(p);
            //}

            List<int[]> fixedUpdates = [];
            foreach (var update in invalidUpdates)
            {
                // build the proper list of pages for this update
                var newUpdate = orderedPages.Where(p => update.Contains(p)).ToArray();
                fixedUpdates.Add(newUpdate);
            }

            var fixedMiddleSum = fixedUpdates.Select(u => u.Skip((u.Length - 1) / 2).Take(1).First()).Sum();

            Console.WriteLine($"Sum of the middle numbers of the valid updates is {middleSum}");
            Console.WriteLine($"Sum of the middle numbers of the fixed updates is {fixedMiddleSum}");

            */
        }
    }
}
