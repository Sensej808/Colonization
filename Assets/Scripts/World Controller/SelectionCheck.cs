using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����, ������� ������ ���������� ��� ��������� ������
public class SelectionCheck : MonoBehaviour
{
    public bool isSelected = false;
    private Rect rect;
    Sprite selectBox;


    //��������� ��������� �������
    public void Demonstrate()
    {
        //Debug.Log(isSelected);
        transform.GetChild(0).gameObject.SetActive(isSelected);
    }
}
