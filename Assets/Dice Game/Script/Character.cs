using DG.Tweening;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SkeletonGraphic))]
public class Character : Singleton<Character>,IDiceListener
{
    public float travelTime=0.5f;
    SkeletonGraphic animator;
    public int location,direction;
    public Vector3 offset;
    public ICharacterStatus status;
    public Transform walkablePath { get;set; }
    public Vector3[] path { get;set; }
    AudioSource speaker;
    public static readonly string RUNNING = "run",IDLING="idling";
    // Start is called before the first frame update
    void Start()
    {
        speaker = GetComponent<AudioSource>();speaker.loop=true;
        animator = GetComponent<SkeletonGraphic>();
        transform.position = walkablePath.GetChild(location).position+offset;
        status = new Normal(this);
    }

    public void Move(int step,bool invert)
    {
        speaker.Play();
        animator.AnimationState.SetAnimation(0, RUNNING, true);
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
        speaker.Stop();
        animator.AnimationState.SetAnimation(0, IDLING, true);
        status = new Normal(this);
        if (UIManager.instance.playableArea[location].TryGetComponent(out ICell cell))
            cell.Trigger();
    }
    public void WaitForResult(Dice dice)
    {
        
    }
    public void WaitForPublish(Dice dice, int r)
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
