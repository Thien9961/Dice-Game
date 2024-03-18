using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSpell : Spell
{
    public override void StartEffect()
    {
        Character.instance.status = new Double(Character.instance);
        base.StartEffect();
    }
}
