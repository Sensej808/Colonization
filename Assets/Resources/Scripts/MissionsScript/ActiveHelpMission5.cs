using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveHelpMission5 : MonoBehaviour
{
    public GameObject help;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoAttack()
    {
        Collider2D[] hitColiders = Physics2D.OverlapCircleAll(gameObject.transform.position, 20);
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
