using DG.Tweening;
using Spine.Unity;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SkeletonGraphic))]
public class Character : Singleton<Character>,IDiceListener
{
    public float travelTime=0.5f;
    public SkeletonGraphic animator { get; set; }
    public int location,direction;
    public Vector3 offset;
    public ICharacterStatus status;
    public Transform walkablePath { get;set; }
    public Vector3[] path { get;set; }
    AudioSource speaker;
    public static readonly string RUNNING = "run",IDLING="idling",STUNNED="stun";
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

        if(DOTween.IsTweening(transform)) 
        {
            Debug.Log("Tweening");
            bool a = false,b = false;
            if (typeof(Dizzy) != status.GetType())
            {
                 a = transform.position.x <= walkablePath.GetChild(10).position.x + offset.x;
                 b = transform.position.y > walkablePath.GetChild(10).position.y + offset.y+100;
                Debug.Log("a");
            }
            else
            {
                Debug.Log("b");
                b = transform.position.x <= walkablePath.GetChild(10).position.x + offset.x;
                 a = transform.position.y > walkablePath.GetChild(10).position.y + offset.y+100;
            }
            if (a && b)
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else if(!b)
                transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
