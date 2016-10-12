using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComUnity.Test
{
    [TestClass]
    public class HuffmanTests
    {
        [TestMethod]

        public void TestHuffman()
        {
            var t = new Huffman();
            Assert.AreEqual("BDC", t.Decode("101101", new[] { "00", "10", "01", "11" }));
            Assert.AreEqual("CBAC", t.Decode("10111010", new[] { "0", "111", "10" }));
            Assert.AreEqual("BBBABBAABBABBAAABBA", t.Decode("0001001100100111001", new[] { "1", "0" }));
            Assert.AreEqual("EGGFAC", t.Decode("111011011000100110", new[] { "010", "00", "0110", "0111", "11", "100", "101" }));
            Assert.AreEqual("DBHABBACAIAIC", t.Decode("001101100101100110111101011001011001010", new[] { "110", "011", "10", "0011", "00011", "111", "00010", "0010", "010", "0000" }));
            Assert.AreEqual("NITXOQRE",
                t.Decode("01001111010010010011001000001010001101001000010",
                new[] { "01101", "01110", "01001110", "0100110", "00010", "01000", "0101", "0000", "001001", "111", "010011111", "1010", "100", "0100111101", "00101", "01100", "00011", "010010", "1011", "0011", "1101", "0100111100", "01111", "001000", "1100" }));

            Assert.AreEqual("BBBABBAABBABBAA", t.Decode("000100110010011", new[] { "1", "0" }));
        }
    }
}
