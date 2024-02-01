using System;
using System.Collections;
using System.Collections.Generic;
using EgdFoundation;
using UnityEngine;
public enum BlockState
{
    free,
    hover,
    active
}
public class Block : MonoBehaviour
{
    public BlockState blockState = BlockState.free;
    [SerializeField] private GameObject objectRay, objectRay2;
    [SerializeField] private LayerMask layerMask;
    private List<Cell> cells = new List<Cell>();
    Cell cell;
    private void Awake()
    {
        RegisterEvents();
    }
    private void OnDestroy()
    {
        UnRegisterEvents();
    }
    private void RegisterEvents()
    {
        SignalBus.I.Register<MouseButtonUpSignal>(MouseButtonUpHandle);
        SignalBus.I.Register<InBroadSignal>(InBroadHandle);
        SignalBus.I.Register<OutBroadSignal>(OutBroadHandle);
    }
    private void UnRegisterEvents()
    {
        SignalBus.I.Unregister<MouseButtonUpSignal>(MouseButtonUpHandle);
        SignalBus.I.Unregister<InBroadSignal>(InBroadHandle);
        SignalBus.I.Unregister<OutBroadSignal>(OutBroadHandle);
    }

    private void OutBroadHandle(OutBroadSignal signal)
    {
        ResetCells();
        cell = null;
        blockState = BlockState.free;
    }


    private void InBroadHandle(InBroadSignal signal)
    {
        RaycastHit2D hit = Physics2D.Raycast(objectRay2.transform.position, objectRay2.transform.up, 0, layerMask);
        if (hit.collider != null)
        {
            cell = hit.collider.GetComponent<Cell>();
        }
        if (cell == null) return;
        if (cell.cellState == CellState.active)
        {
            ResetCells();
            blockState = BlockState.free;
        }
        if (cell.cellState == CellState.free)
        {
            ResetCells();
            cells.Add(cell);
            cell.SetState(CellState.hover);
            blockState = BlockState.hover;
        }
        Blocks blocks = transform.parent.GetComponent<Blocks>();
        if (blocks.blocksState == BlocksState.free)
        {
            ResetCells();
            cell = null;
        }
    }


    private void MouseButtonUpHandle(MouseButtonUpSignal signal)
    {
        if (cell != null && blockState == BlockState.hover)
        {
            transform.SetParent(cell.transform);
            transform.localPosition = Vector2.zero;
            Destroy(this);
            cell.SetState(CellState.active);
            blockState = BlockState.active;
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
