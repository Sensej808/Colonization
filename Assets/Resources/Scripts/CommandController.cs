using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    public bool clickInterface; //переменная отвечающая, какие щас будут выполняться команды: от клавиатуры, или интерфейса
    public Dictionary<string, bool> KeyOnMenu; //массив кнопок интерфейса, говорящий, который ставит value true, если кнопка нажата
    public int k;
    //получаем ближайшего юнита из ГРУППЫ(не из всех выделенных), по отношению к какой-то координате
    public GameObject Nearest(Vector3 pos, List<GameObject> group)
    {
        float min = float.MaxValue;
        GameObject nearest = null;
        foreach (GameObject go in group)
        {
            //if (go != null)
            //{
                if ((go.transform.position - pos).magnitude < min && go.GetComponent<BaseUnitClass>().state == StateUnit.Normal)
                {
                    min = (go.transform.position - pos).magnitude;
                    nearest = go;
                }
            //}
        }
        return nearest;
    }
    //выключаем все кнопки интерфейса
    //(это нужно, потому что в 1 момент можно отдать ровно 1 команду)
    public void AllButtonFalse()
    {
        List<string> str = new List<string>();
        foreach (KeyValuePair<string, bool> button in KeyOnMenu)
        {
            str.Add(button.Key);
        }
        foreach (string s in str)
            KeyOnMenu[s] = false;
    }
    //тут мы создаем все нужные функции, которые говорят, какая кнопка щас нажата
    //(актуально для любых кастов, не только для строительства)

    //нажимаем кнопку  в интерфейсе, говорим, что нажата кнопка интерфейса
    public void Click(string key)
    {
        AllButtonFalse();
        clickInterface = true;
        KeyOnMenu[key] = true;
    }
    //основой скрипт, устанавливает позицию и здание, ближайшему рабочему, остальные продолжают заниматься своими делами
    public void SetStruct(string nameStruct)
    {
        Debug.Log("Frame Placed");
        Vector3 myPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        myPos.z = 0;
        //List<GameObject> group = Storage.selectedUnits.FindAll(x => x != null ? x.GetComponent<Build>() != null : x == null);
        List<GameObject> group = Storage.selectedUnits.FindAll(x => x.GetComponent<Build>() != null);
        GameObject builder = Nearest(myPos, group);
        if (builder != null)
        {
            Debug.Log("Going to build");
            builder.GetComponent<Build>().SetStructPos(Resources.Load<GameObject>(nameStruct), myPos);
            builder.GetComponent<Build>().GoAndBuild();
        }
        
    }
    //останавливает строительство ВСЕХ выделенных рабочих
    public void StopBuild()
    {
        //List<GameObject> group = Storage.selectedUnits.FindAll(x => x != null ? x.GetComponent<Build>() != null : x == null);
        List<GameObject> group = Storage.selectedUnits.FindAll(x => x.GetComponent<Build>() != null);
        foreach (GameObject go in group)
            {
                //if (go != null)
                    go.GetComponent<Build>().StopBuild();
            }
    }
    public void AddUnit(string NameUnit)
    {
        //GetSelectedUnits();
        //List<GameObject> group = Storage.selectedUnits.FindAll(x => x != null ? x.GetComponent<DoUnits>() != null : x == null);
        List<GameObject> group = Storage.selectedUnits.FindAll(x => x.GetComponent<DoUnits>() != null);
        GameObject myStruct = null;
        if (group.Count != 0)
            myStruct = group.OrderBy(x => x.GetComponent<DoUnits>().queueUnits.Count).First();
        if (myStruct != null)
            myStruct.GetComponent<DoUnits>().AddUnit(Resources.Load<GameObject>(NameUnit));
    }
    //Даём команду двигаться к точке
    public void MoveUnits(List<GameObject> units, Vector3 pos)
    {
        SortedList<double, int> units_dists_to_pos = new SortedList<double, int>();
        foreach(GameObject unit in units)//Создаём отсортированный по растоянию до конечной позиции список юнитов
        {
            double distance = Vector3.Distance(pos, unit.transform.position);
            units_dists_to_pos.Add(distance, units_dists_to_pos.Count);
        }
        Queue<PathNode> poses = new Queue<PathNode>();
        PathFinding.Instance.grid.GetXY(pos, out int x, out int y);
        poses.Enqueue(PathFinding.Instance.grid.GetValue(x, y));
        foreach(var dist_and_num in units_dists_to_pos)
        {
            while (true)
            {
            var p = poses.Dequeue();
            var neighs = PathFinding.Instance.OpenNeighbours(p);
            foreach (var n in neighs)
                poses.Enqueue(n);
            if (PathFinding.Instance.grid.GetValue(x, y).is_empty)
                {
                    units[dist_and_num.Value].GetComponent<AllyMoving>().MoveTo(PathFinding.Instance.grid.GetWorldPos(p.x, p.y));
                    break;
                }

            }
            

        }
    }
    public void Start()
    {
        KeyOnMenu = new Dictionary<string, bool>();
        k = 100;
        KeyOnMenu.Add("Q", false); //добавляем кнопку Q в интерфейс
        KeyOnMenu.Add("R", false); //добавляем кнопку R в интерфейс
        KeyOnMenu.Add("unitW", false);
        KeyOnMenu.Add("unitF", false);
        KeyOnMenu.Add("unitD", false);
        KeyOnMenu.Add("unitE", false);
    }

    //Отправляем рабочих добывать ресурсы из источника course
    public void StartMining(GameObject source)
    {
        List<GameObject> group = Storage.selectedUnits.FindAll(x => x.GetComponent<Mining>() != null);
        foreach (var worker in group)
        {
            worker.GetComponent<Mining>().source = source.GetComponent<SourseOfRecourses>();
            worker.GetComponent<Mining>().enabled = true;
        }
    }

    void Update()
    {
        //если не нажата кнопка в интерфейсе, то выполняем команды от клавиатуры
        if (!clickInterface)
        {
            //если отпустили кнопку, пошли строить здание
            if (Input.GetKeyUp(KeyCode.Q))
                SetStruct("Prefabs/FrameQ");
            if (Input.GetKeyUp(KeyCode.R))
                SetStruct("Prefabs/FrameR");
            if (Input.GetKeyDown(KeyCode.W))
                AddUnit("Prefabs/Cyborg");
            if (Input.GetKeyDown(KeyCode.F))
                AddUnit("Prefabs/Engineer");
            if (Input.GetKeyDown(KeyCode.D))
                AddUnit("Prefabs/Doctor");
            if (Input.GetKeyDown(KeyCode.E))
                AddUnit("Prefabs/AirUnit");

            //Выпускаем луч из мышки перпендикулярно плоскости игры

            Ray r = new Ray(Input.mousePosition, Input.mousePosition + new Vector3(0, 0, 1));
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (Input.GetMouseButtonDown(1) && hit.collider != null)
            {
                var sourse = hit.collider.gameObject;
                //Если нажали ПКМ и луч попал в источник ресурсов при нажатии, отправляем рабочих добывать
                if (sourse.GetComponent<SourseOfRecourses>() != null)
                {
                    Debug.Log(hit.collider.gameObject.name);    
                    StartMining(sourse);
                    print(Storage.selectedUnits.FindAll(x => x.GetComponent<Mining>() != null).Count);

                }

            }
            else if(Input.GetMouseButtonDown(1) && Storage.selectedUnits.Count != 0)
            {
                MoveUnits(Storage.selectedUnits, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                print("??");
            }
        }
        //если нажата кнопка в интрерфейсе, то выполняем команды по клику мыши
        if (clickInterface)
        {
            //если нажали ПКМ, то пошли строить соответственнное здание
            if (Input.GetMouseButtonDown(0) && KeyOnMenu["R"])
            {
                SetStruct("Prefabs/FrameR");
                k = 100;
                AllButtonFalse();
            }
            if (Input.GetMouseButtonDown(0) && KeyOnMenu["Q"])
            {
                SetStruct("Prefabs/FrameQ");
                k = 100;
                AllButtonFalse();
            }
            if (KeyOnMenu["unitW"])
            {
                AddUnit("Prefabs/Cyborg");
                k = 100;
                AllButtonFalse();
            }
            if (KeyOnMenu["unitF"])
            {
                AddUnit("Prefabs/Engineer");
                k = 100;
                AllButtonFalse();
            }
            if (KeyOnMenu["unitD"])
            {
                AddUnit("Prefabs/Doctor");
                k = 100;
                AllButtonFalse();
            }
            if (KeyOnMenu["unitE"])
            {
                AddUnit("Prefabs/AirUnit");
                k = 100;
                AllButtonFalse();
            }
        }
        //если жмёт ESC или S(Ну типо stop), то отменяются все команды у веделенных юнитов
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.S))
        {
            clickInterface = false;
            StopBuild();
        }
        //объясняю
        //каждый раз, когда мы жмём лкм на карту, мы создаем сетку выбора
        //и вот мы выбрали 5 рабочих, хотим им раздать наши важнейшие поручение, через кнопки интерфейса(осуждаю, всё надо делать через клаву)
        //одного посылаем строить, и так как это происходит по ЛКМ, то также создаётся сетка выделения
        //и так как в ней скорее всего не будет ваших 5 рабочих, то они станут невыделенными
        //поэтому создатель сетки выделения умный, он проверяет, кастуем мы сейччас что-то или нет
        //почему бы просто сразу не обнулять clickInterface
        //потмоу что выходит так, что он обнууляется, и через нано секунды вызывается строитель сетки, поэтому нужен счётчик
        //который будет обнулять клик по интерфейсу через некоторое очень короткое время
        if (k != 0)
        {
            k--;
            if (k == 0)
            {
                clickInterface = false;
            }
        }
    }
}
