using Mono.Cecil.Cil;
using UnityEngine;

namespace ChessTheMaked
{
public class StartingTile : MonoBehaviour
{
 [SerializeField] GameObject[] whitePieces;
 [SerializeField] GameObject[] blackPieces;
    [SerializeField] string color;
   [SerializeField] ChessBoard chessBoard;


 void Start()
 {
  if(color == "white")
  {
   chessBoard.whitePieces.Add(Instantiate(whitePieces[Random.Range(0, whitePieces.Length)], transform));
  }
  else if(color == "black")
  {
   chessBoard.blackPieces.Add(Instantiate(blackPieces[Random.Range(0, blackPieces.Length)], transform));
  }
 }
}
}