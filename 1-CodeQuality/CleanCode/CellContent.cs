namespace CleanCode
{
    public enum PieceColor
    {
        Black,
        White
    }
	public class CellContent
	{
		public static readonly CellContent Empty = new CellContent(null, PieceColor.White);
		public readonly PieceColor Color;
		public readonly Piece Piece;

		public CellContent(Piece piece, PieceColor color)
		{
			Piece = piece;
			Color = color;
		}

		public bool Is(PieceColor color, Piece piece)
		{
			return Piece == piece && Color == color;
		}

		public override string ToString()
		{
			string c = Piece == null ? " ." : " " + Piece.Sign;
			return Color == PieceColor.Black ? c.ToLower() : c;
		}
	}
}