using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDice : Dice
{
    public int guarantee;
    public override void PublishResult()
    {
        listener.ForEach(x => x.ReceiveResult(this, guarantee));
        DestroyImmediate(gameObject);
    }
}
