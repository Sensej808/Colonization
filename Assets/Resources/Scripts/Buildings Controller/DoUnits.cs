using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
//TODO: Написать скрипт спауна юнитов(будет компонентом здания, скорее всего, значит должен реагировать на команды здания)

//Скрипт создания юнитов от зданий
public class DoUnits : MonoBehaviour
{
    //TODO: Переделать таймер
    //Скрипт создания юнитов от зданий
        public float time;
        public BaseStructClass myStruct;
        public Queue<GameObject> queueUnits;
        public GameObject unit;
        public AudioClip structOrder;
        public bool timerRun;
        public GameObject TimeBar;
        private IEnumerator StartTimer()
        {
            while (time >= -0.1f)
            {
                time -= Time.deltaTime;
                yield return null;
            }
            timerRun = false;
        }
        public void Start()
        {
            myStruct = gameObject.GetComponent<BaseStructClass>();
            queueUnits = new Queue<GameObject>();
            StartCoroutine(StartTimer());
            timerRun = true;
        TimeBar = Instantiate(Resources.Load<GameObject>("Prefabs/Bar"), new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + gameObject.GetComponent<BoxCollider2D>().size.y / 2.2f * gameObject.transform.localScale.y, 1), gameObject.transform.rotation);
        TimeBar.transform.parent = gameObject.transform;
        TimeBar.GetComponent<Bar>().maxValue = 1;
        TimeBar.GetComponent<Bar>().realValue = 0;
        TimeBar.GetComponent<Bar>().bar.GetComponent<Renderer>().material.color = Color.blue;
        TimeBar.GetComponent<Bar>().UpdateBar();
        }
        public void AddUnit(GameObject unit)
        {
            if (unit.GetComponent<BaseUnitClass>().price <= Storage.amountResources)
            {
                Storage.TakeResources(unit.GetComponent<BaseUnitClass>().price);
                queueUnits.Enqueue(unit);
                Audio.instance.PlaySound(structOrder);
                if (!timerRun)
                {
                    time = 0;
                    StartCoroutine(StartTimer());
                    timerRun = true;
                }
            }
            //print("юнит заказан");
        }
        public void CreateUnit()
        {
            if (queueUnits.Count != 0 && unit == null)
            {
                unit = queueUnits.Peek();
                time = unit.GetComponent<BaseUnitClass>().ProductionTime;
                TimeBar.GetComponent<Bar>().maxValue = time;
            }
            if (unit != null)
            {
            TimeBar.GetComponent<Bar>().realValue = time;
            TimeBar.GetComponent<Bar>().UpdateBar();
            if (time <= 0)
                {
                    Instantiate(unit, gameObject.transform.Find("SpawnUnits").position, transform.rotation);
                    unit = null;
                    queueUnits.Dequeue();
                }
            }
        }
        public void Update()
        {
            CreateUnit();
        }
    public void OnDestroy()
    {
        //if (unit != null)
        //    Storage.AddResources(unit.GetComponent<BaseUnitClass>().price);
        foreach(var x in queueUnits)
        {
            Storage.AddResources(x.GetComponent<BaseUnitClass>().price);
        }
    }
}
