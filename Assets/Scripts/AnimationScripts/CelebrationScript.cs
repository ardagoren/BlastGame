using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class CelebrationScript : MonoBehaviour
{
    private Vector3 targetScale = new Vector3(3.5f, 3.5f, 3.5f);                

    private Vector3 rotationSpeed = new Vector3(0f, 0f, -120f);                   

    void Start()
    {
        StartCoroutine(ReturnMainMenu(3f));
    }

    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, Space.Self);
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale,Time.deltaTime);
    }

    IEnumerator ReturnMainMenu(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MainMenu");
    }
}

