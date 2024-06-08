using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour , IObject
{
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
    void Start()
    {
        cantMove = true;
    }
}
