using System.Diagnostics;

class KnapsackProblem
{
    public const int knapsackCapacity = 55;
    public const int NumberOfTests = 10;
    public const int MaximumItemWeightAndValue = 10;

    // Generate a set of random items
    public static KnapsackItem[] GenerateRandomItems()
    {
        int itemCount = knapsackCapacity / 2;
        Random random = new Random();
        List<KnapsackItem> items = new List<KnapsackItem>();

        for (int i = 0; i < itemCount; i++)
        {
            items.Add(new KnapsackItem(
                random.Next(1, MaximumItemWeightAndValue + 1),
                random.Next(1, MaximumItemWeightAndValue + 1)
            ));
        }
        return items.ToArray();
    }

    public static void Main(string[] args)
    {
        // Disable garbage collection for more consistent results
        GC.TryStartNoGCRegion(1024*1024*100);

        // Solutions to be timed
        ISolution[] solutions = {
            new RecursiveBruteForce(),
            new MeetInTheMiddle(),
            new Dynamic(),
        };
        Stopwatch stopwatch = new Stopwatch();
        Random random = new Random();
        int result;

        // Dictionary of lists for storing the results
        Dictionary<string, List<Tuple<double, int>>> results = new();
        foreach (ISolution solution in solutions)
        {
            results[solution.GetName()] = new List<Tuple<double, int>>();
        }

        for (int i = 0; i < NumberOfTests; i++)
        {
            // Create random knapsack and items
            KnapsackItem[] items = GenerateRandomItems();

            foreach (ISolution solution in solutions)
            {
                // Start the stopwatch
                stopwatch.Reset();
                stopwatch.Start();
                // Run solution
                result = solution.Solve(items, knapsackCapacity);
                // Add results to the list
                stopwatch.Stop();
                results[solution.GetName()].Add(new Tuple<double, int>(
                    stopwatch.Elapsed.TotalMicroseconds,
                    result
                ));
            }
        }

        // Print the results
        foreach (var (solutionName, solutionResults) in results)
        {
            Console.WriteLine($"Results for: {solutionName}");
            foreach (var solutionResult in solutionResults) {
                Console.Write(solutionResult.Item2.ToString().PadRight(6));
                Console.WriteLine(solutionResult.Item1 + "µs");
            }
            Console.WriteLine();
        }
    }
}
