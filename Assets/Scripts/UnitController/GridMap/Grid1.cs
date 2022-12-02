using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid1 : MonoBehaviour
{
    PathFinding pf;
    // Start is called before the first frame update
    void Start()
    {
        pf = new PathFinding(10,10);
        
    }
}
