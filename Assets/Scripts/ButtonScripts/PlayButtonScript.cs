using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayButtonScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    void Start()
    {
        text.text = "Level "+ DataManager.Instance.GetLevelData();
    }

    void Update()
    {
        if (DataManager.Instance.GetLevelData() == 11)
        {
            text.text = "Finished!";
            GetComponent<Button>().interactable = false;
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(DataManager.Instance.GetLevelData());
    }
}
