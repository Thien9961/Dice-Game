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

public interface IAdapter<A, T>
{
    public A adaptee { get; set; }
    public T Adapt();
}

public interface INotification
{
}
public interface ISubject
{
    public List<IObserver> observers { get; set; }

    public void Notify(INotification @event)
    {
        for (int i = 0; i < observers.Count; i++)
            observers[i].Response(@event);
    }

    public void Attach(IObserver observer, INotification @event, Action action)
    {
        if (observers == null)
            observers = new List<IObserver>();
        if (observer.responses == null)
            observer.responses = new Hashtable();
        if (!observers.Contains(observer))
            observers.Add(observer);
        observer.responses.Add(@event, action);
    }

    public void Detach(IObserver observer, INotification @event)
    {
        observers.Remove(observer);
        observer.responses.Remove(@event);
    }
}

public interface IObserver
{
    public Hashtable responses { get; set; }

    public void Response(INotification @event)
    {
        Action a = (Action)responses[@event];
        a?.Invoke();
    }
}

[CreateAssetMenu(fileName ="Roller",menuName ="Roller Settings")]
public class RollerSetting:ScriptableObject
{
    public float rollTime { get; set; }
    public float showTime { get; set; }
    public int dice { get; set; }
}

public interface IRoller
{
    public static readonly List<int> VALID_SCORE =new() { 1,2,3,4,5,6};
    public RollerSetting settings { get; set; }
    public bool active { get; set; }
    public void Roll();
    public int GetResult();
    public static List<GameObject> listener { get; set; }
}

public interface IDiceListener

{
    public void Wait(IRoller roller);
    public void Refresh();
}

[CreateAssetMenu(fileName = "Cell", menuName = "Cell Data")]
public class CellData : ScriptableObject
{
    public Sprite itemIcon;
}

[CreateAssetMenu(fileName = "Resource Cell", menuName = "Cell Data")]
public class ResourceCellData : CellData
{
    public float value;
    public int level;
}


public interface ICell
{  
    public Sprite itemIcon {  get; set; }
    public void Trigger();
}


public interface IWindow
{
    public Button exit { get; set; }
    public void Exit();
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


public enum Panel
{

}
public struct PanelDB
{
  
}