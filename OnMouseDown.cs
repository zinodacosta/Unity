using UnityEngine;
using System.Collections;

public class MouseClick : MonoBehaviour
{

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMouseDown ()
    {
        rb.AddForce(-transform.forward * 500f);
        rb.useGravity = true;
    }
}
