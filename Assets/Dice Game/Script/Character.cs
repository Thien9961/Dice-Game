using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Singleton<Character>
{
    public bool isStunned;
    public float travelTime;
    public int startLocation;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = UIManager.instance.path[startLocation]+offset;

    }

    public void Move(int step,bool invert)
    {
        transform.DOPath(UIManager.instance.path.ToArray(), travelTime * step);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
