using System.Collections;
using System.Collections.Generic;
using EgdFoundation;
using UnityEngine;

public class ResetCellsSignal : Signal
{
}
public class UpdateBlocksSignal : Signal
{
}
public class MouseButtonUpSignal : Signal
{
}
public class InBroadSignal : Signal
{
}
public class OutBroadSignal : Signal
{
}
public class ResetCellSignal : Signal
{
    public int minId, maxId;

    public ResetCellSignal(int minId, int maxId)
    {
        this.minId = minId;
        this.maxId = maxId;
    }
}