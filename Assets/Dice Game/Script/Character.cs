using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Character : Singleton<Character>,IDiceListener
{
    public float travelTime=0.5f;
    Animator animator;
    public int location,direction;
    public Vector3 offset;
    public ICharacterStatus status;
    public Transform walkablePath { get;set; }
    public Vector3[] path { get;set; }
    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
        transform.position = walkablePath.GetChild(location).position+offset;
        status = new Normal(this);
    }


    public void Move(int step,bool invert)
    {
        float f = step * travelTime;int i = 0;
        path=new Vector3[step];
        while (step != 0)
        {
            status.Apply(ref step,ref i);
        }
        transform.DOPath(path,f);
        Invoke(nameof(Stop), f);
    }

    public void Stop()
    {
        if(UIManager.instance.playableArea[location].TryGetComponent(out ICell cell))
            cell.Trigger();
    }
    public void WaitForResult(Dice dice)
    {
        
    }
    public void WaitForPublish(Dice dice)
    {

    }
    public void ReceiveResult(Dice dice, int result)
    {
        Move(dice.sides[result].value, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
