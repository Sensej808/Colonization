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
        }
        public void AddUnit(GameObject unit)
        {
            queueUnits.Enqueue(unit);
            Audio.instance.PlaySound(structOrder);
            if (!timerRun)
            {
                time = 0;
                StartCoroutine(StartTimer());
                timerRun = true;
            }
        }
        public void CreateUnit()
        {
            if (queueUnits.Count != 0 && unit == null)
            {
                unit = queueUnits.Peek();
                time = unit.GetComponent<BaseUnitClass>().ProductionTime;
                myStruct.timeBar.transform.Find("background").GetComponent<UIBar>().time = time;
            }
            if (unit != null)
            {
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
}
