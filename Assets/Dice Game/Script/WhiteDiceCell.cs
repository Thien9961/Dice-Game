using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteDiceCell : ResourceCell
{
    // Start is called before the first frame update
    protected override void Start()
    {
        storage.Add( WhiteRoller.instance);
        base.Start();
    }

    public override void Trigger()
    {
        base.Trigger();
        level = 1;
    }

}
