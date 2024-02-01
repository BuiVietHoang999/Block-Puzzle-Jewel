using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EgdFoundation;
using UnityEngine;

public enum BlocksState
{
    free,
    hover,
    active
}
public class Blocks : MonoBehaviour
{
    public BlocksState blocksState = BlocksState.free;
    [SerializeField] public Vector2 size;
    [SerializeField] public int row;
    [SerializeField] public GameObject blockPre;
    private Vector2 startPos;
    private void Awake()
    {
        RegisterEvents();
    }
    private void OnDestroy()
    {
        UnRegisterEvents();
    }
    private void Start()
    {
        startPos = transform.position;
        UpdateBlocksHandle();
    }
    private void RegisterEvents()
    {
        SignalBus.I.Register<UpdateBlocksSignal>(UpdateBlocksHandle);
        SignalBus.I.Register<MouseButtonUpSignal>(MouseButtonUpHandle);
    }
    private void UnRegisterEvents()
    {
        SignalBus.I.Unregister<UpdateBlocksSignal>(UpdateBlocksHandle);
        SignalBus.I.Unregister<MouseButtonUpSignal>(MouseButtonUpHandle);
    }

    private void MouseButtonUpHandle(MouseButtonUpSignal signal)
    {
        OnDropBlocks();
    }

    void UpdateBlocksHandle(UpdateBlocksSignal signal = null)
    {
        row = Random.Range(1, 4);
        size = new Vector2(Random.Range(1, 4), row == 1 ? Random.Range(1, 4) : row);
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < size.x; j++)
            {
                GameObject block = Instantiate(blockPre, transform);
                block.transform.localPosition = new Vector3(j, i, 0);
            }
            if (row == 1)
            {
                for (int k = i + 1; k < size.y; k++)
                {
                    GameObject block = Instantiate(blockPre, transform);
                    block.transform.localPosition = new Vector3(size.x - 1, k, 0);
                }
            }
        }

        // List<float> listPosX = new List<float>();
        // List<float> listPosY = new List<float>();
        // foreach (Transform child in transform)
        // {
        //     listPosX.Add(child.localPosition.x);
        //     listPosY.Add(child.localPosition.y);
        // }
        // size = new Vector2(listPosX.Max() + 1, listPosY.Max() + 1);
    }
    public void OnDragBlocks(Vector3 followPos)
    {
        transform.position = followPos;
        transform.localScale = Vector3.one / 5;
        if (transform.position.x > 0 && transform.position.x <= (8 - size.x)
        && transform.position.y > 0 && transform.position.y <= (8 - size.y))
        {
            SignalBus.I.FireSignal<InBroadSignal>(new InBroadSignal());
            if (CheckBlockStateSatisfied())
            {
                blocksState = BlocksState.hover;
            }
            else
            {
                blocksState = BlocksState.free;
            }
        }
        else
        {
            blocksState = BlocksState.free;
            SignalBus.I.FireSignal<OutBroadSignal>(new OutBroadSignal());
        }
    }
    private void OnDropBlocks()
    {
            blocksState = BlocksState.free;
        transform.position = startPos;
        transform.localScale = Vector3.one / 10;
    }
    private bool CheckBlockStateSatisfied()
    {
        foreach (Transform block in transform)
        {
            if (block.GetComponent<Block>().blockState != BlockState.hover) return false;
        }
        return true;
    }
}
