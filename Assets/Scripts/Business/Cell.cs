using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Sprite free, hover, active;
    public void SetState(CellState state)
    {
        cellState = state;
        switch (state)
        {
            case CellState.free:
                GetComponent<SpriteRenderer>().sprite = free;
                break;
            case CellState.hover:
                GetComponent<SpriteRenderer>().sprite = hover;
                break;
            case CellState.active:
                cellState = CellState.active;
                GetComponent<SpriteRenderer>().sprite = active;
                break;
        }
    }
}
