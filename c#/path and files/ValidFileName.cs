﻿using System.IO;
using System.Text.RegularExpressions;

namespace TCLight.ApiDataPopulator.Helpers
{
	public class ValidFileName
	{
		/// <summary>
		/// Strip illegal chars and reserved words from a candidate filename (should not include the directory path)
		/// </summary>
		/// <remarks>
		/// https://stackoverflow.com/a/12924582/1081724
		/// </remarks>
		private static string CoerceValidFileName(string filename)
		{
			var invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
			var invalidReStr = $@"[{invalidChars}]+";

			var reservedWords = new[]
			{
				"CON", "PRN", "AUX", "CLOCK$", "NUL", "COM0", "COM1", "COM2", "COM3", "COM4",
				"COM5", "COM6", "COM7", "COM8", "COM9", "LPT0", "LPT1", "LPT2", "LPT3", "LPT4",
				"LPT5", "LPT6", "LPT7", "LPT8", "LPT9"
			};

			var sanitisedNamePart = Regex.Replace(filename, invalidReStr, "_");
			foreach(var reservedWord in reservedWords)
			{
				var reservedWordPattern = $"^{reservedWord}\\.";
				sanitisedNamePart = Regex.Replace(sanitisedNamePart, reservedWordPattern, "_reservedWord_.", RegexOptions.IgnoreCase);
			}

			return sanitisedNamePart;
		}
	}
}