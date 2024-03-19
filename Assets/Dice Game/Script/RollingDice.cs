using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SkeletonGraphic))]
[RequireComponent (typeof(AudioSource))]
public class RollingDice : MonoBehaviour,IDiceListener
{
    SkeletonGraphic animator;
    public Sprite[] sides;
    public Image result,shadow;
    public AudioClip clip;
    public DiceType type;
    static readonly string[] ANIMATION_NAME= { "GreenRoll", "RedRoll" };
    Image dice;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<SkeletonGraphic>();
    }
    public void WaitForResult(Dice dice)
    {
        animator.AnimationState.SetAnimation(0, ANIMATION_NAME[(int)type],true);
    }
    public void WaitForPublish(Dice dice, int result)
    {
        this.dice = Instantiate(this.result, Instantiate(shadow, transform).transform);
        this.dice.sprite = sides[result];
        animator.enabled = false;
    }


    public void ReceiveResult(Dice dice, int result)
    {
        DestroyImmediate(gameObject);
    }
}

public enum DiceType
{
    WHITE=0,RED=1
}
