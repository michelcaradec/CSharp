using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

public class Program
{
	// http://www.codeproject.com/Articles/21080/In-Depth-with-RegEx-Matching-Nested-Constructions
	// http://www.codeproject.com/Articles/21183/In-Depth-with-NET-RegEx-Balanced-Grouping
	public static void Main()
	{
		const string formula = @"=Date(Year(A$5),Month(A$5),1)-(Weekday(Date(Year((A$5+1)),Month(A$5),1))-1)+{0;1;2;3;4;5}*7+{1,2,3,4,5,6,7}-1";
		var functions = GetExcelFunctions(formula);
		foreach (var function in functions)
		{
			Console.WriteLine(function);
		}
	}

	private static IEnumerable<Tuple<string, int>> GetExcelFunctions(string formula)
	{
		const string pattern = @"(?<function>[a-z][a-z0-9]*)\(";
		var matches = Regex.Matches(formula, pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant);
		return matches
			.Cast<Match>()
			.Select(m => m.Groups["function"].Value)
			.GroupBy(f => f)
			.Select(g => new Tuple<string, int>(g.Key, g.Count()));
	}
}
