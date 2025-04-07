using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
  public List<Tile> neighbors;
  public int coloumnIndex;
  public Chip chip;
  SpriteRenderer spriteRenderer;

  private void Awake()
  {
    spriteRenderer ??= GetComponent<SpriteRenderer>();
  }

  public void AddNeighbor(Tile tile)
  {
    if (!neighbors.Contains(tile))
         neighbors.Add(tile);
  }
  
  public void Highlight()
  {
    spriteRenderer.color = Color.red;
  }
  public void DownLight()
  {
    spriteRenderer.color = Color.white;
  }

  private void OnDrawGizmosSelected()
  {
      Gizmos.color = Color.black;
        foreach (var neighbor in neighbors)
        {
            Gizmos.DrawSphere(neighbor.transform.position,1f);
        }
      
  }

  public void Collect()
  {
    Destroy(chip.gameObject);
    chip = null;
  }
}
