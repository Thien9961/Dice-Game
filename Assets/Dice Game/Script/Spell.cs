using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public abstract class Spell : MonoBehaviour
{
    public virtual void StartEffect()
    {
        DestroyImmediate(transform.parent.parent.parent.gameObject);
    }
}
