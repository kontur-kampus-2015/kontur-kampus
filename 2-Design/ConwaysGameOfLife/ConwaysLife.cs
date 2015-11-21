using System.Collections.Generic;
using System.Linq;

namespace ConwaysGameOfLife
{
	public class ConwaysLife : IReadonlyField
	{
		private readonly int width;
		private readonly int height;
		private readonly IGameUi ui;
		private int[,] cellAge;

		public ConwaysLife(int width, int height, IGameUi ui)
		{
			this.width = width;
			this.height = height;
			this.ui = ui;
			cellAge = new int[width, height];
		}

		public int GetAge(Point pos)
		{
			return GetAge(pos.X, pos.Y);
		}

		public int GetAge(int x, int y)
		{
			return cellAge[(x + width) % width, (y + height) % height];
		}

		public void ReviveCells(params Point[] points)
		{
			foreach (var pos in points)
				cellAge[(pos.X + width) % width, (pos.Y + height) % height] = 1;
			ui.Update(this);
		}

		public void SetAge(Point pos, int age)
		{
			cellAge[(pos.X + width) % width, (pos.Y + height) % height] = age;
		}

		public void Step()
		{
			int[,] newCellAge = new int[width, height];
			for (int y = 0; y < height; y++)
				for (int x = 0; x < width; x++)
				{
					var aliveCount = GetNeighbours(x, y).Count(p => GetAge(p) > 0);
					newCellAge[x, y] = NewCellAge(cellAge[x, y], aliveCount);
					if (newCellAge[x, y] != cellAge[x, y])
						ui.Update(x, y, newCellAge[x, y]);
			}
			cellAge = newCellAge;
		}

		private int NewCellAge(int age, int aliveNeighbours)
		{
			var willBeAlive = aliveNeighbours == 3 || aliveNeighbours == 2 && age > 0;
			return willBeAlive ? age + 1 : 0;
		}

		private IEnumerable<Point> GetNeighbours(int x, int y)
		{
			yield return new Point(x - 1, y - 1);
			yield return new Point(x - 1, y);
			yield return new Point(x - 1, y + 1);
			yield return new Point(x, y - 1);
			yield return new Point(x, y + 1);
			yield return new Point(x + 1, y - 1);
			yield return new Point(x + 1, y);
			yield return new Point(x + 1, y + 1);
		}

		public override string ToString()
		{
			var rows = Enumerable.Range(0, height)
				.Select(y => 
					string.Join("",
						Enumerable.Range(0, width).Select(x => cellAge[x, y] > 0 ? "#" : " ")
				));
			return string.Join("\n", rows);
		}
	}
}