using System;
using System.Collections;
using System.Collections.Generic;
using EgdFoundation;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
public enum CellState
{
    free,
    hover,
    active
}
public class Cell : MonoBehaviour
{
    public int id;
    public CellState cellState = CellState.free;
    [SerializeField] private SpriteRenderer stateSprite;
    [SerializeField] private Sprite free, hover, active;
    public void SetState(CellState state)
    {
        cellState = state;
        switch (state)
        {
            case CellState.free:
                stateSprite.sprite = free;
                break;
            case CellState.hover:
                stateSprite.sprite = hover;
                break;
            case CellState.active:
                cellState = CellState.active;
                stateSprite.sprite = active;
                break;
        }
    }
    private void Awake()
    {
        SignalBus.I.Register<ResetCellSignal>(ResetCellHandle);
    }
    private void OnDestroy()
    {
        SignalBus.I.Unregister<ResetCellSignal>(ResetCellHandle);
    }

    private void ResetCellHandle(ResetCellSignal signal)
    {
        if (id >= signal.minId && id <= signal.maxId)
        {
            Destroy(transform.GetChild(1).gameObject);
            SetState(CellState.free);
        }
    }

}
