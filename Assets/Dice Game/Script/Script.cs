using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Singleton<T> : MonoBehaviour where T : Component
{
    public static T instance;

    protected virtual void Awake()
    {
        if (instance == null)
            instance = this as T;
        else
            DestroyImmediate(this);
    }
}

[CreateAssetMenu(fileName ="Dice",menuName ="Roller Settings")]
public class DiceData:ScriptableObject
{
    public float rollTime;
    public float showTime;
    public int[] sideValue = {1,2,3,4,5,6};
    public IDice dice;
}

public interface IRoller
{
    public void Roll();
}

public interface IDice
{
    public void Roll();
    public void Stop();
}
public interface IDiceListener
{
    public void WaitForResult(Dice dice);
    public void WaitForPublish(Dice dice);
    public void ReceiveResult(Dice dice,int result);
}


public interface ICell:ICharacterListener
{  
    public Sprite itemIcon {  get; set; }
    public void Trigger();
}

public interface IContainer
{
    public void Add(ResourceCell source,float amount);
}

public static class Extension
{
    public static void WaitForSeconds(this MonoBehaviour mono, float seconds, Action action)
    {
        mono.StartCoroutine(IWaitForSeconds(seconds, action));
    }
    static IEnumerator IWaitForSeconds(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }
}

public interface IEffect
{
    public void Apply();
}

public interface ICharacterListener
{
    //public void OnCharacterMove();
    public void OnCharacterStopped();
}