using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    public bool clickInterface; //���������� ����������, ����� ��� ����� ����������� �������: �� ����������, ��� ����������
    public Dictionary<string, bool> KeyOnMenu; //������ ������ ����������, ���������, ������� ������ value true, ���� ������ ������
    public int k;
    //�������� ���������� ����� �� ������(�� �� ���� ����������), �� ��������� � �����-�� ����������
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
    //��������� ��� ������ ����������
    //(��� �����, ������ ��� � 1 ������ ����� ������ ����� 1 �������)
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
    //��� �� ������� ��� ������ �������, ������� �������, ����� ������ ��� ������
    //(��������� ��� ����� ������, �� ������ ��� �������������)

    //�������� ������  � ����������, �������, ��� ������ ������ ����������
    public void Click(string key)
    {
        AllButtonFalse();
        clickInterface = true;
        KeyOnMenu[key] = true;
    }
    //������� ������, ������������� ������� � ������, ���������� ��������, ��������� ���������� ���������� ������ ������
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
    //������������� ������������� ���� ���������� �������
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
    //��� ������� ��������� � �����
    public void MoveUnits(List<GameObject> units, Vector3 pos)
    {
        
        SortedList<double, int> units_dists_to_pos = new SortedList<double, int>();
        foreach(GameObject unit in units)//������ ��������������� �� ��������� �� �������� ������� ������ ������
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

        foreach(var dist_and_num in units_dists_to_pos)
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
                if (units[dist_and_num.Value].layer == 9) //�������� �����
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
        KeyOnMenu.Add("Q", false); //��������� ������ Q � ���������
        KeyOnMenu.Add("R", false); //��������� ������ R � ���������
        KeyOnMenu.Add("unitW", false);
        KeyOnMenu.Add("unitF", false);
        KeyOnMenu.Add("unitD", false);
        KeyOnMenu.Add("unitE", false);
    }

    //���������� ������� �������� ������� �� ��������� course
    public void StartMining(GameObject source)
    {
        List<GameObject> group = Storage.selectedUnits.FindAll(x => x.GetComponent<Mining>() != null);
        foreach (var worker in group)
        {
            //worker.GetComponent<Mining>().source = source.GetComponent<SourseOfRecourses>();
            //worker.GetComponent<Mining>().enabled = true;
        }
    }

    void Update()
    {
        //���� �� ������ ������ � ����������, �� ��������� ������� �� ����������
        if (!clickInterface)
        {
            //���� ��������� ������, ����� ������� ������
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

            //��������� ��� �� ����� ��������������� ��������� ����

            Ray r = new Ray(Input.mousePosition, Input.mousePosition + new Vector3(0, 0, 1));
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (Input.GetMouseButtonDown(1) &&  hit.collider != null && hit.collider.gameObject.tag != "Vision" && !hit.collider.isTrigger)
            {

                var sourse = hit.collider.gameObject;
                Debug.Log($"Move to {hit.collider.gameObject.name}");
                //MoveToObj(Storage.selectedUnits, sourse.gameObject);
                //���� ������ ��� � ��� ����� � �������� �������� ��� �������, ���������� ������� ��������
                /*if (sourse.GetComponent<SourseOfRecourses>() != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    StartMining(sourse);
                    print(Storage.selectedUnits.FindAll(x => x.GetComponent<Mining>() != null).Count);

                }*/

            }
            else if(Input.GetMouseButtonDown(1) && Storage.selectedUnits.Count != 0)
            {
                Debug.Log("??");
                MoveUnits(Storage.selectedUnits, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }
        //���� ������ ������ � �����������, �� ��������� ������� �� ����� ����
        if (clickInterface)
        {
            //���� ������ ���, �� ����� ������� ���������������� ������
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
        //���� ��� ESC ��� S(�� ���� stop), �� ���������� ��� ������� � ���������� ������
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.S))
        {
            clickInterface = false;
            StopBuild();
        }
        //��������
        //������ ���, ����� �� ��� ��� �� �����, �� ������� ����� ������
        //� ��� �� ������� 5 �������, ����� �� ������� ���� ��������� ���������, ����� ������ ����������(�������, �� ���� ������ ����� �����)
        //������ �������� �������, � ��� ��� ��� ���������� �� ���, �� ����� �������� ����� ���������
        //� ��� ��� � ��� ������ ����� �� ����� ����� 5 �������, �� ��� ������ �������������
        //������� ��������� ����� ��������� �����, �� ���������, ������� �� ������� ���-�� ��� ���
        //������ �� ������ ����� �� �������� clickInterface
        //������ ��� ������� ���, ��� �� �����������, � ����� ���� ������� ���������� ��������� �����, ������� ����� �������
        //������� ����� �������� ���� �� ���������� ����� ��������� ����� �������� �����
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
