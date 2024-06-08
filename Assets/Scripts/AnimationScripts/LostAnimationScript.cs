using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class LostAnimationScript : MonoBehaviour
{
    public float moveAmount = 5f;
    public float duration = 1f;
    void Start()
    {
        transform.DOMoveY(transform.position.y - moveAmount*6, duration);
    }

}
