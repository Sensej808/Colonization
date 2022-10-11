using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//создает сетку выделения юнитов
public class CreateSelectionGrid : MonoBehaviour
{
    public GameObject prefabSelectionGrid; //префаб сетки выделения
    private Vector3 pos1; //позиция мыши НА ЭКРАНЕ, при первом нажатии ЛКМ
    private Vector3 pos2; //позиция мыши НА ЭКРАНЕ, во время расстягивания сетки выделения
    private Vector3 rpos1;//позиция мыши НА КАРТЕ, при первом нажатии ЛКМ
    private Vector3 rpos2;//позиция мыши НА КАРТЕ, во время расстягивания сетки выделения
    private Vector3 posc; //центр сетки выделения
    private float length; //длина сетки выделения
    private float width;  //ширина сетки выделения
    private bool gridIsBe = false; //false - если сетки сейчас нет, true - если сетка создана
    private GameObject realSelectionGrid; //сетка выделения на экране
    void Update()
    {
        if (Input.GetMouseButton(0)) //сетка создаётся при нажатии ЛКМ
        {
            if (!gridIsBe)
            {
                pos1 = Input.mousePosition;
                pos2 = pos1;
            }
            else
                pos2 = Input.mousePosition;
            CenterPos();
            rpos1 = Camera.main.ScreenToWorldPoint(pos1); //позиция мыши НА КАРТЕ, при первом нажатии ЛКМ
            rpos2 = Camera.main.ScreenToWorldPoint(pos2); //позиция мыши НА КАРТЕ, во время расстягивания сетки выделения
            length = (rpos2.x - rpos1.x);
            width = (rpos1.y - rpos2.y);
            if (!gridIsBe)
            {
                realSelectionGrid = Instantiate(prefabSelectionGrid, posc, transform.rotation);
                var s = realSelectionGrid.GetComponent<Select>(); //честно говоря не помню зачем это, но без этого почему-то не работало
                s.k = true;
            }
            realSelectionGrid.transform.position = new Vector3(posc.x, posc.y, pos1.z);
            realSelectionGrid.transform.localScale = new Vector3(length, width, pos1.z);
            gridIsBe = true;
        }
        else
        {
            gridIsBe = false;
            if (realSelectionGrid != null)
            {
                var s = realSelectionGrid.GetComponent<Select>(); //тоже самое, что выше, не помню
                s.k = false;
            }
            Destroy(realSelectionGrid);
        }
    }
    void CenterPos()//изменяет, создаёт центр сетки выделения
    {
        posc.x = pos2.x - (pos2.x - pos1.x) / 2;
        posc.y = pos1.y - (pos1.y - pos2.y) / 2;
        posc.z = pos1.z;
        posc = Camera.main.ScreenToWorldPoint(posc);
        posc.z = pos1.z;
    }
}
