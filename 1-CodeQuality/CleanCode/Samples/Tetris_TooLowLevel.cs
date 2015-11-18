using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace CleanCode.Samples
{
	class Tetris_TooLowLevel
	{
		private readonly int width;
		private readonly int height;
		private Cell[,] cells;
		private const int FILLED = 1;

		public void ClearFullLines()
		{
			for (int y = 0; y < height; y++)
			{
				int count = 0;
				int fullY = 0;
				for (int x = 0; x < width; x++)
					if (cells[x, y].getState() == FILLED)
					{
						count++;
						if (count == width) fullY = y;
					}
				if (count == width)
				{
					for (int yy = fullY; yy < height; yy++)
						for (int x = 0; x < width; x++)
							cells[x, yy] = new Cell(x, yy, cells[x, yy + 1].getState());
					for (int x = 0; x < width; x++)
						cells[x, height] = new Cell(x, height, 0);
				}

			}
		}

		public void ClearFullLines_Refactored(ref int score)
		{
			for (int lineIndex = 1; lineIndex < height + 1; lineIndex++)
			{
				if (LineIsFull(lineIndex))
				{
					score++;
					ShiftLinesDown(lineIndex);
					AddEmptyLineOnTop();
				}
			}
		}

		private void AddEmptyLineOnTop()
		{
			throw new NotImplementedException();
		}

		private void ShiftLinesDown(int lineIndex)
		{
			throw new NotImplementedException();
		}

		private bool LineIsFull(int y)
		{
			throw new NotImplementedException();
		}
	}



	class Cell
	{
		private readonly int x;
		private readonly int y;
		private readonly int state;

		public Cell()
		{
			this.x = 0;
			this.y = 0;
			this.state = 0;
		}

		public Cell(int _x, int _y, int state)
		{
			this.x = _x;
			this.y = _y;
			this.state = state;
		}

		public int getX()
		{
			return this.x;
		}

		public int getY()
		{
			return this.y;
		}

		public int getState()
		{
			return state;
		}

	}

}