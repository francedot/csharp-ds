namespace CSharp.DS.Algo.Strings
{
    public partial class Strings
    {
        public bool IsAnagram(string s, string t)
        {
            // TC
            // s = "anagram"
            // t = "nagaram"

            // a->3
            // n->1
            // g->1
            // r->1
            // m->1

            if (s.Length != t.Length)
                return false;

            var cMap = new int[26];

            for (var i = 0; i < s.Length; i++)
            {
                cMap[s[i] - 'a']++;
                cMap[t[i] - 'a']--;
            }

            for (var i = 0; i < cMap.Length; i++)
            {
                if (cMap[i] != 0)
                    return false;
            }

            return true;
        }
    }
}
