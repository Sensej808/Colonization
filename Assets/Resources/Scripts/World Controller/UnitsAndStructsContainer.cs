using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����-���������, ���������� � ���� ������ ������, ������ � ���������� ������ ������
public class UnitsAndStructsContainer : MonoBehaviour
{
    public static List<GameObject> Structs;
    public static List<GameObject> Units;
    public static List<GameObject> SelectedUnits { get; private set; }
    public static UnitsAndStructsContainer instance { get; private set; }
    // Start is called before the first frame update
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }
        Destroy(this.gameObject);
    }



    public static void UpdateSelectedUnits(List<GameObject> Selected)
    {
        SelectedUnits = Selected;
    }

}
