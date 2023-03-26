using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

//создает сетку выделения юнитов
public class CreateSelectionGrid : MonoBehaviour
{
    public GameObject prefabSelectionGrid; //префаб сетки выделения
    public GameObject prefabAirSelectionGrid;
    private Vector3 pos1; //позиция мыши НА ЭКРАНЕ, при первом нажатии ЛКМ
    private Vector3 pos2; //позиция мыши НА ЭКРАНЕ, во время расстягивания сетки выделения
    private Vector3 rpos1;//позиция мыши НА КАРТЕ, при первом нажатии ЛКМ
    private Vector3 rpos2;//позиция мыши НА КАРТЕ, во время расстягивания сетки выделения
    private Vector3 posc; //центр сетки выделения
    private bool selecting = false; //false - если сетки сейчас нет, true - если сетка создана
    public GameObject SelectionGrid; //сетка выделения на экране
    public GameObject AirSelectionGrid; //сетка выделения на экране
    public CommandController command;
    void Update()
    {
        if (!Input.GetKey(KeyCode.A) &&Input.GetMouseButtonDown(0) && !command.clickInterface && !EventSystem.current.IsPointerOverGameObject())//при щелчке начинаем выделение
        {
            pos1 = Input.mousePosition;
            //Debug.Log("START");
            selecting = true;
            SelectionGrid = Instantiate(prefabSelectionGrid, pos1,transform.rotation); //создание сетки
            AirSelectionGrid = Instantiate(prefabAirSelectionGrid, pos1, transform.rotation); //создание сетки
            rpos1 = Camera.main.ScreenToWorldPoint(pos1); //позиция мыши НА КАРТЕ, при первом нажатии ЛКМ

        }
        else if (Input.GetMouseButton(0) && selecting) // сетка изменяет форму,пока зажата мышка
        {
            //Debug.Log("Continue");
            pos2 = Input.mousePosition;
            rpos2 = Camera.main.ScreenToWorldPoint(pos2); //позиция мыши НА КАРТЕ, во время расстягивания сетки выделения
            CenterPos(); //Находим центр
            SelectionGrid.transform.position = new Vector3(posc.x, posc.y, pos1.z);//меняем местоположение, из-за всенаправленного растяжения
            SelectionGrid.transform.localScale = rpos2 - rpos1;//меняем размер сетки
            AirSelectionGrid.transform.position = new Vector3(posc.x, posc.y, pos1.z);//меняем местоположение, из-за всенаправленного растяжения
            AirSelectionGrid.transform.localScale = rpos2 - rpos1;//меняем размер сетки
            Debug.Log(rpos2 - rpos1);
        }
        else if(Input.GetMouseButtonUp(0) && selecting)
        {
            SelectionGrid.GetComponent<Select>().IsDone = true;//подтверждаем удаление и сохраняем выделение
            AirSelectionGrid.GetComponent<Select>().IsDone = true;//подтверждаем удаление и сохраняем выделение
            //Debug.Log("FIN");
            selecting = false;
            foreach (GameObject x in SelectionGrid.GetComponent<Select>().SelectedUnits)
            {
                AirSelectionGrid.GetComponent<Select>().SelectedUnits.Add(x);
            }
            Destroy(SelectionGrid);
            Destroy(AirSelectionGrid);
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

/*
 * Если сетки нет, то 
 *      удаляем старую сетку,
 *      создаем новую сетку с 
 *      началом в щелчке, 
 *      а концом в месте, 
 *      где отпустили щелчок
 *      Выделяем всех юнитов в сетке
 * Иначе ничего
 */

/*if (!gridIsBe)
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

 
 if (SelectionGrid != null)
            {
                var s = SelectionGrid.GetComponent<Select>(); //тоже самое, что выше, не помню
                s.k = false;
            }*/