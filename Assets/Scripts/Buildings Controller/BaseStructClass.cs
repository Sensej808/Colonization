using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SelectionCheck))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(DoUnits))]
public class BaseStructClass : MonoBehaviour
{
    public SelectionCheck Selection;
    public Health Health;
    public DoUnits Create;

    public int SizeX = 2;
    public int SizeY = 2;
    public AudioClip structOrder;
    
    
    void Start()
    {

        Selection = gameObject.GetComponent<SelectionCheck>();
        Health = gameObject.GetComponent<Health>();
        Create = gameObject.GetComponent<DoUnits>();

        //Debug.Log($"pf = null: {PathFinding.Instance == null}");
        PathFinding.Instance.grid.GetXY(transform.position - new Vector3(SizeX / 2, SizeY / 2, 0), out int x, out int y);
        for (int i = 0; i < SizeX; i++)
        {
            for (int j = 0; j < SizeY; j++)
            {

                //Debug.Log($"pf = null: {PathFinding.Instance == null} \n grid = null: {PathFinding.Instance.grid == null} \n GetValue = null: {PathFinding.Instance.grid.GetValue(new Vector3(x + i, y + j, 0)) == null}");
                PathFinding.Instance.grid.GetValue(x + i, y + j).SetWalkable(false);
            }
        }
        var HpBar = Instantiate(Resources.Load<GameObject>("Prefabs/BarCanvas"), new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + gameObject.GetComponent<BoxCollider2D>().size.y / 1.7f * gameObject.transform.localScale.y, 1), gameObject.transform.rotation);
        HpBar.name = "HpBar";
        HpBar.transform.parent = gameObject.transform;
        //GetComponent<AudioSource>().Play();
        Audio.instance.PlaySound(structOrder);
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
    }
   
}
