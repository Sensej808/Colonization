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
    public HealthScript Health;
    public virtual void Start()
    {
        Moving = gameObject.AddComponent<AllyMoving>();
        Selection = gameObject.AddComponent<SelectionCheck>();
        Health = gameObject.AddComponent<HealthScript>();
    }
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
        return result;
    }
    public GameObject TargetUnit()
    {
        GameObject target = FocusTarget();
        if (target == null)
            target = NearestTarget();
        return target;
    }
    public virtual void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0) && Input.GetKey("a") && Selection.isSelected)
        {
            GameObject target = TargetUnit();
            print($"{target.transform.position.x} {target.transform.position.y} {target.transform.position.z}");
        }
        */
    }
}
