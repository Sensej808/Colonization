using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BaseUnitClass : MonoBehaviour
{
    public AllyMoving Moving;
    public SelectionCheck Selection;
    public Health Health;
    public TargetUnitForAttack TakeTargetUnit;
    
    /*
    public float attack_radius;
    public GameObject tUnit;
    public GameObject bullet;
    public GameObject realTarget;
    public bool goAttack = false;
    public GameObject nextTarget;
    public GameObject targetFocusUnit;
    */
    public virtual void Start()
    {
        Moving = gameObject.AddComponent<AllyMoving>();
        Selection = gameObject.AddComponent<SelectionCheck>();
        Health = gameObject.AddComponent<Health>();
        TakeTargetUnit = gameObject.AddComponent<TargetUnitForAttack>();
    }
    /*
    public virtual void Attack()
    {

    }
    public GameObject NearestTarget()
    {
        float min_dist = float.MaxValue;
        GameObject nearest_unit = null;
        GameObject[] arrEnemyUnits = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject unit in arrEnemyUnits)
        {
            if (unit != null)
            {
                float real_dist = (gameObject.transform.position - unit.transform.position).magnitude;
                if (real_dist < min_dist)
                {
                    min_dist = real_dist;
                    nearest_unit = unit;
                }
            }
        }
        return nearest_unit;
    }
    public GameObject FocusTarget()
    {
        GameObject result = null;
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        Collider2D hitColider = Physics2D.OverlapCircle(pos, 0.1f);
        if (hitColider != null)
        {
            if (hitColider.gameObject.tag == "Enemy")
                result = hitColider.gameObject;
        }
        targetFocusUnit = result;
        return result;
    }
    public GameObject TargetUnit()
    {
        GameObject target = FocusTarget();
        if (target == null)
            target = NearestTarget();
        if ((transform.position - target.transform.position).magnitude >= attack_radius && bullet == null)
            target = null;
        return target;
    }
    public virtual void Update()
    {
    }
    */
}
