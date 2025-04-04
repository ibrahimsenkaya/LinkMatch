using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public int ColumnCount;
    public int RowCount;
        
    public int targetMoves;
    public int targetScore;

}
 
