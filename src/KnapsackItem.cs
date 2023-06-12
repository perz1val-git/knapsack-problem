// Knapsack item, has weigh and value
public readonly struct KnapsackItem
{
    public int weight { get; init; }
    public int value { get; init; }

    public KnapsackItem(int weight, int value)
    {
        this.weight = weight;
        this.value = value;
    }
}
