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
        List<Collider2D> gos = new List<Collider2D>(Physics2D.OverlapCircleAll(pos, 0.01f));
        if (null == gos.FindAll(x => x.gameObject.GetComponent<Rigidbody2D>() != null).Find(x => x.gameObject.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Static))
        {
            SortedList<double, int> units_dists_to_pos = new SortedList<double, int>();
            foreach (GameObject unit in units)//Создаём отсортированный по растоянию до конечной позиции список юнитов
            {
                double distance = Vector3.Distance(pos, unit.transform.position);
                if (units_dists_to_pos.ContainsKey(distance))
                    units_dists_to_pos.Add(distance + 0.01f, units_dists_to_pos.Count);
                else
                    units_dists_to_pos.Add(distance, units_dists_to_pos.Count);
            }
            Debug.Log(units_dists_to_pos.Count);
            Queue<PathNode> poses = new Queue<PathNode>();
            PathFinding.Instance.grid.GetXY(pos, out int x, out int y);
            poses.Enqueue(PathFinding.Instance.grid.GetValue(x, y));

            List<PathNode> aims = new List<PathNode>();

            foreach (var dist_and_num in units_dists_to_pos)
            {

                while (true)
                {
                    var p = poses.Dequeue();
                    var neighs = PathFinding.Instance.OpenNeighbours(p);
                    foreach (var n in neighs)
                    {
                        if (n.is_walkable)
                        {

                            poses.Enqueue(n);

                        }

                    }
                    bool is_taken = false;
                    if (units[dist_and_num.Value].layer == 9) //Наземные юниты
                        is_taken = Physics2D.OverlapBoxAll(PathFinding.Instance.grid.GetWorldPos(p), Vector2.one * p.grid.CellSize, 0, LayerMask.GetMask("GroundUnits")).All((col) => col.isTrigger);
                    else
                        is_taken = Physics2D.OverlapBoxAll(PathFinding.Instance.grid.GetWorldPos(p), Vector2.one * p.grid.CellSize, 0, LayerMask.GetMask("Air")).All((col) => col.isTrigger);

                    if (PathFinding.Instance.grid.GetValue(p.x, p.y).is_empty && is_taken)
                    {

                        units[dist_and_num.Value].GetComponent<AllyMoving>().MoveTo(PathFinding.Instance.grid.GetWorldPos(p.x, p.y));
                        aims.Add(p);
                        p.is_empty = false;
                        break;
                    }

                }
            }
            foreach (var node in aims)
            {
                node.is_empty = true;
            }
        }
    }
    public void MoveToObj(List<GameObject> units, GameObject aim)
    {
        foreach(var un in units)
        {
            un.GetComponent<AllyMoving>().MoveTo(aim);
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
        //Debug.Log($"units - {Storage.selectedUnits.Count}");

        BaseStructClass storage = Storage.AllStructions.First().GetComponent<BaseStructClass>();
        double dist = Mathf.Infinity;
        foreach(var bs in Storage.AllStructions){
            if(Vector3.Distance(bs.transform.position, source.transform.position) < dist)
            {
                dist = Vector3.Distance(bs.transform.position, source.transform.position);
                storage = bs.GetComponent<BaseStructClass>();
            }
        }

        foreach (var worker in group)
        {
            //Debug.Log("Here");
            Vector3 ResPos = source.GetComponent<Transform>().position;
            Vector3 diff = worker.transform.position - ResPos;
            Vector3 offset = new Vector3(0, 0, 0);
            //TODO: сделать нормальный подход к объектам

            if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
            {
                offset.x += 1 * Mathf.Sign(diff.x);
            }
            else
            {
                offset.y += 1 * Mathf.Sign(diff.y);
            }

            Debug.Log($"offset - {offset}");
            Debug.Log($"Moving to " + source.name + $" Coords: {ResPos + offset}");
            worker.GetComponent<AllyMoving>().MoveTo(ResPos + offset);
            worker.GetComponent<Mining>().source = source.GetComponent<SourseOfRecourses>();
            worker.GetComponent<Mining>().storage = storage;
            if (worker.GetComponent<Mining>().storage != null)
            {
                worker.GetComponent<Mining>().enabled = true;

            }


            
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
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            //Debug.Log(hits.Length);
            var Lhits = hits.ToList();
            int i = 0;
            foreach (var hit in Lhits)
            {
                if (Input.GetMouseButtonDown(1))
                {

                    Debug.Log(hit.collider != null);
                    Debug.Log(!hit.collider.isTrigger);
                    Debug.Log($"AAA {hit.collider.gameObject.name == "Metal_sourse"}");

                }

                if (Input.GetMouseButtonDown(1) && Lhits.Find((hit) => (hit.collider.gameObject.GetComponent<SourseOfRecourses>() != null)) == true)
                {

                    //var sourse = hit.collider.gameObject;
                    //Debug.Log($"Move to {Lhits.Find((hit) => (hit.collider.gameObject.GetComponent<SourseOfRecourses>() != null)).collider.gameObject.name}");
                    //Debug.Log($"Move to {Lhits.Find((hit) => (hit.collider.gameObject.GetComponent<SourseOfRecourses>() != null)).collider.gameObject.transform.position}");
                    //Debug.Log("??????");
                    StartMining(Lhits.Find((hit) => (hit.collider.gameObject.GetComponent<SourseOfRecourses>() != null)).collider.gameObject);
                    break;
                    //MoveToObj(Storage.selectedUnits, sourse.gameObject);
                    //Если нажали ПКМ и луч попал в источник ресурсов при нажатии, отправляем рабочих добывать
                    /*if (sourse.GetComponent<SourseOfRecourses>() != null)
                    {
                        Debug.Log(hit.collider.gameObject.name);
                        print(Storage.selectedUnits.FindAll(x => x.GetComponent<Mining>() != null).Count);

                    }*/
                }
                else if(Input.GetMouseButtonDown(1) && Storage.selectedUnits.Count != 0)
                {
                    Debug.Log("??");
                    MoveUnits(Storage.selectedUnits, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                }
            }
            if (Input.GetMouseButtonDown(1) && Storage.selectedUnits.Count != 0 && Lhits.Count == 0)
            {
                    MoveUnits(Storage.selectedUnits, Camera.main.ScreenToWorldPoint(Input.mousePosition));
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
