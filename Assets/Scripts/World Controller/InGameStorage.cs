using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Хранилище игрока, где записываются внутриигровые данные
public class InGameStorage : MonoBehaviour
{
    public int[] resourses; //Количество ресурсов для строительства
    public List<GameObject> Units; //Все юниты
    public List<GameObject> SelectedUnits; // Выделенные юниты
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Init");
        Units = new List<GameObject>();
        SelectedUnits = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
