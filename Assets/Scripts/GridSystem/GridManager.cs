using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GridManager : MonoBehaviour
{
    public float spacing = 1;
    public TextAsset jsonFile; 
    public GameObject redPrefab;
    public GameObject greenPrefab;
    public GameObject bluePrefab;
    public GameObject yellowPrefab;
    public GameObject tntPrefab;
    public GameObject boxPrefab;
    public GameObject stonePrefab;
    public GameObject vasePrefab;
    public GameObject[] randomPrefabs; // Rastgele renkler için prefablar

    public LevelData levelData;
    public GameObject[,] grid;
    public int move_count;

    public bool isTweening = false;


    void Start()
    {

    }
    private void Awake()
    {
        LoadLevelData();
        grid = new GameObject[levelData.grid_width, levelData.grid_height];
        CreateGrid();
        CenterGridToCamera();
        move_count = levelData.move_count;
    }
    void Update()
    {
        DropDownAndFillEmptySpaces();

    }

    void LoadLevelData()
    {
        levelData = JsonUtility.FromJson<LevelData>(jsonFile.text);
    }

    void CreateGrid()
    {
        for (int x = 0; x < levelData.grid_width; x++)
        {
            for (int y = 0; y < levelData.grid_height; y++)
            {
                int index = y * levelData.grid_width + x;
                string gridItem = levelData.grid[index];
                GameObject prefabToInstantiate = GetPrefab(gridItem);

                if (prefabToInstantiate != null)
                {
                    GameObject obj = Instantiate(prefabToInstantiate, new Vector3(x * spacing, y * spacing, 0) + transform.position, Quaternion.identity, transform);
                    grid[x, y] = obj;
                    obj.GetComponent<IObject>().Initialize(this, x, y);
                }
            }
        }
    }

    GameObject GetPrefab(string gridItem)
    {
        switch (gridItem)
        {
            case "r": return redPrefab;
            case "g": return greenPrefab;
            case "b": return bluePrefab;
            case "y": return yellowPrefab;
            case "t": return tntPrefab;
            case "bo": return boxPrefab;
            case "s": return stonePrefab;
            case "v": return vasePrefab;
            case "rand": return randomPrefabs[Random.Range(0, randomPrefabs.Length)];
            default: return null;
        }
    }

    void CenterGridToCamera()
    {
        // Grid'in toplam genişliğini ve yüksekliğini hesapla
        float totalGridWidth = (levelData.grid_width - 1) * spacing;
        float totalGridHeight = (levelData.grid_height - 1) * spacing;

        // Grid'in ortasını hesapla
        Vector3 gridCenter = new Vector3(totalGridWidth / 2, totalGridHeight / 2, 0);

        // Kameranın pozisyonunu al
        Vector3 cameraPosition = Camera.main.transform.position;

        // GridManager'ın pozisyonunu hesapla ve kameranın ortasına hizala
        transform.position = new Vector3(cameraPosition.x - gridCenter.x, cameraPosition.y - gridCenter.y - 3, 0);
    }

    //OBJELERİ AŞAĞI DÜŞÜREN VE YUKARIYA YENİSİNİ EKLEYEN FONKSİYON
    public void DropDownAndFillEmptySpaces()
    {
        for (int x = 0; x < levelData.grid_width; x++)
        {
            for (int y = 0; y < levelData.grid_height; y++)
            {
                if (grid[x, y] == null)
                {
                    for (int k = y + 1; k < levelData.grid_height; k++)
                    {
                        if (grid[x, k] != null)
                        {
                            
                            if(!grid[x,k].GetComponent<IObject>().CantMove)
                            {        
                                    // Objeyi aşağı düşür
                                    grid[x, y] = grid[x, k];
                                    grid[x, k] = null;
                                    MoveObjectToPosition(grid[x, y], new Vector3(x * spacing, y * spacing, 0) + transform.position, 0.8f);
                                    grid[x, y].GetComponent<Imoveable>().SetPosition(x, y);
                                    break;
                            }
                        }
                    }
                }
            }

            // Boş alanları yeni objelerle doldur
            for (int y = levelData.grid_height - 1; y >= 0; y--)
            {
                if (grid[x, y] == null)
                {
                    int randomIndex = Random.Range(0, randomPrefabs.Length);
                    GameObject obj = Instantiate(randomPrefabs[randomIndex], new Vector3(x * spacing, (y * spacing) + 5, 0) + transform.position, Quaternion.identity);
                    grid[x, y] = obj;
                    obj.GetComponent<IObject>().Initialize(this, x, y);
                    MoveObjectToPosition(obj, new Vector3(x * spacing, y * spacing, 0) + transform.position, 0.8f);
                }
            }
        }
    }

    //TİLE LAR İÇİN DÜŞERKEN KULLANILAN TWWEN FONKSİYONU 
    //TWEEN ANİMASYONUNDAYKEN KULLANICININ TIKLAMASINI ENGELLER
    //TİLE LARDA ONMOUSEDOWN FONKSİYONUNDA EĞER TWEENİNG TRUE İSE KULLANICI TIKLAYAMADI
    void MoveObjectToPosition(GameObject tileObject, Vector3 targetPosition, float duration)
    {
        // Geçerli olup olmadığını kontrol et
        if (tileObject != null && tileObject.transform != null)
        {
            // Collider bileşenini al
            Collider2D collider = tileObject.GetComponent<Collider2D>();
            if (collider != null)
            {
                // Collider bileşenini devre dışı bırak
                collider.enabled = false;
            }

            // DOTween ile hareket animasyonu başlat
            isTweening = true;
            tileObject.transform.DOMove(targetPosition, duration).OnKill(() =>
            {
                // Animasyon iptal edildiğinde veya tamamlandığında çağrılır
                if (tileObject == null || tileObject.transform == null)
                {
                    return; // Objeye erişim yoksa geri dön
                }

                // Collider bileşenini etkinleştir
                if (collider != null)
                {
                    collider.enabled = true;
                }

                isTweening = false;
            }).OnComplete(() =>
            {
                if (tileObject == null || tileObject.transform == null)
                {
                    return; // Objeye erişim yoksa geri dön
                }

                // Collider bileşenini etkinleştir
                if (collider != null)
                {
                    collider.enabled = true;
                }

                isTweening = false;
            });
        }
    }
}
    


