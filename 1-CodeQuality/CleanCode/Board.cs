using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CleanCode
{
	public class Board
	{
		private readonly CellContent[,] cells = new CellContent[8,8];

		public Board(TextReader inp)
		{
			for (int y = 0; y < 8; y++)
			{
				string line = inp.ReadLine();
				if (line == null) throw new Exception("incorrect input");
				for (int x = 0; x < 8; x++)
				{
					char figureSign = line[x];
					PieceColor color = Char.IsUpper(figureSign) ? PieceColor.White : PieceColor.Black;
					Set(new Location(x, y), new CellContent(Piece.FromChar(figureSign), color));
				}
			}
		}

		public IEnumerable<Location> GetPieces(PieceColor color)
		{
			return Location.AllBoard().Where(loc => Get(loc).Piece != null && Get(loc).Color == color);
		}

		public CellContent Get(Location location)
		{
			return !location.InBoard ? CellContent.Empty : cells[location.X, location.Y];
		}

		public void Set(Location location, CellContent cell)
		{
			cells[location.X, location.Y] = cell;
		}

		public override string ToString()
		{
			var b = new StringBuilder();
			for (int y = 0; y < 8; y++)
			{
				for (int x = 0; x < 8; x++)
					b.Append(Get(new Location(x, y)));
				b.AppendLine();
			}
			return b.ToString();
		}

		public Move PerformMove(Location from, Location to)
		{
			CellContent old = Get(to);
			Set(to, Get(from));
			Set(from, CellContent.Empty);
			return new Move(this, from, to, old);
		}
	}

	public class Move
	{
		private readonly Board board;
		private readonly Location from;
		private readonly Location to;
		private readonly CellContent oldDestinationCell;

		public Move(Board board, Location from, Location to, CellContent oldDestinationCell)
		{
			this.board = board;
			this.from = from;
			this.to = to;
			this.oldDestinationCell = oldDestinationCell;
		}

		public void Undo()
		{
			board.Set(from, board.Get(to));
			board.Set(to, oldDestinationCell);
		}
	}
}