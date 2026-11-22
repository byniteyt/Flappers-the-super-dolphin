using UnityEngine;

public enum Direction 
{
    Non, Left, Right, Up, Down
}

[System.Serializable]
public struct NeighborLevels
{
    public Direction dir; 
    public LevelRep level;
}

public class LevelRep : MonoBehaviour
{
    public NeighborLevels[] levels;
    public string scene;
}