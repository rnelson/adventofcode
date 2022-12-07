using System.Text.RegularExpressions;
using advent2022.Share;

namespace advent2022.Solutions;

public class Day05 : DayBase
{
	private const string parseStepPattern = @"move (\d+) from (\d+) to (\d+)";
	
	/// <inheritdoc />
	public override (object, object) Solve()
	{
		#region Part A
		var (stacksA, steps) = Parse(Input!.ToList());
		
		foreach (var (count, source, destination) in steps)
		{
			for (var i = 0; i < count; i++)
				stacksA.ElementAt(destination - 1).Push(stacksA.ElementAt(source - 1).Pop());
		}
		
		var topsA = stacksA.Aggregate(string.Empty, (current, stack) => current + stack.Peek());
		#endregion Part A
		
		#region Part B
		var (stacksB, _) = Parse(Input!.ToList());

		foreach (var (count, source, destination) in steps)
		{
			var sourceStack = stacksB.ElementAt(source - 1);
			var destinationStack = stacksB.ElementAt(destination - 1);

			sourceStack.MoveInOrder(destinationStack, count);
		}
		
		var topsB = stacksB.Aggregate(string.Empty, (current, stack) => current + stack.Peek());
		#endregion Part B

		return (topsA, topsB);
	}

	private (List<Stack<char>>, List<Tuple<int, int, int>>) Parse(IEnumerable<string> input)
	{
		var stackLines = new List<string>();
		var stacks = new List<Stack<char>>();

		// Find the messy ones that we need to deal with
		foreach (var line in Input!)
		{
			if (line.Contains("["))
				stackLines.Add(line);
			else
				break;
		}

		var skip = stackLines.Count + 2; // picture + stack numbers + blank line
		var stackCount = stackLines.Last().Count(c => c == '[');
		var parser = new Regex(parseStepPattern);

		#region Parse the stacks
		for (var stackNumber = 1; stackNumber <= stackCount; stackNumber++)
		{
			var stack = new Stack<char>();
			var pos = 1 + (stackNumber - 1) * 4;

			for (var i = stackLines.Count - 1; i >= 0; i--)
			{
				var line = stackLines.ElementAt(i);
				var c = line[pos];

				if (c == ' ')
					break;
				
				stack.Push(c);
			}

			stacks.Add(stack);
		}
		#endregion Parse the stacks
		
		// Parse the steps
		var steps = (
			from line in input.Skip(skip)
			select parser.Matches(line) into matches
			let count = int.Parse(matches[0].Groups[1].Value)
			let source = int.Parse(matches[0].Groups[2].Value)
			let destination = int.Parse(matches[0].Groups[3].Value)
			select new Tuple<int, int, int>(count, source, destination)
		).ToList();

		return (stacks, steps);
	}
}