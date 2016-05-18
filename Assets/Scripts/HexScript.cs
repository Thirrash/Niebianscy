using UnityEngine;

public enum TerrainType
{
    Plains, Forest, Water
}

public class HexScript : MonoBehaviour
{
    public int x, y, z;
    public TerrainType terrain;
    public bool occupied;

    public void highlight(Color c)
    {
        GetComponent<MeshRenderer>().material.color = c;
    }

    public void Refresh()
    {
        Color c = Color.white;
        switch(terrain)
        {
            case TerrainType.Plains:
                c = Color.yellow;
                break;
            case TerrainType.Forest:
                c = Color.green;
                break;
            case TerrainType.Water:
                c = Color.blue;
                break;
        }
        GetComponent<MeshRenderer>().material.color = c;
    }

    public void Set(int col, int row, TerrainType ter)
    {
        x = col;
        y = row - (col - (col%2)) / 2;
        z = -x - y;
        terrain = ter;
        occupied = false;
    }
}
