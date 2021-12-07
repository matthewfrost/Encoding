using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Encoding.Huffman
{
    public class HuffmanEncoder
    {
        public HuffmanEncoder()
        {
        }

        public void Encode(string text)
        {
            Dictionary<char, int> frequencyDict = createdFrequencyDictionary(text);
            List<HuffmanNode> tree = createTree(frequencyDict);
            var encoded = GetBitValue(text, tree);
        }

        //Returns frequency dictionary sorted in descending order
        private Dictionary<char, int> createdFrequencyDictionary(string text)
        {
            Dictionary<char, int> frequencyDict = new Dictionary<char, int>();
            char[] inputChar = text.ToCharArray();
            foreach (char c in inputChar)
            {
                int currentValue = -1;
                if (frequencyDict.TryGetValue(c, out currentValue))
                {
                    currentValue = frequencyDict[c];
                    frequencyDict[c] = ++currentValue;
                }
                else
                {
                    frequencyDict.Add(c, 1);
                }
            }

            var sortedDict = from entry in frequencyDict orderby entry.Value descending select entry;
            return sortedDict.ToDictionary(x => x.Key, y => y.Value);
        }

        private List<HuffmanNode> createTree(Dictionary<char, int> frequencyDict)
        {
            List<HuffmanNode> tree = new List<HuffmanNode>();
            foreach(KeyValuePair<char, int> entry in frequencyDict)
            {
                if(tree.Count == 0)
                {
                    tree.Add(new HuffmanNode(entry.Key, "0"));
                    continue;
                }
                if(tree.Count == 1)
                {
                    HuffmanNode node = tree[0];
                    HuffmanNode newNode = new HuffmanNode(entry.Key, null);
                    newNode.addEncodeValue(node.EncodeValue, "1");
                    node.AddRight(newNode);
                    tree.Add(newNode);
                    continue;
                }
                
                for(var i = 1; i < tree.Count; i++)
                {
                    var node = tree[i];

                    if (node.Left == null)
                    {
                        //var newEncodeValue = new StringBuilder(node.EncodeValue.ToString() + "0");
                        HuffmanNode newNode = new HuffmanNode(entry.Key, null);
                        newNode.addEncodeValue(node.EncodeValue, "0");
                        node.AddLeft(newNode);
                        tree.Add(newNode);
                        break;
                    }
                    if(node.Right == null)
                    {
                        //var newEncodeValue = new StringBuilder(node.EncodeValue.ToString() + "1");
                        HuffmanNode newNode = new HuffmanNode(entry.Key, null);
                        newNode.addEncodeValue(node.EncodeValue, "1");
                        node.AddRight(newNode);
                        tree.Add(newNode);
                        break;
                    }
                }
            }

            return tree;
        }

        private List<uint> GetBitValue(string originalValue, List<HuffmanNode> nodes)
        {
            List<uint> bits = new List<uint>();
            foreach(char c in originalValue.ToCharArray())
            {
                HuffmanNode matchingNode = nodes.Where(x => x.Value == c).FirstOrDefault();
                bits.Add(uint.Parse(matchingNode.EncodeValue, System.Globalization.NumberStyles.Any));
            }

            return bits;
        }
    }
}
