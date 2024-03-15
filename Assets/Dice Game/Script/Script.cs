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

[CreateAssetMenu(fileName = "Cell", menuName = "Cell Settings")]
public class CellSetting : ScriptableObject
{
    public Sprite 
}

public enum CellType
{
    RESOURCE,EFFECT
}

public interface ICell
{
    public int level { get; set; }
    public CellType type { get; set; }
    public bool trigger { get; set; }
    public bool Trigger();
}