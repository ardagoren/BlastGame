using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : MonoBehaviour , IObject , Imoveable
{
    public int Health=2;

    [SerializeField]
    private Sprite brokenVase;

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

    void Update()
    {
        if (Health == 1)
        {
            GetComponent<SpriteRenderer>().sprite = brokenVase;
        }
    }
}
