using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComUnity
{
 /*
* When text is Huffman encoded, each symbol is replaced by a string of 0's and 1's called a bit string.
* The replacement is done in such a way that the bit string of a symbol is never the prefix of the bit string of another symbol.
* You will be given a String archive and a String[] dictionary.
* The i-th element of dictionary will be the bit string of the i-th uppercase letter.
* Decode the archive using the dictionary and return the result as a single String.
* Below is a C# class with a method Decode and a test TestHuffman. Implement the Decode method so that all the tests pass.
*/

    public class Huffman
    {

        public string Decode(string archive, string[] dictionary)
        {
            var tree = BuildTree(dictionary);
            return Decode(archive, tree);
        }

        const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        string Decode(string archive, Node tree)
        {
            if (string.IsNullOrEmpty(archive))
            {
                throw new ArgumentNullException(nameof(tree));
            }
            if (tree == null)
            {
                throw new ArgumentNullException(nameof(tree));
            }

            var result = new StringBuilder();
            var values = archive.Select(x => x == '1' ? true : false);
            var current = tree;
            foreach (bool value in values)
            {
                if (ParseArchiveValue(ref result, ref current, value))
                {
                    current = tree; //found a leaf, reset to root node
                }
            }

            return result.ToString();
        }

        static bool ParseArchiveValue(ref StringBuilder result, ref Node current, bool value)
        {
            if (current[value] == null)
            {
                throw new ArgumentException("Badly formed archive argument or invalid dictionary: Too many steps");
            }

            current = current[value];
            if (current.IsLeaf)
            {
                result.Append(current.Token);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Reverse engineer a Huffman tree from a dictionary of known frequency values / indices
        /// </summary>
        /// <param name="dictionary">Array of bit strings in "alphabetical order" that represent the huffman index of each character</param>
        /// <returns>A Node representing the Tree</returns>
        Node BuildTree(string[] dictionary)
        {
            //TODO: Figure out a way to combine dictionary with alphabet into a sorted dictionary in a more concise fashion
            var map = new SortedDictionary<string, char>();
            for (int i = 0; i < dictionary.Length; i++)
            {
                map.Add(dictionary[i], alphabet[i]);
            }

            var root = new Node('~', string.Empty);
            foreach (var item in map)
            {
                var node = new Node(item.Value, item.Key);
                root.Merge(node);
            }

            return root;
        }

        /// <summary>
        /// Psuedo Huffman tree node, used to reverse populate a "Huffman tree"
        /// </summary>
        class Node
        {
            internal char Token { get; private set; }
            internal Node Left { get; private set; }
            internal Node Right { get; private set; }
            internal bool IsLeaf { get; private set; }

            /// <summary>
            /// Makes accessing the left and right nodes programatically easier, but sacrifices readability
            /// </summary>
            /// <param name="direction"></param>
            /// <returns></returns>
            public Node this[bool direction]
            {
                get {
                    if (direction)
                    {
                        return Right;
                    }
                    return Left;
                }
                set {
                    if (direction)
                    {
                        Right = value;
                        return;
                    }
                    Left = value;
                }
            }

            public Node(char token, string bitString)
            {
                if (bitString.Length == 0)
                {
                    //we've reached the end of the branch, set the token
                    Token = token;
                    IsLeaf = true;
                    return;
                }

                this[bitString.StartsWith("1")] = new Node(token, bitString.Remove(0, 1));
            }

            /// <summary>
            /// Combine a Node into this instance, does not overwrite existing Nodes
            /// </summary>
            /// <param name="value"></param>
            public void Merge(Node value)
            {
                //TODO: Make this nicer, it smells...
                if (value.Left != null && Left != null)
                {
                    Left.Merge(value.Left);
                }
                if (value.Left != null && Left == null)
                {
                    Left = value.Left;
                }
                if (value.Right != null && Right != null)
                {
                    Right.Merge(value.Right);
                }
                if (value.Right != null && Right == null)
                {
                    Right = value.Right;
                }
            }
        }
    }
}
