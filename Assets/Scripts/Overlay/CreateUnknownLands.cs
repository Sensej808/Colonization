using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateUnknownLands : MonoBehaviour
{
    public GameObject square;
    void Start()
    {
        int k = (int)square.transform.localScale.x;
        for (int i = -70; i < 70; i+=k)
        {
            for (int j = -70; j < 70; j+=k)
                Instantiate(square, new Vector3(i, j, 1), transform.rotation).transform.parent = gameObject.transform;
        }
    }
}
