using UnityEngine;

namespace ChessTheMaked
{
public enum PieceType { Pawn, Bishop, Rook, Queen, King, Knight, Alchemist, Sniper }
public enum Player { white, black }
public class GamePiece : MonoBehaviour
{
    [SerializeField] public Player player;
    [SerializeField] public PieceType pieceType;
    [SerializeField] public int health;
    [SerializeField] public int damage;
    [SerializeField] public int thorns;
    [SerializeField] public AudioClip attackSFX;
    [SerializeField] public AudioClip deathSFX;
    [SerializeField] public AudioClip selectSFX;

    public Player GetPlayer() => player;
    public PieceType GetPieceType() => pieceType;
}
}