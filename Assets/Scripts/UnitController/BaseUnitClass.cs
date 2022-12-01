using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//Базовый класс-интерфейс юнитов
public enum StateUnit { Normal, BuildStruct, GoUseAbility }
public class BaseUnitClass : MonoBehaviour
{
    [HideInInspector]
    public AllyMoving Moving;
    [HideInInspector]
    public SelectionCheck Selection;
    [HideInInspector]
    public Health Health;
    public BaseAttack Attack;
    public StateUnit state;
    public float ProductionTime;
    public virtual void Start()
    {
        Moving = gameObject.AddComponent<AllyMoving>();
        Selection = gameObject.AddComponent<SelectionCheck>();
        Health = gameObject.AddComponent<Health>();
        Attack = gameObject.AddComponent<BaseAttack>();
        state = StateUnit.Normal;
        /*
<<<<<<< HEAD
        state = StateUnit.Normal;
=======
        gameObject.GetComponent<BaseAttack>();
>>>>>>> 35b82799be7df5c57d45881bc123d1572a74d261
        */

    }
}
