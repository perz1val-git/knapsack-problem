// Knapsack problem solution interface
interface ISolution
{
    string GetName();
    int Solve(KnapsackItem[] items, int knapsackCapacity);
}
