using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broad : MonoBehaviour
{
    [SerializeField] private List<Cell> cells = new List<Cell>();
    [SerializeField] private Vector2Int size = new Vector2Int(8, 8);
    public void GetNearestObject(GameObject objectRay)
    {
        RaycastHit2D hit = Physics2D.Raycast(objectRay.transform.position, objectRay.transform.up, 2);

        float distance = hit.distance;
        if (hit.collider != null)
        {
            Debug.Log("Khoảng cách đến vật phía trước: " + distance);
            Cell cell = hit.collider.gameObject.GetComponent<Cell>();
            if (distance < 1)
            {
                cell.SetState(CellState.hover);
            }
            else
            {
                cell.SetState(CellState.free);
            }
            //if (Vector2.Distance(transform.position, cell.transform.position) > 1) cell.SetState(CellState.free);
        }
    }
    public void CheckDoneRow()
    {
        int i = 0;
        int r = 0;
        foreach (Cell cell in cells)
        {
            if (cell.cellState == CellState.active)
            {
                i++;
                if (i == 8)
                {
                    for (int j = r; j > r - 8; j--)
                    {
                        cells[j].SetState(CellState.free);
                        Destroy(cells[j].transform.GetChild(0).gameObject);
                    }
                }
            }
            else
            {
                i = 0;
            }
            r++;
        }
        int t = 0;
        for (int k = 0; k < 8; k++)
        {
            for (int h = k; h < cells.Count; h += 8)
            {
                if (cells[h].cellState == CellState.active)
                {
                    t++;
                    if (t == 8)
                    {
                        for (int u = k; u < cells.Count; u += 8)
                        {
                            cells[u].SetState(CellState.free);
                            Destroy(cells[u].transform.GetChild(0).gameObject);
                        }
                    }
                }
                else
                {
                    t = 0;
                }
            }
        }
    }
    public void ResetBroad()
    {
        foreach (Cell cell in cells)
        {
            cell.SetState(CellState.free);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
