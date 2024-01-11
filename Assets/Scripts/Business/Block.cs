using System;
using System.Collections;
using System.Collections.Generic;
using EgdFoundation;
using UnityEngine;
public enum BlockState
{
    free,
    active
}
public class Block : MonoBehaviour
{
    public BlockState blockState = BlockState.free;
    [SerializeField] private GameObject objectRay;
    private List<Cell> cells = new List<Cell>();

    private void Awake()
    {
        SignalBus.I.Register<ResetCellsSignal>(ResetCellsHandle);
    }
    private void OnDestroy()
    {
        SignalBus.I.Unregister<ResetCellsSignal>(ResetCellsHandle);
    }

    private void ResetCellsHandle(ResetCellsSignal signal)
    {
        print("aaa");
        ResetCells();
        Blocks blocks = transform.parent.GetComponent<Blocks>();
        blocks.isOk = true;
    }


    public void Update()
    {
        Blocks blocks = transform.parent.GetComponent<Blocks>();
        if (blocks.transform.position.x > 0 && blocks.transform.position.x <= (8 - blocks.size.x)
        && blocks.transform.position.y > 0 && blocks.transform.position.y <= (8 - blocks.size.y))
        {
            RaycastHit2D hit = Physics2D.Raycast(objectRay.transform.position, objectRay.transform.up, 2);
            if (hit.collider == null) return;
            Cell cell = hit.collider.gameObject.GetComponent<Cell>();
            if (cell == null) return;
            if (cell.cellState == CellState.active)
            {
                print("fire");
                SignalBus.I.FireSignal<ResetCellsSignal>(new ResetCellsSignal());
            }
            // else
            // {
            //     blocks.isOk = false;
            // }
            if (!blocks.isOk && hit.distance < .5f && cell.cellState == CellState.free)
            {
                ResetCells();
                cells.Add(cell);
                cell.SetState(CellState.hover);

            }
            else if (hit.distance >= .5f && cell.cellState == CellState.hover)
            {
                ResetCells();
            }

            if (!GameManager.I.isDrag && blockState == BlockState.free)
            {
                transform.SetParent(cell.transform);
                transform.localPosition = Vector2.zero;
                this.enabled = false;
                this.GetComponentInChildren<Collider2D>().enabled = false;
                cell.SetState(CellState.active);
                blockState = BlockState.active;
            }
        }
        else
        {
            ResetCells();
        }
    }
    private void ResetCells()
    {
        foreach (Cell cell in cells)
        {
            cell.SetState(CellState.free);
        }
        cells.Clear();

    }
}
