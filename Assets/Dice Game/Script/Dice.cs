using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Unity.VisualScripting;
using UnityEngine;


public class Dice : MonoBehaviour,IDice
{
    public float rollTime, showTime;
    public Side[] sides;
    public static List<IDiceListener> listener = new();
    public RollingDice rollingDice;

    protected int result;

    // Start is called before the first frame update
    public virtual void Roll()
    {
        listener.Add(Instantiate(rollingDice, UIManager.instance.transform));
        Extension.WaitForSeconds(this, rollTime, Stop);
        listener.ForEach(x=>x.WaitForResult(this));
    }

    public virtual void Stop()
    {
        result = Random.Range(0, sides.Length);
        listener.ForEach(x => x.WaitForPublish(this,result));
        Extension.WaitForSeconds(this,showTime, PublishResult);
    }

    public virtual void PublishResult()
    {
        listener.ForEach(x => x.ReceiveResult(this,result));
        DestroyImmediate(gameObject);
    }
    protected virtual void Awake()
    {
        if (!listener.Contains(WhiteRoller.instance))
            listener.Add(WhiteRoller.instance);
        if(!listener.Contains(RedRoller.instance))
            listener.Add(RedRoller.instance);
        if(!listener.Contains(UIManager.instance))
            listener.Add(UIManager.instance);
        if(!listener.Contains(Character.instance))
            listener.Add(Character.instance);
    }
}
