using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoalScript : MonoBehaviour
{
    public bool filled;

    private string obstacleString;
    private LevelManager lvlManager;

    private TextMeshProUGUI counterText;
    public void Initialize(string obstacleString,LevelManager lvlManager)
    {
        this.obstacleString = obstacleString;
        this.lvlManager = lvlManager;
        ChangeSprite();
    }
    void Start()
    {
        counterText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        //EĞER OBSTACLE VARSA OBSTACLE SAYISINI YAZDIR VE 0'LANINCA CHİLD OBJEM OLAN VE GÖREVİN TAMAMLANDIĞINI GÖSTEREN TİK İŞARETİMİ AKTİF ET
        if (filled == true)
        {
            int obstacleCount = lvlManager.ObstacleCounter(obstacleString);
            counterText.text = obstacleCount.ToString();
            if (obstacleCount == 0)
            {
                transform.GetChild(1).gameObject.SetActive(true);
                counterText.text = null;
            }
        }

    }

    //SPRİTE'IMI OBJE İSMİNE GÖRE RESOURCES KLASÖRÜNDEN BULUP DEĞİŞTİR
    public void ChangeSprite()
    {
        Sprite newSprite = Resources.Load<Sprite>("Sprites/" + obstacleString);
        if (newSprite != null)
        {
            GetComponent<Image>().sprite = newSprite;
        }
       
    }

}
