using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DizzyCell : Cell
{
    public override void Trigger()
    {
        Character.instance.status=new Dizzy(Character.instance);
    }
}
