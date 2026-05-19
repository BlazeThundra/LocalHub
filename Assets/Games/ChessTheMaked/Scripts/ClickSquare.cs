using UnityEngine;
using Mono.Cecil.Cil;

namespace ChessTheMaked
{
public class ClickSquare : MonoBehaviour
{
    [SerializeField] ChessBoard chessBoard;
    [SerializeField] PieceSFX pieceSFX;
    [SerializeField] StartingTile startingTile;
    private SpriteRenderer rend;
    [SerializeField] public Color originalColor;
    public Color highlightColor = Color.yellow;
    public Color attackColor = Color.red;
    public Color healColor = Color.green;
    [SerializeField] public Color alchBuffColor;
    [SerializeField] GameObject[] whitePieces;
    [SerializeField] GameObject[] blackPieces;
    [SerializeField] string color;

    public string turnColor = "white";
    public bool highlighted;
    public bool attackHighlighted;
    public bool healHighlighted;
    public bool summonHighlighted;
    public bool kingHighlighted;
    public bool alchBuffHighlighted;

    GamePiece piece;


    void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        originalColor = rend.color;

        if ((color == "white") && CompareTag("Starting Tile"))
        {
            chessBoard.whitePieces.Add(Instantiate(whitePieces[Random.Range(0, whitePieces.Length)], transform));
        }
        else if ((color == "black") && CompareTag("Starting Tile"))
        {
            chessBoard.blackPieces.Add(Instantiate(blackPieces[Random.Range(0, blackPieces.Length)], transform));
        }
    }


