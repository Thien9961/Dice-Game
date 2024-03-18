using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDiceCell : ResourceCell
{
    // Start is called before the first frame update
    protected override void Start()
    {
        storage.Add(RedRoller.instance);
        base.Start();
        valueTxt.color = Color.clear;
        levelTxt.color = Color.clear;


    }

    public override void Trigger()
    {
        base.Trigger();
        level = 1;
    }
}
