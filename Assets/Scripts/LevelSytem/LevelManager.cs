using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI moveCountText;

    [SerializeField]
    private GameObject WinCanvas;
    [SerializeField]
    private GameObject LostCanvas;

    private GridManager gridManager;

    public List<string> obstacleList;

    public GameObject[] emptyGoals;

    private Dictionary<string, IObstacleCounter> obstacleCounters;
    void Start()
    {
        obstacleList = new List<string>();
        if (DataManager.Instance == null)
        {
            GameObject dataManager = new GameObject("DataManager");
            dataManager.AddComponent<DataManager>();
        }
        //LevelDatam sahne indexiyle aynı olduğundan emin olalım
        if (DataManager.Instance.GetLevelData() != SceneManager.GetActiveScene().buildIndex)
        {
            DataManager.Instance.SetLevelData(SceneManager.GetActiveScene().buildIndex);
        }
        gridManager = GameObject.Find("GridManager").GetComponent<GridManager>();
        InitializeObstacleCounters();
        FillObstacleList();
        PlaceObstacles();

    }

    void Update()
    {
        moveCountText.text = gridManager.move_count.ToString();
        EndLevel();
    }

    //HER OBSTACLE'I SAYAN FONKSİYON
    void InitializeObstacleCounters()
    {
        obstacleCounters = new Dictionary<string, IObstacleCounter>
        {
            { "box", ObstacleFactory.GetCounter("box") },
            { "stone", ObstacleFactory.GetCounter("stone") },
            { "vase_01", ObstacleFactory.GetCounter("vase_01") }
        };
    }

    //SAHNEMDE HANGİ OBSTACLELAR VARSA SADECE ONLARIN İSİMLERİNİ OBSTACLELİST'E EKLEDİK
    void FillObstacleList()
    {
        foreach (var counter in obstacleCounters)
        {
            int count = counter.Value.Count(gridManager);
            if (count > 0)
            {
                obstacleList.Add(counter.Key);
            }
        }
    }

    //OBSTACLE SAYISINII İSMİNE GÖRE ÇEKEN FONKSİYON (GOALSCRİPTTE KULLANILDI)
    public int ObstacleCounter(string name)
    {
        if (obstacleCounters.TryGetValue(name, out var counter))
        {
            return counter.Count(gridManager);
        }
        return 0;
    }

    //SAHNEMDE HANGİ OBSTACLE VARSA ONU GOALSCRİPT'E İLETİR
    void PlaceObstacles()
    {

        foreach (string str in obstacleList)
        {
            foreach (GameObject emptygoal in emptyGoals)
            {
                if (emptygoal.GetComponent<GoalScript>().filled != true)
                {
                    emptygoal.GetComponent<GoalScript>().Initialize(str, this);
                    emptygoal.GetComponent<GoalScript>().filled = true;  //Eğer goalscriptim doluysa diğerine geç, böylece obstacle'lar sırasıyla emptyGoal objelerime yerleştirilir
                    break;
                }
            }
        }
    }

    //KAZANMA VEYA KAYBETMEYİ DENETLEYİP ONA GÖRE POP-UP AKTİF EDEN FONKSİYON
    void EndLevel()
    {
        int allObjects = obstacleList.Sum(obstacle => ObstacleCounter(obstacle));

        if (gridManager.move_count >= 0 && allObjects == 0)
        {
            if (!WinCanvas.activeSelf)
            {
                LostCanvas.SetActive(false);
                DataManager.Instance.IncreaseLevel();
                WinCanvas.SetActive(true);
            }
        }
        else if (gridManager.move_count == 0 && allObjects > 0)
        {
            if (!LostCanvas.activeSelf)
            {
                LostCanvas.SetActive(true);
            }
        }
    }
}