    void OnMouseDown()
    {
        var index = chessBoard.GetIndex(gameObject.name);
        if (index == null) return;


        piece = GetComponentInChildren<GamePiece>();

        if (kingHighlighted)
        {
            chessBoard.kingAttacking = true;
            rend.color = originalColor;
        }

        if (summonHighlighted)
        {
            if (chessBoard.turn.ToString().Equals("white", System.StringComparison.OrdinalIgnoreCase))
            {
                print("white pawn make");
                chessBoard.whitePieces.Add(Instantiate(whitePieces[0], transform));
            }
            else
            {
                print("black pawn make");
                chessBoard.blackPieces.Add(Instantiate(blackPieces[0], transform));
            }
            chessBoard.turnAttack = false;
            chessBoard.ResetHighlight();
        }


        if (highlighted && chessBoard.selectedPiece != null)
        {
            chessBoard.selectedPiece.transform.parent = transform;
            chessBoard.selectedPiece.transform.position = transform.position;
            chessBoard.turnMove = false;
            chessBoard.ResetHighlight();
            chessBoard.selectedPiece = null;
            return;
        }
        else if (attackHighlighted)
        {
            var target = GetComponentInChildren<GamePiece>();
            var attacker = chessBoard.selectedPiece.GetComponent<GamePiece>();
            pieceSFX.AttackSFX(attacker);

            if (target != null && attacker != null)
            {
                if(attacker.GetPieceType() != PieceType.Alchemist)
                {
                 target.health -= attacker.damage;
                }
                
                if (target.GetPlayer() != attacker.GetPlayer())
                {
                 attacker.health -= target.thorns;
                }

                if (chessBoard.alchemistAttacking)
                {
                    target.damage -= attacker.damage;
                }
            }

            if (target.health <= 0)
            {
                if (target.GetPlayer().ToString().Equals("white", System.StringComparison.OrdinalIgnoreCase))
                {
                    chessBoard.blackPieces.Remove(target.gameObject);
                }
                else { chessBoard.whitePieces.Remove(target.gameObject); }
                pieceSFX.DeathSFX(target);
                Destroy(target.gameObject);
                if (chessBoard.queenAttacking)
                {
                    attacker.transform.parent = transform;
                    attacker.transform.position = transform.position;
                }
            }
            if (attacker.health <= 0) Destroy(attacker.gameObject);
            if (chessBoard.queenAttacking) { chessBoard.turnMove = false; chessBoard.queenAttacking = false; }


            chessBoard.turnAttack = false;
            chessBoard.ResetHighlight();


            if (!chessBoard.turnMove)
            {
                chessBoard.selectedPiece = null;
                chessBoard.ChangeTurn();
            }
            else
            {
                chessBoard.selectedPiece = null;
            }
            return;
        }
        else if (healHighlighted)
        {
            GetComponentInChildren<GamePiece>().health -= chessBoard.selectedPiece.GetComponent<GamePiece>().damage;
            chessBoard.ResetHighlight();
            pieceSFX.AttackSFX(chessBoard.selectedPiece.GetComponent<GamePiece>());
            chessBoard.selectedPiece = null;
            chessBoard.turnAttack = false;
            return;
        }
        else if (alchBuffHighlighted)
        {
            var target = GetComponentInChildren<GamePiece>();
            var attacker = chessBoard.selectedPiece.GetComponent<GamePiece>();
            pieceSFX.AttackSFX(attacker);

            if (target.pieceType.ToString() == "Bishop")
            {
                target.damage -= attacker.damage;
            }
            else
            {
                target.damage += attacker.damage;
            }
            chessBoard.turnAttack = false;
            chessBoard.ResetHighlight();

            if (!chessBoard.turnMove)
            {
                chessBoard.selectedPiece = null;
                chessBoard.ChangeTurn();
            }
            else
            {
                chessBoard.selectedPiece = null;
            }
            return;
        }

        if (chessBoard.selectedPiece == null || (piece != null && piece.GetPlayer().ToString() == chessBoard.turn))
        {
            if (piece == null || piece.GetPlayer().ToString() != chessBoard.turn) return;


            if (!chessBoard.kingAttacking) { chessBoard.ResetHighlight(); }
            chessBoard.selectedPiece = piece.gameObject;
            pieceSFX.SelectSFX(chessBoard.selectedPiece.GetComponent<GamePiece>());


            if (piece.GetPlayer().ToString().Equals(chessBoard.turn, System.StringComparison.OrdinalIgnoreCase))
            {
                if (chessBoard.selectedPiece != piece.gameObject)
                {
                    chessBoard.ResetHighlight();
                }
            }


            switch (piece.GetPieceType())
            {
                case PieceType.Pawn: HighlightPawnMoves(piece, index.Item1, index.Item2); break;
                case PieceType.Rook: HighlightRookMoves(piece, index.Item1, index.Item2); break;
                case PieceType.Knight: HighlightKnightMoves(piece, index.Item1, index.Item2); break;
                case PieceType.Bishop: HighlightBishopMoves(piece, index.Item1, index.Item2); break;
                case PieceType.Queen: HighlightQueenMoves(piece, index.Item1, index.Item2); break;
                case PieceType.King: HighlightKingMoves(piece, index.Item1, index.Item2); break;
                case PieceType.Alchemist: HighlightAlchemistMoves(piece, index.Item1, index.Item2); break;
                // case PieceType.Sniper : HighlightSniperMoves(piece, Index,Item1, index.Item2); break;
            }
        }
    }


    public void Highlight()
    {
        if (chessBoard.turnMove)
        {
            highlighted = true;
            rend.color = highlightColor;
        }
    }


    public void ResetHighlight()
    {
        highlighted = false;
        attackHighlighted = false;
        healHighlighted = false;
        summonHighlighted = false;
        alchBuffHighlighted = false;
        rend.color = originalColor;
        chessBoard.queenAttacking = false;
        chessBoard.kingAttacking = false;
        chessBoard.alchemistAttacking = false;
        kingHighlighted = false;
    }


    public void AttackHighlight()
    {
        if (chessBoard.turnAttack)
        {
            if (chessBoard.selectedPiece.GetComponent<GamePiece>().damage < 0)
            {
                HealHighlight();
            }
            else
            {
                attackHighlighted = true;
                rend.color = attackColor;
            }
        }
    }


    public void HealHighlight()
    {
        if (chessBoard.turnAttack)
        {
            healHighlighted = true;
            rend.color = healColor;
        }
    }

    public void KingHighlight()
    {
        rend.color = healColor;
        kingHighlighted = true;
    }

    public void SummonHighlight()
    {
        rend.color = attackColor;
        summonHighlighted = true;
    }

    public void AlchemistBuffHighlight()
    {
        rend.color = alchBuffColor;
        alchBuffHighlighted = true;
    }

