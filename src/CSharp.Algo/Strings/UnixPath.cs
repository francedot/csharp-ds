using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp.DS.Algo.Strings
{
    public partial class Strings
    {
		/*
			Given an absolute path for a file (Unix-style), simplify it. Or in other words, convert it to the canonical path.
			In a UNIX-style file system, a period . refers to the current directory. Furthermore, a double period .. moves the directory up a level.
			Note that the returned canonical path must always begin with a slash /, and there must be only a single slash / between two directory names. The last directory name (if it exists) must not end with a trailing /. Also, the canonical path must be the shortest string representing the absolute path.
			
			Input: "/a//b////c/d//././/.."
			Output: "/a/b/c"
		 */
		public string SimplifyPath(string path)
		{
			var folderStack = new Stack<string>();
			var folders = path.Split('/', StringSplitOptions.RemoveEmptyEntries)
							  .Where(x => x != ".");

			foreach (var folder in folders)
			{
				if (folder == ".." && folderStack.Any())
					folderStack.Pop();
				else if (folder != "..")
					folderStack.Push(folder);
			}

			return "/" + string.Join('/', folderStack.Reverse());
		}


		public string GetRelativePath(string dirPath, string filePath)
		{
			// (c:\a\b\c, c:\a\b\c\d\file.txt)
			// (c:\a\b\x\d\sa\, c:\a\b\c\d\file.txt) => ..\c\d\file.txt
			// (c:\a\x\c\d, c:\a\b\c\d\file.txt ) => ..\..\..\b\c\d\file.txt
			// (c:\a\b\..\x, c:\a\b\d\file.txt)

			// First Simplify Paths

			var dirPathLevels = dirPath.Split("\\");
			var filePathLevels = filePath.Split("\\");

			int lastMatchingLevel = 0;
			while (lastMatchingLevel < dirPathLevels.Length
					&& dirPathLevels[lastMatchingLevel] == filePathLevels[lastMatchingLevel])
				lastMatchingLevel++;

			var diffLevel = dirPathLevels.Length - 1 - lastMatchingLevel; // 0
			var result = "";
			if (diffLevel == 0)
				result += ".";
			else
				for (var i = diffLevel + 1; i < dirPathLevels.Length; i++)
					result += "..\\";

			for (var i = diffLevel + 1; i < filePathLevels.Length; i++)
				result += "\\" + filePathLevels[i]; // \d\file.path

			return result;
		}
	}
}
