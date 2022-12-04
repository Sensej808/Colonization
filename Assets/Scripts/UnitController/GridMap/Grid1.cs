using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid1 : MonoBehaviour
{
    PathFinding pf;
    // Start is called before the first frame update
    void Awake()
    {
        //Debug.Log("PF created");
        pf = PathFinding.Init(30,20);
        //pf.grid.ChangeColor();
    }

}
