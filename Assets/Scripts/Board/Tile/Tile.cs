using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
  public List<Tile> neighbors;
  public Chip chip;
  
  public void AddNeighbor(Tile tile)
  {
    if (!neighbors.Contains(tile))
         neighbors.Add(tile);
  }

  private void OnDrawGizmosSelected()
  {
      Gizmos.color = Color.black;
        foreach (var neighbor in neighbors)
        {
            Gizmos.DrawSphere(neighbor.transform.position,1f);
        }
      
  }
}
