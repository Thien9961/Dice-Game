using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : Spell
{
    public override void StartEffect()
    {
        var rng = Random.Range(0, UIManager.instance.playableArea.Length); ResourceCell cell;
        while (UIManager.instance.playableArea[rng].TryGetComponent(out cell) == false)
        {
            rng = Random.Range(0, UIManager.instance.playableArea.Length);
        }
        cell.Trigger();
        base.StartEffect();
    }
}
