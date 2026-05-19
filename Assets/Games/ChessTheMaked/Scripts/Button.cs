using System;
using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace ChessTheMaked
{
public class Button : MonoBehaviour
{
    public Image cover;
    public ChessBoard chessboard;
    public AudioSource source;
    public AudioClip clicksound;

    public void LoadStart()
    {
        SceneManager.LoadScene("CTMStartScreen");
        source.PlayOneShot(clicksound);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("CTMGameScene");
        source.PlayOneShot(clicksound);
    }

    public void LoadQuit()
    {
        SceneManager.LoadScene("Hub");
        StartCoroutine(QuitterDelay());
        source.PlayOneShot(clicksound);        
    }

    public void Options()
    {
        SceneManager.LoadScene("CTMOptions");
        source.PlayOneShot(clicksound);
    }

    public void LightVictory()
    {
        print("Light Team Wins!!");
    }

    public void DarkVictory()
    {
        print("Dark Team Wins!!");
    }

    public void Skip()
    {
        chessboard.turnMove = false;
        chessboard.turnAttack = false;
        chessboard.ResetHighlight();
        chessboard.ChangeTurn();
        source.PlayOneShot(clicksound);
    }

    IEnumerator QuitterDelay()
    {
        yield return new WaitForSeconds(30f);
    }
}
}