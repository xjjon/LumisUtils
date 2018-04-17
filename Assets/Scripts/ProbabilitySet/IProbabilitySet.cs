namespace Assets.Scripts.ProbabilitySet
{
    public interface IProbabilitySet<T>
    {
        T RandomItem();
        void Add(T obj);
    }
}