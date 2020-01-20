using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkkDict;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SkkDictTest
{
    [TestClass]
    public class UnitTest1
    {
        public void f(string s, IEnumerable<Entry> entries)
        {
            Entry.GetEntries(new StringReader(s)).Should().BeEquivalentTo(entries);
        }

        [TestMethod]
        public void TestParser()
        {
            f("", new Entry[] { });
            f(";;", new Entry[] { });
            f(";;abcd", new Entry[] { });
            ((Action)(() => Entry.GetEntries(new StringReader("BadSample")).ToArray())).Should().Throw<FormatException>();
            f("りりs /凛々/凛凛/律々;=凛々しい/", new Entry[]
            {
                new Entry()
                {
                    Label = "りりs", Words = new Word[]
                    {
                        new Word("凛々"),
                        new Word("凛凛"),
                        new Word("律々", "=凛々しい"),
                    }
                }
            });
            f(@";;
あい /愛/哀/相/挨/
おん /音/温/御/恩/穏/遠/
;;
ぐん /群/郡/軍/
こと /事/異/言/殊/琴/", new Entry[]
            {
                new Entry()
                {
                    Label = "あい", Words = new Word[]
                    {
                        new Word("愛"),
                        new Word("哀"),
                        new Word("相"),
                        new Word("挨"),
                    }
                },
                new Entry()
                {
                    Label = "おん", Words = new Word[]
                    {
                        new Word("音"),
                        new Word("温"),
                        new Word("御"),
                        new Word("恩"),
                        new Word("穏"),
                        new Word("遠"),
                    }
                },
                new Entry()
                {
                    Label = "ぐん", Words = new Word[]
                    {
                        new Word("群"),
                        new Word("郡"),
                        new Word("軍"),
                    }
                },
                new Entry()
                {
                    Label = "こと", Words = new Word[]
                    {
                        new Word("事"),
                        new Word("異"),
                        new Word("言"),
                        new Word("殊"),
                        new Word("琴"),
                    }
                }
            });
        }
    }
}
