using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public Vector2Int gridSize;
    public float cellRadius = 0.5f;
    public FlowField curFlowField;
	public GridDebug gridDebug;

    private void InitializeFlowField()
	{
        curFlowField = new FlowField(cellRadius, gridSize, transform.position);
        curFlowField.CreateGrid(transform.position);
		gridDebug.SetFlowField(curFlowField);
	}

	private void Update()
	{
		if (Input.GetMouseButtonUp(1))
		{
			InitializeFlowField();

			curFlowField.CreateCostField();

			Vector3 mousePos = new Vector3(Input.mousePosition.x,Input.mousePosition.y, 0);
			Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
			Cell destinationCell = curFlowField.GetCellFromWorldPos(worldMousePos - transform.position);
			curFlowField.CreateIntegrationField(destinationCell);

			curFlowField.CreateFlowField();

			gridDebug.DrawFlowField();
		}
	}
}
