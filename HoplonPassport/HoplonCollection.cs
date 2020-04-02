using System;
using System.Collections.Generic;

namespace Hoplon.Collections
{
    class HoplonCollectionNode
    {
        private readonly Dictionary<int, SortedSet<string>> map = new Dictionary<int, SortedSet<string>>();

        public bool Add(int subIndex, string value)
        {
            SortedSet<string> values;

            if (!map.TryGetValue(subIndex, out values))
            {
                values = new SortedSet<string>();
                map.Add(subIndex, values);
            }

            return values.Add(value);
        }

        public IList<string> Get(int start, int end)
        {
            List<string> values = new List<string>();

            int[] subIndexes = new int[map.Keys.Count];
            map.Keys.CopyTo(subIndexes, 0);
            Array.Sort(subIndexes);

            foreach (var subIndex in subIndexes)
            {
                values.AddRange(map[subIndex]);
            }
            
            if (end <= -1)
            {
                end = end + values.Count - start;
            }

            end++;

            return values.GetRange(start, end);
        }


    }

    class HoplonCollection : IHoplonCollection
    {
        private readonly Dictionary<string, HoplonCollectionNode> nodes = new Dictionary<string, HoplonCollectionNode>();

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

        public IList<string> Get(string key, int start, int end)
        {
            HoplonCollectionNode node;

            if (!nodes.TryGetValue(key, out node))
            {
                return null;
            }

            return node.Get(start, end);
        }

        public long IndexOf(string key, string value)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public bool RemoveValuesFromSubIndex(string key, int subIndex)
        {
            throw new NotImplementedException();
        }
    }
}