    void HighlightPawnMoves(GamePiece pawn, int row, int col)
    {
        int direction = pawn.GetPlayer() == Player.white ? 1 : -1;


        var forwardSquare = chessBoard.GetSquareAt(row + direction, col);
        if (forwardSquare != null && forwardSquare.transform.childCount == 0)
            forwardSquare.GetComponent<ClickSquare>().Highlight();


        bool isAtStartRow = pawn.transform.parent.tag == "Starting Tile";
        if (isAtStartRow)
        {
            var forwardTwo = chessBoard.GetSquareAt(row + (2 * direction), col);
            if (forwardTwo != null && forwardTwo.transform.childCount == 0)
                forwardTwo.GetComponent<ClickSquare>().Highlight();
        }


        int[,] attacks = new int[,] { { 0, 1 }, { 0, -1 } };
        for (int i = 0; i < attacks.GetLength(0); i++)
        {
            int newRow = row + attacks[i, 0];
            int newCol = col + attacks[i, 1];
            var square = chessBoard.GetSquareAt(newRow, newCol);
            if (square == null) continue;


            if (square.transform.childCount != 0 &&
                square.GetComponentInChildren<GamePiece>().GetPlayer().ToString() != chessBoard.turn)
            {
                square.GetComponent<ClickSquare>().AttackHighlight();
            }
        }
    }


    void HighlightRookMoves(GamePiece rook, int row, int col)
    {
        int[,] directions = { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };


        for (int d = 0; d < 4; d++)
        {
            int dRow = directions[d, 0];
            int dCol = directions[d, 1];


            int currentRow = row + dRow;
            int currentCol = col + dCol;


            while (true)
            {
                var square = chessBoard.GetSquareAt(currentRow, currentCol);
                if (square == null) break;


                if (square.transform.childCount == 0)
                    square.GetComponent<ClickSquare>().Highlight();
                else break;


                currentRow += dRow;
                currentCol += dCol;
            }
        }
    }


    void HighlightKnightMoves(GamePiece knight, int row, int col)
    {
        int[,] moves =
        {
            { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 },
            { 1, 2 }, { 1, -2 }, { -1, 2 }, { -1, -2 }
        };


        for (int i = 0; i < moves.GetLength(0); i++)
        {
            int newRow = row + moves[i, 0];
            int newCol = col + moves[i, 1];
            var square = chessBoard.GetSquareAt(newRow, newCol);
            if (square == null) continue;


            if (square.transform.childCount == 0)
                square.GetComponent<ClickSquare>().Highlight();
        }


        int[,] attacks =
        {
            { 0, 2 }, { 0, -2 }, { 2, 0 }, { -2, 0 },
            { 2, 2 }, { 2, -2 }, { -2, 2 }, { -2, -2 }
        };


        for (int i = 0; i < attacks.GetLength(0); i++)
        {
            int newRow = row + attacks[i, 0];
            int newCol = col + attacks[i, 1];
            var square = chessBoard.GetSquareAt(newRow, newCol);
            if (square == null) continue;


            if (square.transform.childCount != 0 &&
                square.GetComponentInChildren<GamePiece>().GetPlayer().ToString() != chessBoard.turn)
            {
                square.GetComponent<ClickSquare>().AttackHighlight();
            }
        }
    }


    void HighlightBishopMoves(GamePiece bishop, int row, int col)
    {
        int[,] directions = { { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 } };


        for (int d = 0; d < 4; d++)
        {
            int dRow = directions[d, 0];
            int dCol = directions[d, 1];


            int currentRow = row + dRow;
            int currentCol = col + dCol;


            while (true)
            {
                var square = chessBoard.GetSquareAt(currentRow, currentCol);
                if (square == null) break;


                if (square.transform.childCount == 0)
                {
                    square.GetComponent<ClickSquare>().Highlight();
                }
                else if (square.GetComponentInChildren<GamePiece>().GetPlayer().ToString() == chessBoard.turn)
                {
                    square.GetComponent<ClickSquare>().AttackHighlight();
                    break;
                }
                else break;


                currentRow += dRow;
                currentCol += dCol;
            }
        }
    }


