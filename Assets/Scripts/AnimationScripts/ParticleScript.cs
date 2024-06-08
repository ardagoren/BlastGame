using System.Collections;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(DestroyAfterDelay(3f));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject); 
    }
}
