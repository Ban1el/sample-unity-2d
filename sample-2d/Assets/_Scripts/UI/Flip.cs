
using UnityEngine;

public class Flip : MonoBehaviour
{
    // Study later
    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
    }

    void LateUpdate()
    {
        float parentDirection = transform.parent.localScale.x;

        transform.localScale = new Vector3(
            Mathf.Abs(initialScale.x) * Mathf.Sign(parentDirection),
            initialScale.y,
            initialScale.z
        );
    }
}
