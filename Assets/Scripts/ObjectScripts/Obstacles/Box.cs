using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour , IObject
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

    // Start is called before the first frame update
    void Start()
    {
        cantMove = true;
    }
}
