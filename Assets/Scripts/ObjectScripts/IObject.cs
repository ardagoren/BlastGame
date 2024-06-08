using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObject
{
   public void Initialize(GridManager gridManager,int x,int y);

   bool CantMove { get; set; }

}
