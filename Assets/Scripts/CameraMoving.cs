using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    private Vector3 position;
    private float speed = 15.0f;
    void Start()
    {
        
    }
    void Update()
    {
        position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = transform.position.z;
        if ((Input.mousePosition.x) >= Screen.width)
        {
            position.y = transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
        }
        if ((Input.mousePosition.y) >= Screen.height)
        {
            position.x = transform.position.x;
            transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
        }
        if ((Input.mousePosition.x) <= 0)
        {
            position.y = transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
        }
        if ((Input.mousePosition.y) <= 0)
        {
            position.x = transform.position.x;
            transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
        }
    }
}