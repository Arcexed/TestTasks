using System;
using System.Collections.Generic;

namespace TestTasks.VowelCounting
{
    internal class StringProcessor
    {
        public (char symbol, int count)[] GetCharCount(string veryLongString, char[] countedChars)
        {
            List<ValueTuple<char, int>> tuple = new List<ValueTuple<char, int>>();
            foreach (var c in countedChars)
            {
                int count = (veryLongString.Length - veryLongString.Replace(c.ToString(), "").Length);
                tuple.Add(new ValueTuple<char, int>(c,count));
            }

            return tuple.ToArray();
        }
    }
}
