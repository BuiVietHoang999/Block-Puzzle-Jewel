using System.Collections;
using System.Collections.Generic;
using EgdFoundation;
using UnityEngine;

public class Booster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CheckDoneBooster())
        {
            SignalBus.I.FireSignal<UpdateBlocksSignal>(new UpdateBlocksSignal());
        }
    }
    private bool CheckDoneBooster()
    {
        foreach (Transform blocks in transform)
        {
            if (blocks.childCount != 0) return false;
        }
        return true;
    }
}
