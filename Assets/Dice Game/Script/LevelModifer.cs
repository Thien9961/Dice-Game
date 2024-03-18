using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelModifer : Spell
{
    public int value = 1;
    public override void StartEffect()
    {
        var rng = Random.Range(0, UIManager.instance.playableArea.Length);ResourceCell cell;
        while (UIManager.instance.playableArea[rng].TryGetComponent(out cell) ==false)
        {
            rng = Random.Range(0, UIManager.instance.playableArea.Length);
        }
        cell.level+=value;
        base.StartEffect();
    }
}
