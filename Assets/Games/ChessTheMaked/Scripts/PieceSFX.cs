using Unity.Mathematics;
using UnityEngine;

namespace ChessTheMaked
{
public class PieceSFX : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    public void AttackSFX(GamePiece pieceType)
    {
     audioSource.PlayOneShot(pieceType.attackSFX);
    }

    public void SelectSFX(GamePiece pieceType)
    {
     audioSource.PlayOneShot(pieceType.selectSFX);
    }

    public void DeathSFX(GamePiece pieceType)
    {
     audioSource.PlayOneShot(pieceType.deathSFX);
    }
}
}