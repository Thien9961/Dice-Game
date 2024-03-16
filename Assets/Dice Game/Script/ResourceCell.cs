using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class ResourceCell : MonoBehaviour,ICell
{
    public ResourceCellData data;
    public event Action<float,int> onTrigger;
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

    public void Trigger()
    {
        onTrigger.Invoke(value,level);
    }



    // Start is called before the first frame update
    void Start()
    {
        itemIcon = data.itemIcon;
        value = data.value;
        level = data.level;
        onTrigger += UIManager.instance.Reward(value,level);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
