using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid1 : MonoBehaviour
{
    public PathFinding pf;
    // Start is called before the first frame update
    void Awake()
    {
        //Debug.Log("PF created");
        pf = PathFinding.Init(120, 120);
        //pf.grid.ChangeColor();
    }

}
