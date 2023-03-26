using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����� ���������� �� �����
public class Obstacle : MonoBehaviour
{
    public int SizeX;
    public int SizeY;

    //��������� ������ ��� �������
    public void TakePlase()
    {
        PathFinding.Instance.grid.GetXY(transform.position - new Vector3(SizeX / 2, SizeY / 2, 0), out int x, out int y);
        for (int i = 0; i < SizeX; i++)
        {
            for (int j = 0; j < SizeY; j++)
            {

                //Debug.Log($"pf = null: {PathFinding.Instance == null} \n grid = null: {PathFinding.Instance.grid == null} \n GetValue = null: {PathFinding.Instance.grid.GetValue(new Vector3(x + i, y + j, 0)) == null}");
                PathFinding.Instance.grid.GetValue(x + i, y + j).SetWalkable(false);
            }
        }
    }
    
}
