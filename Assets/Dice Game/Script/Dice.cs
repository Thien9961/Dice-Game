using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class Dice : MonoBehaviour,IDice
{
    public float rollTime, showTime;
    public Side[] sides;
    public static List<IDiceListener> listener= new List<IDiceListener>();

    // Start is called before the first frame update
    public virtual void Roll()
    {
        Extension.WaitForSeconds(this, rollTime, Stop);
        listener.ForEach(x=>x.WaitForResult(this));
    }

    public virtual void Stop()
    {
        listener.ForEach(x => x.WaitForPublish(this));
        Extension.WaitForSeconds(this,showTime, PublishResult);
    }

    public virtual void PublishResult()
    {
        listener.ForEach(x => x.ReceiveResult(this,Random.Range(0,sides.Length+1)));
        DestroyImmediate(gameObject);
    }
    protected virtual void Awake()
    {
        listener.Add(WhiteRoller.instance);
        listener.Add(RedRoller.instance);
        listener.Add(UIManager.instance);
    }
}
