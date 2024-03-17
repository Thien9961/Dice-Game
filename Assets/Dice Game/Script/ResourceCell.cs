using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class ResourceCell : MonoBehaviour,ICell
{
    public delegate void AddResource(ResourceCell cell,float value);
    public event AddResource onTrigger;
    public List<IContainer> storage=new();
    public Sprite itemIcon { get { return item.GetComponent<SpriteRenderer>().sprite; } set { item.GetComponent<SpriteRenderer>().sprite = value; } }
    public float value { get {
            string value=string.Empty;
            foreach (char character in valueTxt.text)
            {
                if (char.IsDigit(character))
                    value += character;
                else if (character == 'K')
                    value += "000";     
            }
            return float.Parse(value); }
        set
        {
            if (value < 10000)
                valueTxt.text = value.ToString();
            else
                valueTxt.text = (value/1000).ToString()+'K';
        }
    }
    public int level { get { int indexOfNumber = levelTxt.text.LastIndexOf('.'); return int.Parse(levelTxt.text.Substring(indexOfNumber + 1)); }  set { levelTxt.text = "Lv."+value.ToString(); } }
    public TextMeshProUGUI levelTxt,valueTxt;
    public Transform item;

    public void OnCharacterStopped()
    {
        Trigger();
    }
    public void Trigger()
    {
        onTrigger.Invoke(this,value * level);
    }

    protected virtual void Awake()
    {
        storage.Add(UIManager.instance);
        foreach (IContainer container in storage)
            onTrigger += container.Add;
    }

}
