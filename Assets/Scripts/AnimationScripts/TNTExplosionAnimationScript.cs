using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TNTExplosionAnimationScript : MonoBehaviour
{
    public float scaleMultiplier = 7f; 
    public float duration = 0.2f; 
  
    void Start()
    {
        transform.DOScale(transform.localScale * scaleMultiplier, duration)
            .OnComplete(DestroyGameObject); 
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
