using System.Collections;
using System.Collections.Generic;
using EgdFoundation;
using UnityEngine;

public class Broad : MonoBehaviour
{
    [SerializeField] private Cell cellPre;
    [SerializeField] private List<Cell> cells = new List<Cell>();
    [SerializeField] private Vector2Int size = new Vector2Int(8, 8);
    private void Start()
    {
        int id = 1;
        for (int i = 0; i <= size.x - 1; i++)
        {
            for (int j = 0; j <= size.y - 1; j++)
            {
                Cell cell = Instantiate(cellPre, transform);
                cell.transform.position = new Vector3(i, j, 0);
                cell.id = id;
                cells.Add(cell);
                id++;
            }
        }
    }
    public void CheckDoneRow()
    {
        // int id = 0;
        // int t = 1;
        // for (int i = 0; i <= size.x - 1; i++)
        // {
        //     if (t >= 8)
        //     {
        //         // print(t);
        //         SignalBus.I.FireSignal<ResetCellSignal>(new ResetCellSignal(id - 7, id));
        //     }
        //     for (int j = 0; j <= size.y - 1; j++)
        //     {
        //         if (cells[id].cellState == CellState.active)
        //         {
        //             t++;
        //         }
        //         else
        //         {
        //             t = 0;
        //         }
        //         id++;
        //     }
        // }
        // int o = 1;
        // foreach (Cell cell in cells)
        // {
        //     if (cell.cellState == CellState.active)
        //     { print(o); o++;}
        // }
        // int i = 1;
        // int r = 1;
        // foreach (Cell cell in cells)
        // {
        //     if (cell.cellState == CellState.active)
        //     {
        //         if (i == 8)
        //         {
        //             SignalBus.I.FireSignal<ResetCellSignal>(new ResetCellSignal(r - 7, r));
        //         }
        //         i++;
        //     }
        //     else
        //     {
        //         i = 1;
        //     }
        //     r++;
        // }
        List<Cell> cellsDone1 = new List<Cell>();
        for (int i = 0; i <= 7; i++)
        {
            int q = 0;
            for (int j = 0; j <= 7; j++)
            {
                for (int t = j; t <= 63; t += 8)
                {
                    if (cells[t].cellState == CellState.active)
                    {
                        if (cells[t].transform.position.y == i)
                        {
                            q++;
                            cellsDone1.Add(cells[t]);
                        }
                        else
                        {
                            q = 0;
                            cellsDone1.Clear();
                        }
                    }
                    print(cellsDone1.Count);
                    if (q >= 8)
                    {
                        foreach (Cell cell in cellsDone1)
                        {
                            Destroy(cell.transform.GetChild(1).gameObject);
                            cell.SetState(CellState.free);
                        }
                    }
                }
            }
        }
        List<Cell> cellsDone = new List<Cell>();
        for (int k = 0; k <= 7; k++)
        {
            int m = 0;
            for (int h = 0; h <= 63; h++)
            {
                if (cells[h].cellState == CellState.active)
                {
                    if (cells[h].transform.position.x == k)
                    {
                        m++;
                        cellsDone.Add(cells[h]);
                    }
                    else
                    {
                        m = 0;
                        cellsDone.Clear();
                    }
                }
                // print(cellsDone.Count);
                if (m >= 8)
                {
                    foreach (Cell cell in cellsDone)
                    {
                        Destroy(cell.transform.GetChild(1).gameObject);
                        cell.SetState(CellState.free);
                    }
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
}
