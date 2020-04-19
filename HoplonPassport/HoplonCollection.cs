using System.Collections.Generic;

namespace Hoplon.Collections
{
    public class HoplonCollection : IHoplonCollection
    {
        private readonly Dictionary<string, HoplonCollectionNode> nodes;

        public HoplonCollection()
        {
            nodes = new Dictionary<string, HoplonCollectionNode>();
        }

        // Em um Dictionary, TryGetValue é uma operação O(1)
        // A chamanda do método Add é uma operação O(n)
        // Complexidade assintótica = O(1 + n) = O(n)
        public bool Add(string key, int subIndex, string value)
        {
            HoplonCollectionNode node;

            if (!nodes.TryGetValue(key, out node))
            {
                node = new HoplonCollectionNode();
                nodes.Add(key, node);
            }

            return node.Add(subIndex, value);
        }

        // Em um Dictionary, TryGetValue é uma operação O(1)
        // A chamanda do método Get é uma operação O(n)
        // Complexidade assintótica = O(1 + n) = O(n)
        public IList<string> Get(string key, int start, int end)
        {
            HoplonCollectionNode node;

            if (!nodes.TryGetValue(key, out node))
            {
                return null;
            }

            return node.Get(start, end);
        }

        // Em um Dictionary, this[key] é uma operação O(1)
        // A chamada do método IndexOf é uma operação O(n)
        // Complexidade assintótica = O(1 + n) = O(n)
        public long IndexOf(string key, string value)
        {
            return nodes[key].IndexOf(value);
        }

        // Em um Dictionary, Remove é uma operação O(1)
        // Complexidade assintótica = O(1)
        public bool Remove(string key)
        {
            return nodes.Remove(key);
        }

        // Em um Dictionary, this[key] é uma operação O(1)
        // Em um SortedDictionary, Remove é uma operação O(log(n))
        // Complexidade assintótica = O(1 + log(n)) = O(log(n))
        public bool RemoveValuesFromSubIndex(string key, int subIndex)
        {
            return nodes[key].RemoveBySubIndex(subIndex);
        }
    }
}