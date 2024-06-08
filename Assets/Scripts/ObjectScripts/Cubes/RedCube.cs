using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RedCube : Cube
{

    [SerializeField]
    private Sprite RedSprite;
    [SerializeField]
    private Sprite TNTSprite;

    [SerializeField]
    private GameObject TNTObject;
    void Update()
    {
        CheckClusterTNTState();
    }

    private void OnMouseDown()
    {
        //CANVAS VARSA TIKLANAMASIN
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (gridManager.isTweening)
        {
            // Tween animasyonu aktifken tıklama işlemini engelle
            return;
        }
        DestroyCluster();

        //EĞER TNT STATE'DEYKEN CUBE'E BASILIRSA TNT OLUŞTUR
        if (GetComponent<SpriteRenderer>().sprite == TNTSprite)
        {
            GameObject obj = Instantiate(TNTObject, new Vector3(x * gridManager.spacing, y * gridManager.spacing, 0) + gridManager.transform.position, Quaternion.identity);
            obj.GetComponent<TNT>().CantMove = true;
            gridManager.grid[x, y] = obj;
            obj.GetComponent<IObject>().Initialize(gridManager, x, y);
        }

    }
    //CLUSTER TNT STATE OLUP OLMADIĞINI KONTROL EDER
    void CheckClusterTNTState()
    {
        List<GameObject> cluster = new List<GameObject>();
        FindCluster(x, y, this.name, cluster);

        if (cluster.Count >= 5)
        {
            foreach (GameObject obj in cluster)
            {
                obj.GetComponent<SpriteRenderer>().sprite = TNTSprite;
            }
        }
        else
        {
            foreach (GameObject obj in cluster)
            {
                obj.GetComponent<SpriteRenderer>().sprite = RedSprite;
            }
        }
    }

}
