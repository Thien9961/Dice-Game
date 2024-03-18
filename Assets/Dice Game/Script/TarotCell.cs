using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TarotCell : Cell
{
    public TarotSelectionWindow window;
    public override void Trigger()
    {
        Instantiate(window,UIManager.instance.transform);
    }


}
