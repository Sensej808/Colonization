using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    public float GetTargetRange;
    public GameObject target;
    public GameObject prefabHealingBeam;
    public GameObject HealingBeam;
    public float cooldown;
    public float realCooldown;
    public bool timerRun;
    public double treatment;
    // Start is called before the first frame update
    void Start()
    {
        GetTargetRange = 5f;
        realCooldown = 0f;
        timerRun = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if ((target.transform.position - gameObject.transform.position).magnitude > GetTargetRange)
                target = FindNearestTarget();
        }
        else
        {
            Destroy(HealingBeam);
            target = FindNearestTarget();
        }
        if (target != null)
            Treat();
    }
    public GameObject FindNearestTarget()
    {
        float min_dist = float.MaxValue;
        GameObject nearest_unit = null;
        Collider2D[] hitColiders = Physics2D.OverlapCircleAll(gameObject.transform.position, GetTargetRange);
        foreach (Collider2D unit in hitColiders)
        {
            if (unit.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                if (unit.gameObject != null && unit.gameObject.tag == gameObject.tag && unit.gameObject.GetComponent<Health>() != null && unit.gameObject.GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Static)
                {
                    if (unit.gameObject.GetComponent<Health>().CurrentHealth < unit.gameObject.GetComponent<Health>().HP && unit.gameObject != gameObject)
                    {
                        float real_dist = (gameObject.transform.position - unit.gameObject.transform.position).magnitude;
                        if (real_dist < min_dist)
                        {
                            min_dist = real_dist;
                            nearest_unit = unit.gameObject;
                        }
                    }
                }
            }
        }
        return nearest_unit;
    }
    public void Treat()
    {
        if (HealingBeam == null)
        {
            HealingBeam = Instantiate(prefabHealingBeam, transform.position, transform.rotation);
        }
        else
        {
            HealingBeam.GetComponent<HealingBeam>().begPos = transform.position;
            HealingBeam.GetComponent<HealingBeam>().endPos = target.transform.position;
        }
        if (!timerRun)
        {
            realCooldown = 0;
            StartCoroutine(StartTimer());
            timerRun = true;
        }
        if (realCooldown <= 0f)
        {
            target.GetComponent<Health>().GetHealth(treatment);
            realCooldown = cooldown;
        }
        if (target.GetComponent<Health>().CurrentHealth >= target.GetComponent<Health>().HP)
            Destroy(HealingBeam);
    }
    private IEnumerator StartTimer()
    {
        while (realCooldown >= -0.1f)
        {
            realCooldown -= Time.deltaTime;
            yield return null;
        }
        timerRun = false;
    }
}