    void HighlightKingMoves(GamePiece king, int row, int col)
    {

        int[,] moves =
        {
            { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 },
            { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 }
        };

        if (!chessBoard.kingAttacking)
        {
            for (int i = 0; i < moves.GetLength(0); i++)
            {
                int newRow = row + moves[i, 0];
                int newCol = col + moves[i, 1];


                var square = chessBoard.GetSquareAt(newRow, newCol);
                if (square == null) continue;


                if (square.transform.childCount == 0)
                {
                    square.GetComponent<ClickSquare>().Highlight();
                }
            }

            if (chessBoard.turnAttack)
            {
                GetComponent<ClickSquare>().KingHighlight();
            }        
        }
        else
        {
            for (int i = 0; i < moves.GetLength(0); i++)
            {
                int newRow = row + moves[i, 0];
                int newCol = col + moves[i, 1];


                var square = chessBoard.GetSquareAt(newRow, newCol);
                if (square == null) continue;

                if (square.transform.childCount == 0 && chessBoard.turnAttack)
                {
                    square.GetComponent<ClickSquare>().ResetHighlight();
                    square.GetComponent<ClickSquare>().SummonHighlight();
                }
            }
        }
    }


    void HighlightQueenMoves(GamePiece queen, int row, int col)
    {
        int[,] directions =
        {
            { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 },
            { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 }
        };


        for (int d = 0; d < directions.GetLength(0); d++)
        {
            int dRow = directions[d, 0];
            int dCol = directions[d, 1];

            int currentRow = row + dRow;
            int currentCol = col + dCol;


            while (true)
            {
                var square = chessBoard.GetSquareAt(currentRow, currentCol);
                if (square == null) break;


                if (square.transform.childCount == 0)
                {
                    square.GetComponent<ClickSquare>().Highlight();
                }
                else if (square.GetComponentInChildren<GamePiece>().GetPlayer().ToString() != chessBoard.turn && chessBoard.turnAttack == true && chessBoard.turnMove == true)
                {
                    square.GetComponent<ClickSquare>().AttackHighlight();
                    chessBoard.queenAttacking = true;
                    break;
                }
                else break;


                currentRow += dRow;
                currentCol += dCol;
            }
        }
    }

void HighlightAlchemistMoves(GamePiece alchemist, int row, int col)
{
    int[,] moves =
    {
        { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 },
        { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 },
        { 2, 0 }, { -2, 0 }, { 0, 2 }, { 0, -2 },
        { 2, 2 }, { 2, -2 }, { -2, 2 }, { -2, -2 },
        { 2, 1 }, { 1, 2 }, { -1, 2 }, { -2, 1 },
        { -1, -2 }, { -2, -1 }, { 2,-1 }, { 1,-2 }
    };

    int[,] attacks =
    {
        { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 }
    };
 
    for (int i = 0; i < moves.GetLength(0); i++)
    {
        int newRow = row + moves[i, 0];
        int newCol = col + moves[i, 1];

        var square = chessBoard.GetSquareAt(newRow, newCol);
        if (square == null) continue;

        var clickSquare = square.GetComponent<ClickSquare>();
        var occupant = square.GetComponentInChildren<GamePiece>();

        if (occupant == null)
        {
            clickSquare.Highlight();
        }
    }

    for (int i = 0; i < attacks.GetLength(0); i++)
    {        
        int newRow = row + moves[i, 0];
        int newCol = col + moves[i, 1];
        
        var square = chessBoard.GetSquareAt(newRow, newCol);
        if (square == null) continue;
        
        var clickSquare = square.GetComponent<ClickSquare>();
        var occupant = square.GetComponentInChildren<GamePiece>();

        if (occupant == null)
        {
            clickSquare.Highlight();
        }
        else if (occupant.GetPlayer().ToString() != chessBoard.turn && chessBoard.turnAttack)
        {
            clickSquare.AttackHighlight();
            chessBoard.alchemistAttacking = true;
        }
        else if (occupant.GetPlayer().ToString() == chessBoard.turn && chessBoard.turnAttack)
        {
            clickSquare.AlchemistBuffHighlight();
            chessBoard.alchemistAttacking = true;
        }
    }
  }

//   void HighlightSniperMoves(GamePiece Sniper, int row, int col)
//   {
//    int[,]moves=
//    {
//     {0,1}, {0,-1}, {1,0}, {-1,0}
//    };

//    int[,] attacks=
//    {
//     {1,0}, {-1,0}, {0,1}, {0,-1}
//    };
//   }
}
}