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
        pf = PathFinding.Init(200, 200);
        //pf.grid.ChangeColor();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var x = pf.grid.GetValue(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            Debug.Log($"clicked on:{x.x} {x.y}");
        }
    }
}
