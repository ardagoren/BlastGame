using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TNT : MonoBehaviour , IObject , Imoveable
{
    [SerializeField]
    private GameObject[] TNTAnimationParticles;

    private GridManager gridManager;
    public int x, y;

    private bool cantMove;
    public bool CantMove
    {
        get { return cantMove; }
        set { cantMove = value; }
    }

    public void Initialize(GridManager gridManager, int x, int y)
    {
        this.gridManager = gridManager;
        this.x = x;
        this.y = y;
    }

    public void SetPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    // Start is called before the first frame update
    void Start()
    {
         cantMove=false;
    }
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (gridManager.isTweening)
        {
            // Tween animasyonu aktifken tıklama işlemini engelle
            return;
        }
        gridManager.move_count -= 1; // Her TNT basışında move count azalt
        int neighbourTNTcount = CountNeighbourTNTs(x, y);
        if (neighbourTNTcount >=1)
        {
            İnstantiateAnimationObjects();
            Destroy7x7Area(x, y);
        }
        else
        {
            İnstantiateAnimationObjects();
            Destroy5x5Area(x, y);
        }
    }

    //5x5 LİK ALANI YOK EDEN FONKSİYON
    private void Destroy5x5Area(int centerX, int centerY)
    {
        for (int x = centerX - 2; x <= centerX + 2; x++)
        {
            for (int y = centerY - 2; y <= centerY + 2; y++)
            {
                if (x < 0 || x >= gridManager.levelData.grid_width || y < 0 || y >= gridManager.levelData.grid_height || gridManager.grid[x, y] == null)
                    continue;


                if(gridManager.grid[x, y].name == "vase_01(Clone)" && gridManager.grid[x, y].GetComponent<Vase>().Health == 2)
                {
                    gridManager.grid[x, y].GetComponent<Vase>().Health -= 1;
                }
                else if (gridManager.grid[x, y] != null)
                {
                    GameObject obj = gridManager.grid[x, y];
                    gridManager.grid[x, y] = null;
                    if(obj.GetComponent<AnimateParticle>()!=null)
                       obj.GetComponent<AnimateParticle>().InstantiateParticle();
                    Destroy(obj);
                }
            }
        }
    }

    //KOMBO İLE 7X7 LİK ALANI YOK EDEN FONKSİYON
    private void Destroy7x7Area(int centerX, int centerY)
    {
        for (int x = centerX - 3; x <= centerX + 3; x++)
        {
            for (int y = centerY - 3; y <= centerY + 3; y++)
            {
                if (x < 0 || x >= gridManager.levelData.grid_width || y < 0 || y >= gridManager.levelData.grid_height || gridManager.grid[x, y] == null)
                    continue;

                if (gridManager.grid[x, y].name == "vase_01(Clone)" && gridManager.grid[x, y].GetComponent<Vase>().Health == 2)
                {
                    gridManager.grid[x, y].GetComponent<Vase>().Health -= 1;
                }
                else if (gridManager.grid[x, y] != null)
                {
                    GameObject obj = gridManager.grid[x, y];
                    gridManager.grid[x, y] = null;
                    if (obj.GetComponent<AnimateParticle>() != null)
                        obj.GetComponent<AnimateParticle>().InstantiateParticle();
                    Destroy(obj);
                }
            }
        }
    }

    //KOMŞU TNT'LERİ SAYAN FONKSİYON
    private int CountNeighbourTNTs(int x, int y)
    {
        int count = 0;
        if (IsNeighbourMatching(x + 1, y)) count++;
        if (IsNeighbourMatching(x - 1, y)) count++;
        if (IsNeighbourMatching(x, y + 1)) count++;
        if (IsNeighbourMatching(x, y - 1)) count++;
        return count;
    }

    //KOMŞU VAR İSE TRUE DÖNDÜREN FONSKİYON
    private bool IsNeighbourMatching(int x, int y)
    {
        if (x < 0 || x >= gridManager.levelData.grid_width || y < 0 || y >= gridManager.levelData.grid_height || gridManager.grid[x, y] == null)
            return false;

        return gridManager.grid[x, y].name == "TNT(Clone)";

    }

    //TNT PATLAMA ANİMASYONU
    private void İnstantiateAnimationObjects()
    {
        foreach(GameObject obj in TNTAnimationParticles)
        {
            Instantiate(obj, transform.position, Quaternion.identity);
        }
    }
}
