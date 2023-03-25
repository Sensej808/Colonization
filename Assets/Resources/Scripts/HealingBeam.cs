using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingBeam : MonoBehaviour
{
    public Vector3 begPos;
    public Vector3 endPos;
    public Vector3 CenPos; 
    void Update()
    {
        SetCenterPos();
        SetLength();
        SetRotation();
        transform.position = CenPos;
    }
    public void SetCenterPos()
    {
        CenPos.x = (begPos.x + endPos.x)/2;
        CenPos.y = (begPos.y + endPos.y)/2;
        CenPos.z = 1;
    }
    public void SetLength()
    {
        transform.localScale = new Vector3((float)Math.Sqrt(Math.Abs(begPos.x - endPos.x)* Math.Abs(begPos.x - endPos.x) + Math.Abs(begPos.y - endPos.y) * Math.Abs(begPos.y - endPos.y)), 0.12f, 1);
    }
    public void SetRotation()
    {
        Vector2 temp = new Vector3(endPos.x, begPos.y);
        Vector2 temp1 = new Vector3(endPos.x - begPos.x, endPos.y - begPos.y);
        Vector2 temp2 = new Vector3(temp.x - begPos.x, temp.y - begPos.y);
        float cos = (temp1.x * temp2.x + temp2.y + temp2.y) / (float)(Math.Sqrt(temp1.x*temp1.x + temp1.y*temp1.y) * Math.Sqrt(temp2.x * temp2.x + temp2.y * temp2.y));
        if ((begPos.x < endPos.x && begPos.y > endPos.y) || begPos.x > endPos.x && begPos.y < endPos.y)
            cos = -cos;
        transform.rotation = Quaternion.Euler(0, 0, (float)(Math.Acos(cos)*180/Math.PI));
    }
}
