using System;
using System.Collections.Generic;

namespace Hoplon.Collections
{
    class HoplonCollectionNode
    {
        private readonly SortedDictionary<int, SortedSet<string>> dictionary = new SortedDictionary<int, SortedSet<string>>();

        // Loop numa coleção, é uma operação O(n)
        // Em um SortedDictionary, this[key] é uma operação O(log(n))
        // Em um SortedSet, Remove é uma operação O(log(n))
        // Complexidade assintótica = O(a + log(b) + log(c)) = O(n)
        private void Remove(string value)
        {
            foreach (var key in dictionary.Keys)
            {
                var deleted = dictionary[key].Remove(value);
                if (deleted)
                {
                    break;
                }
            }

        }

        // Em um SortedDictionary, Remove é uma operação O(log(n))
        // Complexidade assintótica = O(log(n))
        public bool Remove(int subIndex)
        {
            return dictionary.Remove(subIndex);
        }

        // A chamada do método Remove é uma operação O(n)
        // Em um SortedDictionary, TryGetValue e Add são operações O(log(n))
        // Em um SortedSet, Add é operações O(log(n))
        // Complexidade assintótica = O(a + log(b) + log(c)) = O(n)
        public bool Add(int subIndex, string value)
        {
            Remove(value);

            SortedSet<string> values;

            if (!dictionary.TryGetValue(subIndex, out values))
            {
                values = new SortedSet<string>();
                dictionary.Add(subIndex, values);

            }

            return values.Add(value);
        }

        // Aqui apesar de haver um nested loop, uma coleção não está interando sobre a outra
        // Complexidade assintótica = O(a + b) = O(n)
        public IList<string> Get(int start, int end)
        {
            int count = 0;

            foreach (var subIndex in dictionary.Keys)
            {
                count += dictionary[subIndex].Count;
            }

            if (start < 0)
            {
                start = 0;
            }

            if (end >= count)
            {
                end = count - 1;
            }
            else if (end <= -1)
            {
                end += count;
            }

            if (end <= 0)
            {
                end = 1;
            }

            var values = new List<string>();
            var index = 0;

            foreach (var subIndex in dictionary.Keys)
            {
                foreach (var v in dictionary[subIndex])
                {
                    if (index > end)
                    {
                        break;
                    }

                    if (index >= start && index <= end)
                    {
                        values.Add(v);
                    }

                    index++;
                }
            }

            return values;
        }

        // Mesmo caso do Get, apesar de haver um nested loop, uma coleção não está interando sobre a outra
        // Complexidade assintótica = O(a + b) = O(n)
        public long IndexOf(string value)
        {
            int count = 0;
            int index = -1;

            foreach (var subIndex in dictionary.Keys)
            {
                foreach (var v in dictionary[subIndex])
                {
                    if (value.Equals(v))
                    {
                        return count;
                    }

                    count++;
                }
            }

            return index;
        }
    }

    class HoplonCollection : IHoplonCollection
    {
        private readonly Dictionary<string, HoplonCollectionNode> nodes = new Dictionary<string, HoplonCollectionNode>();

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
            return nodes[key].Remove(subIndex);
        }
    }
}