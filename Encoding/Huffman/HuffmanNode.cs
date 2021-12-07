using System;
using System.Collections;
using System.Text;

namespace Encoding.Huffman
{
    public class HuffmanNode
    {
        public char Value;
        public string EncodeValue;
        public HuffmanNode Left = null;
        public HuffmanNode Right = null;

        public HuffmanNode(char v, string? ec)
        {
            this.Value = v;
            if (ec != null)
            {
                this.EncodeValue = ec;
            }
        }

        public void addEncodeValue(string previousValue, string newValue)
        {
            StringBuilder sb = new StringBuilder(previousValue);
            sb.Append(newValue);
            this.EncodeValue = sb.ToString();
        }

        public uint getBitValue()
        {
            return uint.Parse(EncodeValue);
        }

        public void AddLeft(HuffmanNode node)
        {
            this.Left = node;
        }

        public void AddRight(HuffmanNode node)
        {
            this.Right = node;
        }
    }
}
