using System.Collections.Generic;

namespace Hoplon.Collections
{
    public class HoplonCollectionNode
    {
        private readonly SortedDictionary<int, SortedSet<string>> dictionary = new SortedDictionary<int, SortedSet<string>>();

        // A chamada do método RemoveByValue é uma operação O(n)
        // Em um SortedDictionary, TryGetValue e Add são operações O(log(n))
        // Em um SortedSet, Add é operações O(log(n))
        // Complexidade assintótica = O(a + log(b) + log(c)) = O(n)
        public bool Add(int subIndex, string value)
        {
            RemoveByValue(value);

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
            var count = 0;

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
                end = start;
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
            var count = 0;
            var index = -1;

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

        // Loop numa coleção, é uma operação O(n)
        // Em um SortedDictionary, this[key] é uma operação O(log(n))
        // Em um SortedSet, Remove é uma operação O(log(n))
        // Complexidade assintótica = O(a + log(b) + log(c)) = O(n)
        private void RemoveByValue(string value)
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
        public bool RemoveBySubIndex(int subIndex)
        {
            return dictionary.Remove(subIndex);
        }
    }
}
