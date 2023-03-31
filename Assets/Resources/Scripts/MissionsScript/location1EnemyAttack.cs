using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class location1EnemyAttack : MonoBehaviour
{
    public float nextTime;
    public int period;
    public float timeOrder;
    public float startTime;
    public float radius;
    public int period_time;
    private IEnumerator StartAttack()
    {
        while (true)
        {
            if (timeOrder >= nextTime)
            {
                nextTime += period;
                if (startTime < timeOrder)
                    GoAttack();
                GiveOrder();
            }
            timeOrder += Time.deltaTime;
            yield return null;
        }
    }
    void Start()
    {
        period = period_time;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void StartStartAttack()
    {
        StartCoroutine(StartAttack());
        timeOrder = GameObject.Find("Canvas").transform.Find("Timer").Find("Image").Find("Text").GetComponent<Timer>()._timeLeft;
        nextTime = timeOrder;
        startTime = timeOrder;
    }
    public void GiveOrder()
    {
        Collider2D[] hitColiders = Physics2D.OverlapCircleAll(gameObject.transform.position, radius);
        foreach(Collider2D building in hitColiders)
        {
            if (building.gameObject.GetComponent<DoUnits>() && building.gameObject.tag == "Enemy")
            {
                if (building.gameObject.GetComponent<DoUnits>().queueUnits.Count == 0)
                {
                    building.gameObject.GetComponent<DoUnits>().AddUnit(Resources.Load<GameObject>("Prefabs/Enemy"));
                    building.gameObject.GetComponent<DoUnits>().AddUnit(Resources.Load<GameObject>("Prefabs/Enemy"));
                    //building.gameObject.GetComponent<DoUnits>().AddUnit(Resources.Load<GameObject>("Prefabs/Enemy"));
                }
            }
        }
        print("�����");
    }
    public void GoAttack()
    {
        Collider2D[] hitColiders = Physics2D.OverlapCircleAll(gameObject.transform.position, radius);
        foreach (Collider2D unit in hitColiders)
        {
            if (unit.gameObject.GetComponent<BaseAttack>() && unit.gameObject.tag == "Enemy")
            {
                unit.gameObject.GetComponent<BaseAttack>().finalAttackPos = transform.Find("AttackPos").position;
                unit.gameObject.GetComponent<BaseUnitClass>().state = StateUnit.Aggressive;
            }
        }
    }
}
