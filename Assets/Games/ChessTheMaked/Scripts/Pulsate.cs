using UnityEngine;
using UnityEngine.UI;

namespace ChessTheMaked
{
public class Pulsate : MonoBehaviour
{
    public GameObject returnss;
    private Image op;

    public Color color1 = Color.cyan;  
    public Color color2 = Color.blue;  
    public float speed = 2f;

    void Start()
    {
        op = returnss.GetComponent<Image>();
    }

    void Update()
    {
        if (op != null)
        {
            float t = (Mathf.PingPong(Time.time * speed, 1f));
            op.color = Color.Lerp(color1, color2, t);
        }
    }
}
}