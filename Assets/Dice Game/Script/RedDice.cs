using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RedDice : Dice
{
    public override void Roll()
    {
        GetComponent<Button>().enabled = false;
        Extension.WaitForSeconds(this, rollTime, Stop);
        listener.ForEach(x => x.WaitForResult(this));
    }

    public override void PublishResult()
    {
        listener.ForEach(x => x.ReceiveResult(this, 0));
        DestroyImmediate(gameObject);
    }
}
