using System;
using Encoding.Huffman;
namespace Encoding
{
    class Program
    {
        static void Main(string[] args)
        {
            HuffmanEncoder encoder = new HuffmanEncoder();
            encoder.Encode("added");
        }
    }
}
