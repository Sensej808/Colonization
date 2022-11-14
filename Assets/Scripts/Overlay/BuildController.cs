using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    private Building ghost;
    public List<GameObject> builders;

    public void StartPlacingBuilding(Building buildingPrefab)
    {
        if (ghost != null)
        {
            Destroy(ghost.gameObject);
        }

        ghost = Instantiate(buildingPrefab);
    }

    private void Update()
    {
        if (ghost != null)
        {
            var buildPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            buildPos.x = Mathf.RoundToInt(buildPos.x); //округляем до целых, чтобы здания нельзя было строить где попало
            buildPos.y = Mathf.RoundToInt(buildPos.y); //округляем до целых, чтобы здания нельзя было строить где попало
            buildPos.z = 0;

            ghost.transform.position = buildPos;

            bool available = true;

            if (IsPlaceTaken(ghost)) available = false;

            //ЛЕГАСИ
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Allied");
            builders = new List<GameObject>();
            foreach (GameObject go in gameObjects)
            {
                if (go.GetComponent<Build>())
                {
                    if (go.GetComponent<SelectionCheck>().isSelected)
                        builders.Add(go);
                }
            }
            //

            if (available && Input.GetMouseButtonDown(0) && builders.Count > 0)
            {

                ghost.SetColor(new Color(1, 1, 1, 0.5F));
                ghost = null;
            }

        }
    }

    private bool IsPlaceTaken(Building building)
    {
        return building.Can_builded;
    }
}