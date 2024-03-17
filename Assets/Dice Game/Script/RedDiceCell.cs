using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDiceCell : ResourceCell
{
    // Start is called before the first frame update
    protected override void Awake()
    {
        storage.Add(RedRoller.instance);
        base.Awake();
    }
}
