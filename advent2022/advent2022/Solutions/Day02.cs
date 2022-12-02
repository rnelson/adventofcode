namespace advent2022.Solutions;

public class Day02 : DayBase
{
	/// <inheritdoc />
	public override (object, object) Solve()
	{
		var scoreA = 0;
		var scoreB = 0;

		foreach (var line in Input!)
		{
			var (os, ps) = Parse(line);
			
			scoreA += ScoreA(os, ps);
			scoreB += ScoreB(os, ps);
		}
		
		return (scoreA, scoreB);
	}

	private (OpponentShape, PlayerShape) Parse(string input)
	{
		var shapes = input.Split(' ');
		Enum.TryParse(shapes[0], out OpponentShape os);
		Enum.TryParse(shapes[1], out PlayerShape ps);

		return (os, ps);
	}

	private int ScoreA(OpponentShape os, PlayerShape ps)
	{
		var won = IsWinner(os, ps);
		var draw = (int)os == (int)ps;

		return (won ? 6 : 0) + (draw ? 3 : 0) + (int)ps;
	}

	private int ScoreB(OpponentShape os, PlayerShape ps)
	{
		var requiredShape = PlayerShape.X;

		requiredShape = ps switch
		{
			PlayerShape.X => os switch
			{
				OpponentShape.A => PlayerShape.Z,
				OpponentShape.B => PlayerShape.X,
				OpponentShape.C => PlayerShape.Y,
				_ => requiredShape
			},
			PlayerShape.Y => os switch
			{
				OpponentShape.A => PlayerShape.X,
				OpponentShape.B => PlayerShape.Y,
				OpponentShape.C => PlayerShape.Z,
				_ => requiredShape
			},
			PlayerShape.Z => os switch
			{
				OpponentShape.A => PlayerShape.Y,
				OpponentShape.B => PlayerShape.Z,
				OpponentShape.C => PlayerShape.X,
				_ => requiredShape
			},
			_ => requiredShape
		};

		var won = IsWinner(os, requiredShape);
		var draw = (int)os == (int)requiredShape;

		return (won ? 6 : 0) + (draw ? 3 : 0) + (int)requiredShape;
	}

	private bool IsWinner(OpponentShape os, PlayerShape ps)
	{
		return os switch
		{
			OpponentShape.A => ps == PlayerShape.Y,
			OpponentShape.B => ps == PlayerShape.Z,
			OpponentShape.C => ps == PlayerShape.X,
			_ => false
		};
	}

	private enum OpponentShape
	{
		A = 1, // Rock
		B, // Paper
		C // Scissors
	}

	private enum PlayerShape
	{
		X = 1, // Rock
		Y, // Paper
		Z // Scissors
	}
}