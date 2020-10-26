using System.Collections.Generic;
using System.Linq;

namespace CSharp.Algo.Design
{
    public class FileSystem
    {
        // Visual: https://i.imgur.com/Gv8zOHW.png
        public class File
        {
            public bool isFile = false;
            public Dictionary<string, File> files = new Dictionary<string, File>();
            public string content = "";
        }

        public File root;
        public FileSystem()
        {
            root = new File();
        }

        // O(M + N + KLogK)
        // M = Input string length
        // N = depth of the last directory level
        // K refers to the number of entries(files+subdirectories) in the last level directory(in the current input).
        // We need to sort these names giving a factor of klog(k)klog(k).
        public IList<string> Ls(string path)
        {
            File t = root;
            var result = new List<string>();
            if (path != "/")
            {
                var parts = path.Split("/");
                for (int i = 1; i < parts.Length; i++)
                    t = t.files[parts[i]];
                if (t.isFile)
                {
                    result.Add(parts[parts.Length - 1]);
                    return result;
                }
            }

            result.AddRange(t.files.Keys.OrderBy(x => x));
            return result;
        }

        // O(M+N)
        // M = Input string length
        // N refers to the depth of the last directory level in the mkdir input
        public void Mkdir(string path)
        {
            File t = root;
            var parts = path.Split("/");
            for (int i = 1; i < parts.Length; i++)
            {
                if (!t.files.ContainsKey(parts[i]))
                    t.files.Add(parts[i], new File());
                t = t.files[parts[i]];
            }
        }

        // O(M+N)
        // M = Input string length
        // N refers to the depth of the file name in the current input.
        public void AddContentToFile(string filePath, string content)
        {
            File t = root;
            var parts = filePath.Split("/");
            for (int i = 1; i < parts.Length - 1; i++)
                t = t.files[parts[i]];

            if (!t.files.ContainsKey(parts[^1]))
                t.files.Add(parts[^1], new File());
            t = t.files[parts[^1]];
            t.isFile = true;
            t.content += content;
        }

        // O(M+N)
        // M = Input string length
        // N refers to the depth of the file name in the current input.
        public string ReadContentFromFile(string filePath)
        {
            File t = root;
            var parts = filePath.Split("/");
            for (int i = 1; i < parts.Length - 1; i++)
                t = t.files[parts[i]];
            
            return t.files[parts[^1]].content;
        }
    }
}
