using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Character : Singleton<Character>,IDiceListener
{
    float travelTime;
    Animator animator;
    public int startLocation;
    public Vector3 offset;
    public float speed=0.1f;
    public int currentLoc {  get;  set; }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        int n = 0;
        foreach (int i in UIManager.CELL_MAP)
            if (i != 0)
                n++;
        Debug.Log(n);
        travelTime = 1 / (n*speed);
    }

    public void Move(int step,bool invert)
    {
        Invoke(nameof(Stop), travelTime * step);
        animator.SetFloat("multipler",speed);
        Debug.Log(animator.speed);
    }

    public void Stop()
    {
        animator.SetFloat("multipler", 0);
        Debug.Log("Done");
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
