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

                    }

                    // update
                    if (thisLine.Any(l => l == ','))
                    {
                        updates.Add(thisLine.Split(',').Select(l => int.Parse(l)).ToArray());
                    }
                }
            }


            // for each update, check if the update is valid
            List<int[]> validUpdates = [];
            List<int[]> invalidUpdates = [];
            List<int[]> fixedUpdates = [];

            foreach (var update in updates)
            {
                // build a pageOrderRules object for this update
                // the input data has rules that create a loop, but updatesb themselves do not ever 
                var pageOrderRules = new PageOrderRules();
                foreach (var page in update)
                {
                    // don't need to add upper rules, only lower rules as otherwise there will be duplicates
                    foreach (var rule in rules[page].GetLowerList())
                    {
                        pageOrderRules.AddRule(rule, page);
                    }

                }
                if (pageOrderRules.IsValidOrder(update))
                {
                    validUpdates.Add(update);
                }
                else
                {
                    invalidUpdates.Add(update);
                    fixedUpdates.Add(pageOrderRules.GetValidOrder(update));
                }

            }

            var validSum = validUpdates.Select(u => u.Skip((u.Length - 1) / 2).Take(1).First()).Sum();
            var fixedSum = fixedUpdates.Select(u => u.Skip((u.Length - 1) / 2).Take(1).First()).Sum();
            Console.WriteLine($"Sum of the middle numbers of the valid updates is {validSum}");
            Console.WriteLine($"Sum of the middle numbers of the fixed updates is {fixedSum}");



        }
    }
}
