using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour,ICell
{
    public Sprite itemIcon { get { return item.GetComponent<Image>().sprite; } set { item.GetComponent<Image>().sprite = value; } }
    public Transform item;
    public virtual void Trigger()
    {
        Debug.Log(name + " trigger");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
