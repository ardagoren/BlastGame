using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cube : MonoBehaviour, IObject, Imoveable
{
    protected GridManager gridManager;
    public int x, y;

    private bool cantMove;
    public bool CantMove {
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

    //KOMŞU 2 VEYA DAHA FAZLAYSA OBJELERİ YOK ET
    protected void DestroyCluster()
    {
        List<GameObject> cluster = new List<GameObject>();
        FindCluster(x, y, this.name, cluster);

        if (cluster.Count >= 2)
        {
            gridManager.move_count -= 1; //Her 'geçerli' basışta move count 1 azalt
            foreach (GameObject clu in cluster)
            {
                GameObject obj = clu;
                gridManager.grid[clu.GetComponent<Cube>().x, clu.GetComponent<Cube>().y] = null;
                obj.GetComponent<AnimateParticle>().InstantiateParticle();
                Destroy(obj);
            }

            DestroyAdjacentBoxes(cluster);
            DamageAdjacentVase(cluster);
        }
    }

    //KOMŞULARI SAY VE CLUSTER LİSTESİNE EKLE
    protected void FindCluster(int x, int y, string prefabName, List<GameObject> cluster)
    {
        if (x < 0 || x >= gridManager.levelData.grid_width || y < 0 || y >= gridManager.levelData.grid_height)
            return;

        GameObject obj = gridManager.grid[x, y];
        if (obj == null || obj.name != prefabName || cluster.Contains(obj))
            return;

        cluster.Add(obj);

        FindCluster(x + 1, y, prefabName, cluster);
        FindCluster(x - 1, y, prefabName, cluster);
        FindCluster(x, y + 1, prefabName, cluster);
        FindCluster(x, y - 1, prefabName, cluster);
    }

    //KOMŞU BOX LARI YOK EDEN FONKSİYON
    public void DestroyAdjacentBoxes(List<GameObject> cluster)
    {
        HashSet<GameObject> boxesToDestroy = new HashSet<GameObject>();

        foreach (GameObject tile in cluster)
        {
            Cube colorTile = tile.GetComponent<Cube>();
            CheckAndAddBox(colorTile.x + 1, colorTile.y, boxesToDestroy); // Sağ
            CheckAndAddBox(colorTile.x - 1, colorTile.y, boxesToDestroy); // Sol
            CheckAndAddBox(colorTile.x, colorTile.y + 1, boxesToDestroy); // Üst
            CheckAndAddBox(colorTile.x, colorTile.y - 1, boxesToDestroy); // Alt
        }

        foreach (GameObject box in boxesToDestroy)
        {
            gridManager.grid[box.GetComponent<Box>().x, box.GetComponent<Box>().y] = null;
            box.GetComponent<AnimateParticle>().InstantiateParticle();
            Destroy(box);
        }
    }

    //KOMŞU BOXLARI SAYAN VE LİSTEYE EKLEYEN FONKSİYON
    private void CheckAndAddBox(int checkX, int checkY, HashSet<GameObject> boxesToDestroy)
    {
        if (checkX >= 0 && checkX < gridManager.levelData.grid_width && checkY >= 0 && checkY < gridManager.levelData.grid_height)
        {
            GameObject obj = gridManager.grid[checkX, checkY];
            if (obj != null && obj.name == "box(Clone)")
            {
                boxesToDestroy.Add(obj);
            }
        }
    }

    //VAZOLARI CANINA GÖRE YOK EDEN FONKSİYON
    public void DamageAdjacentVase(List<GameObject> cluster)
    {
        HashSet<GameObject> vaseToDamage = new HashSet<GameObject>();

        foreach (GameObject tile in cluster)
        {
            Cube cube = tile.GetComponent<Cube>();
            CheckAndAddVase(cube.x + 1, cube.y, vaseToDamage); // Sağ
            CheckAndAddVase(cube.x - 1, cube.y, vaseToDamage); // Sol
            CheckAndAddVase(cube.x, cube.y + 1, vaseToDamage); // Üst
            CheckAndAddVase(cube.x, cube.y - 1, vaseToDamage); // Alt
        }

        foreach (GameObject vase in vaseToDamage)
        {

            if (vase.GetComponent<Vase>().Health == 1)
            {
                gridManager.grid[vase.GetComponent<Vase>().x, vase.GetComponent<Vase>().y] = null;
                vase.GetComponent<AnimateParticle>().InstantiateParticle();
                Destroy(vase);

              
            }
            vase.GetComponent<Vase>().Health -= 1;
        }

        //KOMŞU VAZOLARI SAYAN VE LİSTEYE EKLEYEN FONKSİYON
        void CheckAndAddVase(int checkX, int checkY, HashSet<GameObject> vaseToDamage)
        {
            if (checkX >= 0 && checkX < gridManager.levelData.grid_width && checkY >= 0 && checkY < gridManager.levelData.grid_height)
            {
                GameObject obj = gridManager.grid[checkX, checkY];
                if (obj != null && obj.name == "vase_01(Clone)")
                {
                    vaseToDamage.Add(obj);
                }
            }
        }
    }
}
