using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackOfResources : MonoBehaviour
{
    public int amountResources;
    public MaterialsText text;
    public bool isempty;
    void Start()
    {
        text = GameObject.Find("Canvas").transform.Find("Recourses").gameObject.GetComponent<MaterialsText>();
        isempty = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Allied" && isempty == false)
        {
            isempty = true;
            Storage.AddResources(amountResources);
            //text.TextUpdate();
            Destroy(gameObject);
        }
    }
}
