using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public static int maxHp = 10;
    int hp = maxHp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if hp <= 0
        {
            Destroy(gameObject);
        }
    }
}
