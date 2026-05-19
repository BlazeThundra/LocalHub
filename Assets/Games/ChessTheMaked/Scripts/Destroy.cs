using System.Collections;
using UnityEngine;

namespace ChessTheMaked
{
public class Destroy : MonoBehaviour
{
    public GameObject cover;

    void Start()
    {
        print("started");
        StartCoroutine(QuitterDelay());
    }

    IEnumerator QuitterDelay()
    {
        print("timer sytared");
        yield return new WaitForSeconds(30f);
        Destroy(cover);
    }
}
}