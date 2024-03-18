using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Spell
{
    public int destination;
    public override void StartEffect()
    {
        var v = Character.instance;
        v.location = destination;
        v.transform.position=v.walkablePath.transform.GetChild(destination).position+v.offset;
        base.StartEffect();
    }
}
