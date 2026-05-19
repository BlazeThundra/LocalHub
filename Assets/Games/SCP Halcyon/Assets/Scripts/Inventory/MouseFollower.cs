using UnityEngine;

namespace SCPHalcyon
{
public class MouseFollower : MonoBehaviour
{
    void Update()
    {
        transform.position = Input.mousePosition;
    }
}
}