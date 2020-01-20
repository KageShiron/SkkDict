using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SkkDict
{
    public class Word
    {
        public Word(string text, string desc = null)
        {
            this.Text = text;
            this.Desc = desc;
        }

        public string Text { get; }
        public string Desc { get; }

        private static readonly char[] deli = new[] {';'};

        public static Word Parse(string s)
        {
            var items = s.Split(deli, 2);
            if (items.Length == 1) return new Word(items[0]);
            return new Word(items[0], items[1]);
        }
    }

    public class Entry
    {
        public string Label { get; set; }
        public Word[] Words { get; set; }

        private static readonly char[] trim = " \t\r\n".ToCharArray();
        private static readonly char[] space = new[] {' '};

        public static IEnumerable<Entry> GetEntries(TextReader s)
        {
            string l;
            while ((l = s.ReadLine()) != null)
            {
                var e = Entry.Parse(l);
                if (e == null) continue;
                yield return e;
            }
        }

        public static bool TryParse(string s, out Entry res)
        {
            if (s.StartsWith(";;"))
            {
                res = null;
                return true;
            }

            s = s.TrimEnd(trim);
            var items = s.Split(space, 2);
            if (items.Length != 2)
            {
                res = null;
                return false;
            }

            res = new Entry()
            {
                Label = items[0],
                Words = items[1].Trim('/').Split('/').Select(Word.Parse).ToArray()
            };
            return true;
        }

        public static Entry Parse(string s)
        {
            if (TryParse(s, out Entry r)) return r;
            throw new FormatException("The string '" + s + "' was not recognized as a valid SkkDict Entry.");
        }

    }
}