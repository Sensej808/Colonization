using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��������� ������, ��� ������������ ������������� ������
public class InGameStorage : MonoBehaviour
{
    public int[] resourses; //���������� �������� ��� �������������
    public List<GameObject> Units; //��� �����
    public List<GameObject> SelectedUnits; // ���������� �����
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
