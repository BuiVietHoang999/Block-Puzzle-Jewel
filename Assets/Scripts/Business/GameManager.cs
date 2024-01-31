using System.Collections;
using System.Collections.Generic;
using EgdFoundation;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    public bool isDrag;
    [SerializeField] Broad broad;
    RaycastHit2D hit;

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            isDrag = true;

        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;
        }

        if (isDrag)
        {
            if (hit.collider != null && hit.collider.CompareTag("blocks"))
            {
                Blocks blocks = hit.collider.GetComponent<Blocks>();
                if (blocks != null) blocks.OnDragBlocks(mousePosition);
            }
        }
        else
        {
            broad.CheckDoneRow();
            SignalBus.I.FireSignal<MouseButtonUpSignal>(new MouseButtonUpSignal());
        }
    }
}
