public class Dynamic : ISolution
{
    public string GetName()
    {
        return "Dynamic";
    }

    public int Solve(KnapsackItem[] items, int knapsackCapacity)
    {
        int itemCount = items.Count();
        // Arrays to store the values of items
        int[] values = new int[itemCount + 1]; 
        int[] weights = new int[itemCount + 1];

        // Copy weights and values to separate arrays
        for (int i = 0; i < itemCount; i++)
        {
            weights[i + 1] = items[i].weight;
            values[i + 1] = items[i].value;
        }

        // 2D array to store the dynamic table
        int[,] sumsTable = new int[itemCount + 1, knapsackCapacity + 1];

        // Compute the maximum value that can be achieved for each subproblem
        for (int i = 1; i <= itemCount; i++)
        {
            for (int w = 1; w <= knapsackCapacity; w++)
            {
                // If the current item can be included in the knapsack
                if (weights[i] <= w)
                {
                    // Choose the maximum value between including
                    // the current item and excluding the current item
                    sumsTable[i, w] = Math.Max(
                        values[i] + sumsTable[i - 1,
                        w - weights[i]], sumsTable[i - 1, w]
                    );
                }
                else
                {
                    // If the current item's weight is more than the current
                    // capacity, exclude the current item
                    sumsTable[i, w] = sumsTable[i - 1, w];
                }
            }
        }

        // Return the maximum value
        return sumsTable[itemCount, knapsackCapacity];
    }
}
