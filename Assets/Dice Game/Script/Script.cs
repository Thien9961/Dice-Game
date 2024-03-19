using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.FilePathAttribute;

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
    public void WaitForPublish(Dice dice,int result);
    public void ReceiveResult(Dice dice,int result);
}


public interface ICell
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

public interface ICharacterListener
{
    //public void OnCharacterMove();
    public void OnCharacterStopped();

}

public interface ICharacterStatus
{
    Character character { get; set; }
    static int step = 0;
    bool turnConditionA { get; set; }
    bool turnConditionB { get; set; }
    public void Apply(ref int step,ref int i);
}

public class Normal : ICharacterStatus
{
    public Character character { get; set; }
    public bool turnConditionA { get; set; }
    public bool turnConditionB { get; set; }

    public Normal(Character character)
    {
        this.character = character;
        turnConditionA = character.location > 7 && character.location <= UIManager.instance.playableArea.Length - 1;
        turnConditionB = character.location <= 7;
    }
    public void Apply(ref int step,ref int i)
    {
        if (character.location + 1 < UIManager.instance.playableArea.Length)
            character.location++;
        else
        {
            character.location = 0;
            UIManager.instance.laps++;
        }
        if (turnConditionA)
            character.transform.rotation = Quaternion.Euler(0, 180, 0);
        else if (turnConditionB)
            character.transform.rotation = Quaternion.Euler(0, 0, 0);
        Debug.Log("Location " + character.location);
        character.path[i] = character.walkablePath.GetChild(character.location).position + character.offset;
        step--;i++;
    }
}

public class Dizzy : ICharacterStatus
{
    public bool turnConditionA { get; set; }
    public bool turnConditionB { get; set; }
    public Character character { get; set; }

    public Dizzy(Character character)
    {
        this.character = character;
        turnConditionB = character.location > 7 && character.location <= UIManager.instance.playableArea.Length - 1;
        turnConditionA = character.location <= 7;
    }
    public void Apply(ref int step,ref int i) 
    {
        if (character.location - 1 > 0)
            character.location--;
        else 
        { 
            character.location = UIManager.instance.playableArea.Length-1;
            UIManager.instance.laps--;
        }
        if (character.location >= 10)
            character.transform.rotation = Quaternion.Euler(0,0, 0);
        else if (character.location >= 0)
            character.transform.rotation = Quaternion.Euler(0, 180, 0);
        character.path[i] = character.walkablePath.GetChild(character.location).position + character.offset;
        step--;i++;
    }
}

public class Double : ICharacterStatus
{
    public bool turnConditionA { get; set; }
    public bool turnConditionB { get; set; }
    public Character character { get; set; }
    public bool doubled;
    public Double(Character character)
    {
        this.character = character;
        doubled = false;
        turnConditionA = character.location > 7 && character.location <= UIManager.instance.playableArea.Length - 1;
        turnConditionB = character.location <= 7;
    }
    public void Apply(ref int step,ref int i) 
    {
        if (doubled == false)
        {
            step *= 2;
            character.path=new Vector3[step];
            doubled = true;
        }            
        if (character.location + 1 < UIManager.instance.playableArea.Length)
            character.location++;
        else
            character.location = 0;
        if (turnConditionA)
            character.transform.rotation = Quaternion.Euler(0, 180, 0);
        else if (turnConditionB)
            character.transform.rotation = Quaternion.Euler(0, 0, 0);
        character.path[i] = character.walkablePath.GetChild(character.location).position + character.offset;
        step--;i++;
    }
}

public abstract class State
{
    public SkeletonGraphic skeleton;
    public Queue<string> name { get; set; }
    public bool loop { get;set; }
    public int track { get; set; }
    public float delay { get; set; }

    public State(Queue<string> name, bool loop, int track, float delay)
    {
        this.name = name;
        this.loop = loop;
        this.track = track;
        this.delay = delay;
        foreach (string s in name)
        {
            skeleton.AnimationState.AddAnimation(track, s, loop, delay);
        }
        this.delay = delay;
        Behavior();
    }

    public State()
    {

    }

    public virtual void Behavior()
    {
        skeleton.AnimationState.SetAnimation(track, name.Peek(), loop);
    }
}