using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RemoveUnknownSquare: MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "UnknownSquare(Clone)")
            Destroy(collision.gameObject);
    }
}

