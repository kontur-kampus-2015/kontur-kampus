using System.IO;
using System.Linq;

namespace CleanCode
{
    public class Chess
    {
        private Board _board;


        // Определяет мат, шах или пат белым.
        public string GetStringForWhiteKing(StreamReader reader)
        {
            _board = new Board(reader);

            var isWhiteKingUnderAttack = IsWhiteKingUnderAttack();
            var whiteKingHasMoves = WhiteKingSafeMone();
            return GetStrinForWhiteKingResult(isWhiteKingUnderAttack, whiteKingHasMoves);
        }

        private bool IsWhiteKingUnderAttack()
        {
            return (
                from location in _board.GetPieces(PieceColor.Black)
                let locationBlack = _board.Get(location)
                select locationBlack.Piece.GetMoves(location, _board))
                .Any(movesBlack => movesBlack.Any(destination => _board.Get(destination)
                    .Is(PieceColor.White, Piece.King)));
        }

        private string GetStrinForWhiteKingResult(bool isWhiteKingUnderAttack, bool whiteKingHasMoves)
        {
            if (isWhiteKingUnderAttack)
                if (whiteKingHasMoves)
                    return "check";
                else return "mate";
            if (whiteKingHasMoves) return "ok";

            return "stalemate";
        }

        private bool WhiteKingSafeMone()
        {
            var whiteKingHasMoves = false;
            foreach (var whitePiece in _board.GetPieces(PieceColor.White))
            {
                var locationWhiteKing = _board.Get(whitePiece);

                foreach (var whitePieceCanMove in locationWhiteKing.Piece.GetMoves(whitePiece, _board))
                {
                    var possibleMovesWhitePiece = _board.Get(whitePieceCanMove);

                    var cellWhitePiece = _board.Get(whitePiece);

                    _board.Set(whitePieceCanMove, cellWhitePiece);
                    _board.Set(whitePiece, CellContent.Empty);
                    if (!IsWhiteKingUnderAttack())
                        whiteKingHasMoves = true;
                    _board.Set(whitePiece, _board.Get(whitePieceCanMove));
                    _board.Set(whitePieceCanMove, possibleMovesWhitePiece);
                }
            }
            return whiteKingHasMoves;
        }
    }
}