using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
//Класс препятсвия на карте
public class Obstacle : MonoBehaviour
{
    //длина в юнитах
    public int SizeX;
    //высота в юнитах
    public int SizeY;

    //Блокирует клетки для прохода
    public void TakePlase()
    {
        var Scale = transform.localScale;
        var structSize  = GetComponent<BoxCollider2D>().size * Scale;
        PathFinding.Instance.grid.GetXY(transform.position - new Vector3(structSize.x / 2, structSize.y / 2, 0), out int x, out int y);
        for (int i = 0; i < structSize.x ; i++)
        {
            for (int j = 0; j < structSize.y; j++)
            {

                //Debug.Log($"pf = null: {PathFinding.Instance == null} \n grid = null: {PathFinding.Instance.grid == null} \n GetValue = null: {PathFinding.Instance.grid.GetValue(new Vector3(x + i, y + j, 0)) == null}");
                PathFinding.Instance.grid.GetValue(x + i, y + j).SetWalkable(false);
            }
        }
    }
    
}
