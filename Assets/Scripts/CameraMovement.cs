using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Ќаписать скрипт, который будет перемещать камеру стенки, которой касаетс€ мышка
public class CameraMovement : MonoBehaviour
{
    public float camSpeed = 20f;
    private float Border = 15f;
    public Vector2 limit;

    public float scrollwhell = 200f;
    public float minY = 0f;
    public float maxY = 120f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.mousePosition.y >= Screen.height - Border)
        {
            pos.y += camSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.x >= Screen.width - Border)
        {
            pos.x += camSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.y <= Border)
        {
            pos.y -= camSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.x <= Border)
        {
            pos.x -= camSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollwhell * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, -limit.x, limit.x);
        pos.z = Mathf.Clamp(pos.z, -limit.y, limit.y);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);


        transform.position = pos;
    }
}
