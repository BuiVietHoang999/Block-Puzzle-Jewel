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
            if (hit.collider != null)
                hit.transform.parent.parent.position = mousePosition;
            //hit.collider.GetComponent<Block>().GetNearestObject();
            //broad.GetNearestObject(hit.transform);
            //broad.GetCell();
        }

        if (!isDrag) broad.CheckDoneRow();

        // if (isDrag && hit.collider.gameObject != null && hit.collider.gameObject.GetComponent<Block>().blockState == BlockState.free)
        // {
        //     broad.GetNearestObject(hit.transform.position);
        //     hit.transform.position = mousePosition;
        // }
        // if (!isDrag && hit.collider.gameObject != null)
        // {
        //     Cell cell = broad.GetNearestObject(hit.transform.position);
        //     if (hit.transform.position.x > 0 && hit.transform.position.y < 0 && hit.transform.position.y < -hit.collider.bounds.size.y
        //     && hit.transform.position.x < 8 - hit.collider.bounds.size.x)
        //     {
        //         hit.transform.position = cell.transform.position;
        //         cell.cellState = CellState.active;
        //         //hit.collider.gameObject.GetComponent<Block>().blockState = BlockState.active;
        //     }
        //     else
        //     {
        //         hit.transform.position = new Vector2(2, -12);
        //     }
        // }
    }
}
