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
    public void GetSelectedUnits()
    {
        selectedUnits.Clear();
        GameObject[] arrSelectedUnits = GameObject.FindGameObjectsWithTag("Allied");
        foreach (GameObject go in arrSelectedUnits)
        {
            if (go.GetComponent<SelectionCheck>().isSelected)
                selectedUnits.Add(go);
        }
    }
    //получаем ближайшего юнита из ГРУППЫ(не из всех выделенных), по отношению к какой-то координате
    public GameObject Nearest(Vector3 pos, List<GameObject> group)
    {
        float min = float.MaxValue;
        GameObject nearest = null;
        foreach (GameObject go in group)
        {
            if ((go.transform.position - pos).magnitude < min)
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

    //нажимаем кнопку R в интерфейсе, говорим, что нажата кнопка интерфейса
    public void ClickR()
    {
        AllButtonFalse();
        clickInterface = true;
        KeyOnMenu["R"] = true;
    }
    //нажимаем кнопку Q в интерфейсе, говорим, что нажата кнопка интерфейса
    public void ClickQ()
    {
        AllButtonFalse();
        clickInterface = true;
        KeyOnMenu["Q"] = true;
    }
    //тут будут скрипты, отвечающие за выбор здания для строительства

    //основой скрипт, устанавливает позицию и здание, ближайшему рабочему, остальные продолжают заниматься своими делами
    public void SetStruct(string nameStruct)
    {
        Vector3 myPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        myPos.z = 0;
        GetSelectedUnits();
        List<GameObject> group = selectedUnits.FindAll(x => x.GetComponent<Build>() != null);
        GameObject builder = Nearest(myPos, group);
        if (builder != null)
            builder.GetComponent<Build>().SetStructPos(Resources.Load<GameObject>(nameStruct), myPos);
    }
    //нужны чисто для кнопки в интерфейсе, потому что она не принимает функции с параметрами :(
    public void SetStructR() { SetStruct("Prefabs/StructR"); }
    public void SetStructQ() { SetStruct("Prefabs/StructQ"); }
    //останавливает строительство ВСЕХ выделенных рабочих
    public void StopBuild()
    {
            GetSelectedUnits();
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
                SetStructQ();
            if (Input.GetKeyUp(KeyCode.R))
                SetStructR();
        }
        //если нажата кнопка в интрерфейсе, то выполняем команды по клику мыши
        if (clickInterface)
        {
            //если нажали ПКМ, то пошли строить соответственнное здание
            if (Input.GetMouseButtonDown(0) && KeyOnMenu["R"])
            {
                SetStructR();
                k = 100;
            }
            if (Input.GetMouseButtonDown(0) && KeyOnMenu["Q"])
            {
                SetStructQ();
                k = 100;
            }
        }
        //если жмёт ESC или S(Ну типо stop), то отменяются все команды у веделенных юнитов
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.S))
        {
            clickInterface = false;
            StopBuild();
        }
        //объясняю
        //каждый божий раз, когда мы жмём лкм на карту, мы создаем сетку выбора
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
                clickInterface = false;
        }
    }
}
