using System.Text;

namespace CSharp.Algo.Strings
{
    public partial class Strings
    {
        public string ReverseWords(string s)
        {
            // Visualization: https://i.imgur.com/FXOgxuV.png
            // converst string to string builder 
            // and trim spaces at the same time
            StringBuilder sb = TrimSpaces(s);

            // reverse the whole string
            Reverse(sb, 0, sb.Length - 1);

            // reverse each word
            ReverseEachWord(sb);

            return sb.ToString();
        }

        private StringBuilder TrimSpaces(string s)
        {
            int left = 0, right = s.Length - 1;
            // remove leading spaces
            while (left <= right && s[left] == ' ') ++left;

            // remove trailing spaces
            while (left <= right && s[right] == ' ') --right;

            // reduce multiple spaces to single one
            StringBuilder sb = new StringBuilder();
            while (left <= right)
            {
                char c = s[left];

                if (c != ' ') sb.Append(c);
                else if (sb[^1] != ' ') sb.Append(c);

                ++left;
            }

            return sb;
        }

        private void Reverse(StringBuilder sb, int left, int right)
        {
            while (left < right)
            {
                char tmp = sb[left];
                sb[left++] = sb[right];
                sb[right--] = tmp;
            }
        }

        private void ReverseEachWord(StringBuilder sb)
        {
            int n = sb.Length;
            int start = 0, end = 0;

            while (start < n)
            {
                // go to the end of the word
                while (end < n && sb[end] != ' ') ++end;
                // reverse the word
                Reverse(sb, start, end - 1);
                // move to the next word
                start = end + 1;
                ++end;
            }
        }
    }
}
