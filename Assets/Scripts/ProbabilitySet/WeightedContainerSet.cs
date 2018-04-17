using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Assets.Scripts.ProbabilitySet
{
    public class WeightedSet<T> : IProbabilitySet<T>
    {
        public List<ProbabilityItem<T>> Items { get; private set; }

        private int _totalWeights;

        public WeightedSet()
        {
            Items = new List<ProbabilityItem<T>>();
            CalculateWeights();
        }

        private void CalculateWeights()
        {
            _totalWeights = 0;
            Items.ForEach(i => _totalWeights += i.ProbabilityValue);
        }

        public T RandomItem()
        {
            if (Items.Count == 0 || _totalWeights == 0)
            {
                return default(T);
            }

            var randomWeight = Random.Range(0, _totalWeights + 1);
            var currentWeight = 0;

            foreach (var probabilityItem in Items)
            {
                currentWeight += probabilityItem.ProbabilityValue;
                if (currentWeight >= randomWeight) { return probabilityItem.Item; }
            }
            return Items[0].Item;
        }

        public void Add(T obj, int weight)
        {
            if(weight <= 0) { throw new ArgumentException("Probability weight must be greater than 0."); }
            Items.Add(new ProbabilityItem<T>(obj, weight));
            _totalWeights += weight;
        }

        public void Add(T obj)
        {
            Add(obj, 1);
        }
    }
}
