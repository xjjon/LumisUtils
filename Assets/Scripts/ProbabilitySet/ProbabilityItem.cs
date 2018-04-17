namespace Assets.Scripts.ProbabilitySet
{
    public class ProbabilityItem<T>
    {
        public readonly T Item;

        public readonly int ProbabilityValue;

        public ProbabilityItem(T item, int probability)
        {
            Item = item;
            ProbabilityValue = probability;
        }
    }
}
