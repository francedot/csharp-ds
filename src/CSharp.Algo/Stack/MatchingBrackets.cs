using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.DS.Algo.Stack
{
    public class MatchingBrackets
    {
        public bool IsValid(string s)
        {
            var bracketStack = new Stack<char>();
            var bracketsMap = new Dictionary<char, char>
            {
                { '(', ')' },
                { '[', ']' },
                { '{', '}' }
            };

            for (int i = 0; i < s.Length; i++)
            {
                // Console.WriteLine(s[i]);

                if (bracketsMap.TryGetValue(s[i], out var expected)) // opening bracket
                    bracketStack.Push(expected);
                else // closing bracket
                    if (!bracketStack.Any() || bracketStack.Pop() != s[i])
                    return false;
            }

            return !bracketStack.Any();
        }
    }
}
