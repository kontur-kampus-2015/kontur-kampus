using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace CleanCode.Samples
{
	class Field
	{
		public readonly int Height, Width;
		public readonly int Score;
		private readonly ImmutableArray<ImmutableHashSet<int>> filledCellsLineByLine;
		public Field(int width, int height, ImmutableArray<ImmutableHashSet<int>> filledCellsLineByLine, int score = 0)
		{
			Width = width;
			Height = height;
			Score = score;
			this.filledCellsLineByLine = filledCellsLineByLine;
		}

		public Field ClearFullLines()
		{
			var notFullLines = GetAllNotFullLines();
			var clearedLinesCount = Height - notFullLines.Count;
			var newLinesArray = CreateNewLinesArray(clearedLinesCount, notFullLines);
			return new Field(Width, Height, newLinesArray, Score + clearedLinesCount);
		}

		private ImmutableArray<ImmutableHashSet<int>> CreateNewLinesArray(int emptyLinesCount, List<ImmutableHashSet<int>> nonEmptyLines)
		{
			return
				Enumerable.Repeat(ImmutableHashSet.Create<int>(), emptyLinesCount)
					.Concat(nonEmptyLines)
					.ToImmutableArray();
		}

		private List<ImmutableHashSet<int>> GetAllNotFullLines()
		{
			return filledCellsLineByLine.Where(line => line.Count != Width).ToList();
		}
	}
}