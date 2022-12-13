using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    private Vector3 position;
    private float speed = 15.0f;
    public Grid1 grid;
    public int offset = 10;
    void Start()
    {
        
    }
    void Update()
    {
            position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = transform.position.z;
            if ((Input.mousePosition.x + offset) >= Screen.width && gameObject.transform.position.x + Camera.main.aspect * Camera.main.orthographicSize <= grid.pf.grid.RIGHT_BORDER-10)
            {
                position.y = transform.position.y;
                transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
            }
            if ((Input.mousePosition.y + offset) >= Screen.height && gameObject.transform.position.y + Camera.main.orthographicSize <= grid.pf.grid.UP_BORDER-10)
            {
                position.x = transform.position.x;
                transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
            }
            if ((Input.mousePosition.x - offset) <= 0 && gameObject.transform.position.x - Camera.main.aspect * Camera.main.orthographicSize >= grid.pf.grid.LEFT_BORDER+10)
            {
                position.y = transform.position.y;
                transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
            }
            if ((Input.mousePosition.y - offset) <= 0 && gameObject.transform.position.y - Camera.main.orthographicSize >= grid.pf.grid.DOWN_BORDER+10)
            {
                position.x = transform.position.x;
                transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
            }
    }
}