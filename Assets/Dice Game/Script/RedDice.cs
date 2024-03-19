using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RedDice : Dice
{
    public override void Roll()
    {
        listener.Clear();
        if (!listener.Contains(WhiteRoller.instance))
            listener.Add(WhiteRoller.instance);
        if (!listener.Contains(RedRoller.instance))
            listener.Add(RedRoller.instance);
        if (!listener.Contains(UIManager.instance))
            listener.Add(UIManager.instance);
        if (!listener.Contains(Character.instance))
            listener.Add(Character.instance);
        listener.Add(Instantiate(rollingDice, UIManager.instance.transform));
        GetComponent<Button>().enabled = false;
        GetComponent<Image>().enabled = false;
        Extension.WaitForSeconds(this, rollTime, Stop);
        listener.ForEach(x => x.WaitForResult(this));
    }

    public override void Stop()
    {
        result = Random.Range(0, sides.Length);
        listener.ForEach(x => x.WaitForPublish(this, sides[result].value-1));
        Extension.WaitForSeconds(this, showTime, PublishResult);
    }
}
