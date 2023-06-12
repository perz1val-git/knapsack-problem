public class MeetInTheMiddle : ISolution
{
    public string GetName()
    {
        return "Meet in the Middle";
    }
    public int Solve(KnapsackItem[] items, int knapsackCapacity)
    {
        int count = items.Count();
        int half = count / 2;

        // Generate all sums/combinations of items for each half
        // The second list is additionaly sorted by weight
        List<(int, int)> firstHalfSums = GenerateAllSums(items, half, 0);
        List<(int, int)> secondHalfSums = GenerateAllSums(items, count - half, half)
            .OrderBy(x => x.Item1)
            .ToList();

        // Filter the second half by picking subsets with the highest value
        // for the given weight - simple since the list is sorted
        int maxSoFar = 0;
        List<(int, int)> filteredSecondHalfSums = new();
        for (int i = 0; i < secondHalfSums.Count; i++)
        {
            if (maxSoFar < secondHalfSums[i].Item2)
            {
                filteredSecondHalfSums.Add(secondHalfSums[i]);
                maxSoFar = secondHalfSums[i].Item2;
            }
        }
        secondHalfSums = filteredSecondHalfSums;

        int maxProfit = 0;

        // Iterate through each sum in the first half,
        // this is the first step in merging both halves
        for (int i = 0; i < firstHalfSums.Count; i++)
        {
            // Find the remaining weight and value needed to reach the knapsack capacity
            int remainingCapacity = knapsackCapacity - firstHalfSums[i].Item1;
            if (remainingCapacity >= 0)
            {
                // Find the remaining value
                int remainingValue = FindRemainingValue(secondHalfSums, remainingCapacity);

                // Calculate the total value for the current combination
                int totalValue = firstHalfSums[i].Item2 + remainingValue;

                // Update the maximum profit if the total value exceeds the current maximum
                maxProfit = Math.Max(maxProfit, totalValue);
            }
        }

        return maxProfit;
    }

    // Generates all sums for the given subset
    // this is similar to the bruteforce algorithm,
    // but is performed on a subset of elements,
    // thus the number of combinations is greatly decreased
    private List<(int, int)> GenerateAllSums(KnapsackItem[] items, int count, int offset)
    {
        List<(int, int)> sums = new(){(0, 0)};

        for (Int128 i = 0; i < ((Int128)1 << count); i++)
        {
            int weight = 0;
            int value = 0;

            for (int j = 0; j < count; j++)
            {
                if ((i & ((Int128)1 << j)) != 0)
                {
                    weight += items[j + offset].weight;
                    value += items[j + offset].value;
                }
            }
            if (weight > 0)
            {
                sums.Add((weight, value));
            }
        }

        return sums;
    }

    // Finds the most valuable subset of items for the given weight
    private int FindRemainingValue(List<(int, int)> sums, int remainingWeight)
    {
        int left = 0;
        int right = sums.Count - 1;
        int remainingValue = 0;

        // Binary search (sums is sorted)
        while (left <= right)
        {
            int mid = (left + right) / 2;

            if (sums[mid].Item1 <= remainingWeight)
            {
                remainingValue = Math.Max(remainingValue, sums[mid].Item2);
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return remainingValue;
    }
}
