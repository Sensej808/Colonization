using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    private Vector3 position;
    private float speed = 15.0f;
    public Grid1 grid;
    void Start()
    {
        
    }
    void Update()
    {
            position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = transform.position.z;
            if ((Input.mousePosition.x) >= Screen.width && gameObject.transform.position.x + Camera.main.aspect * Camera.main.orthographicSize <= grid.pf.grid.RIGHT_BORDER)
            {
                position.y = transform.position.y;
                transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
            }
            if ((Input.mousePosition.y) >= Screen.height && gameObject.transform.position.y + Camera.main.orthographicSize <= grid.pf.grid.UP_BORDER)
            {
                position.x = transform.position.x;
                transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
            }
            if ((Input.mousePosition.x) <= 0 && gameObject.transform.position.x - Camera.main.aspect * Camera.main.orthographicSize >= grid.pf.grid.LEFT_BORDER)
            {
                position.y = transform.position.y;
                transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
            }
            if ((Input.mousePosition.y) <= 0 && gameObject.transform.position.y - Camera.main.orthographicSize >= grid.pf.grid.DOWN_BORDER)
            {
                position.x = transform.position.x;
                transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
            }
    }
}