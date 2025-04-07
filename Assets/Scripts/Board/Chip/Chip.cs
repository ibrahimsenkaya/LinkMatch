using UnityEngine;

public class Chip : MonoBehaviour
{
    public ChipData chipData;
    public SpriteRenderer spriteRenderer;
    
    public void Initialize(ChipData chipData)
    {
        this.chipData = chipData;
        spriteRenderer.sprite = chipData.sprite;
    }
    
}
