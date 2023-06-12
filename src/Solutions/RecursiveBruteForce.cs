public class RecursiveBruteForce : ISolution
{
    public string GetName()
    {
        return "Brute force";
    }

    private KnapsackItem[] _items = {};

    // Recursive function for checking all combinations
    private int SolveRecursively(int leftoverCapacity, int currentItem = 0)
    {
        // Return 0 if there's no more elements, or if the knapsack is full
        if (_items.Length <= currentItem || leftoverCapacity <= 0)
        {
            return 0;
        }
        // If the item can fit, consider an option with and without it
        if (_items[currentItem].weight <= leftoverCapacity)
        {
            // Choose the best option, with or without the current item
            return Math.Max(
                SolveRecursively(
                    leftoverCapacity - _items[currentItem].weight,
                    currentItem + 1
                ) + _items[currentItem].value,
                SolveRecursively(leftoverCapacity, currentItem + 1)
            );
        }
        // ...otherwise don't include the item
        return SolveRecursively(leftoverCapacity, currentItem + 1);
    }

    public int Solve(KnapsackItem[] items, int knapsackCapacity)
    {
        _items = items;

        return SolveRecursively(knapsackCapacity);
    }
}
