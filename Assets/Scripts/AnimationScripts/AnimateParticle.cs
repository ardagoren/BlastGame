using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateParticle : MonoBehaviour
{
    [SerializeField]
    private GameObject[] fragmentPrefabs; 
    public void InstantiateParticle()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject fragment = Instantiate(fragmentPrefabs[i], transform.position, Quaternion.identity);

            Rigidbody2D rb = fragment.GetComponent<Rigidbody2D>();

            // Parçaların yönlerini belirle
            Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

            // Parçaları belirli bir hızla hareket ettir
            float force = 5f;
            rb.AddForce(direction * force, ForceMode2D.Impulse);
        }
    }

}


