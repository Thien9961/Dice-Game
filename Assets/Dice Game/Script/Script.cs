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

public interface ICharacterListener
{
    //public void OnCharacterMove();
    public void OnCharacterStopped();

}

public interface ICharacterStatus
{
    Character character { get; set; }
    static int step = 0;
    public void Apply(ref int step,ref int i);
}

public class Normal : ICharacterStatus
{
    public Character character { get; set; }
    

    public Normal(Character character)
    {
        this.character = character;
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
            
        character.path[i] = character.walkablePath.GetChild(character.location).position + character.offset;
        step--;i++;
    }
}

public class Dizzy : ICharacterStatus
{
    public Character character { get; set; }

    public Dizzy(Character character)
    {
        this.character = character;
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
        character.path[i] = character.walkablePath.GetChild(character.location).position + character.offset;
        step--;i++;
    }
}

public class Double : ICharacterStatus
{
    public Character character { get; set; }
    public bool doubled;
    public Double(Character character)
    {
        this.character = character;
        doubled = false;
    }
    public void Apply(ref int step,ref int i) 
    {
        if (doubled == false)
        {
            step *= 2;
            doubled = true;
        }            
        if (character.location + 1 < UIManager.instance.playableArea.Length)
            character.location++;
        else
            character.location = 0;
        character.path[i] = character.walkablePath.GetChild(character.location).position + character.offset;
        step--;i++;
    }
}