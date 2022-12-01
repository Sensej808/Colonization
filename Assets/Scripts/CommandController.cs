using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    public List<GameObject> selectedUnits; //лист, содержащий выделенных юнитов
    public bool clickInterface; //переменная отвечающая, какие щас будут выполняться команды: от клавиатуры, или интерфейса
    public Dictionary<string, bool> KeyOnMenu; //массив кнопок интерфейса, говорящий, который ставит value true, если кнопка нажата
    public int k;

    //получаем лист выделенных юнитов
    public void UpdateSelection(List<GameObject> SelectedUnits)
    {
        foreach (var unit in selectedUnits)
        {
            unit.GetComponent<SelectionCheck>().isSelected = false;
            unit.GetComponent<SelectionCheck>().Demonstrate();
        }
        selectedUnits = null;
        selectedUnits = SelectedUnits;
        foreach (var unit in SelectedUnits)
        {
            unit.GetComponent<SelectionCheck>().isSelected = true;
            unit.GetComponent<SelectionCheck>().Demonstrate();
        }
    }
    
    //получаем ближайшего юнита из ГРУППЫ(не из всех выделенных), по отношению к какой-то координате
    public GameObject Nearest(Vector3 pos, List<GameObject> group)
    {
        float min = float.MaxValue;
        GameObject nearest = null;
        foreach (GameObject go in group)
        {
            if ((go.transform.position - pos).magnitude < min && go.GetComponent<BaseUnitClass>().state == StateUnit.Normal)
            {
                min = (go.transform.position - pos).magnitude;
                nearest = go;
            }
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
        Vector3 myPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        myPos.z = 0;
        List<GameObject> group = selectedUnits.FindAll(x => x.GetComponent<Build>() != null);
        GameObject builder = Nearest(myPos, group);
        if (builder != null)
            builder.GetComponent<Build>().SetStructPos(Resources.Load<GameObject>(nameStruct), myPos);
    }
    //останавливает строительство ВСЕХ выделенных рабочих
    public void StopBuild()
    {
            List<GameObject> group = selectedUnits.FindAll(x => x.GetComponent<Build>() != null);
            foreach (GameObject go in group)
            {
                go.GetComponent<Build>().StopBuild();
            }
    }
    public void Start()
    {
        KeyOnMenu = new Dictionary<string, bool>();
        k = 100;
        KeyOnMenu.Add("Q", false); //добавляем кнопку Q в интерфейс
        KeyOnMenu.Add("R", false); //добавляем кнопку R в интерфейс
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
