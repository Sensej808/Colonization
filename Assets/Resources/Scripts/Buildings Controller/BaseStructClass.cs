using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SelectionCheck))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(DoUnits))]
public class BaseStructClass : Obstacle
{
    public SelectionCheck Selection;
    public Health Health;
    public DoUnits Create;
    public AudioClip structOrder;

    void Start()
    {

        Selection = gameObject.GetComponent<SelectionCheck>();
        Health = gameObject.GetComponent<Health>();
        Create = gameObject.GetComponent<DoUnits>();
        Storage.AllStructions.Add(gameObject);

        //Debug.Log($"pf = null: {PathFinding.Instance == null}");
        TakePlase();
        //GetComponent<AudioSource>().Play();
        //Audio.instance.PlaySound(structOrder);
    }
    private void Update()
    {
    }

    void OnDestroy()
    {
        PathFinding.Instance.grid.GetXY(transform.position - new Vector3(SizeX / 2, SizeY / 2, 0), out int x, out int y);
        for (int i = 0; i < SizeX; i++)
        {
            for (int j = 0; j < SizeY; j++)
            {

                //Debug.Log($"pf = null: {PathFinding.Instance == null} \n grid = null: {PathFinding.Instance.grid == null} \n GetValue = null: {PathFinding.Instance.grid.GetValue(new Vector3(x + i, y + j, 0)) == null}");
                PathFinding.Instance.grid.GetValue(x + i, y + j).SetWalkable(true);
            }
        }
        Storage.AllStructions.Remove(gameObject);
    }
   
}
