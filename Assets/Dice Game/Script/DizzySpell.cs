using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DizzySpell : Spell
{
    public override void StartEffect()
    {
        Character.instance.status = new Dizzy(Character.instance);
        base.StartEffect();
    }
}
