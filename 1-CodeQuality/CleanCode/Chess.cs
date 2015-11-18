using System.IO;

namespace CleanCode
{
	public class Chess
	{
		private static Board board;
		public static string Result;

		public static void LoadFrom(StreamReader reader)
		{
			board = new Board(reader);
		}

		// Определяет мат, шах или пат белым.
		public static void SolveTask()
		{
			var isCheck = IsBad();
			var hasMoves = false;
			foreach (Location locFrom in board.GetPieces(PieceColor.White))
			{
				foreach (Location locTo in board.Get(locFrom).Piece.GetMoves(locFrom, board))
				{
					var old = board.Get(locTo);
					board.Set(locTo, board.Get(locFrom));
					board.Set(locFrom, CellContent.Empty);
					if (!IsBad())
						hasMoves = true;
					board.Set(locFrom, board.Get(locTo));
					board.Set(locTo, old);
				}
			}
			if (isCheck)
				if (hasMoves)
					Result = "check";
				else Result = "mate";
			else 
				if (hasMoves) Result = "ok";
				else Result = "stalemate";
		}

		private static bool IsBad()
		{
			bool isCheck = false;
			foreach (var loc in board.GetPieces(PieceColor.Black))
			{
				var cell = board.Get(loc);
				var moves = cell.Piece.GetMoves(loc, board);
				foreach (var destination in moves)
				{
					if (board.Get(destination).Is(PieceColor.White, Piece.King))
						isCheck = true;
				}
			}
			if (isCheck) return true;
			return false;
		}
	}
}