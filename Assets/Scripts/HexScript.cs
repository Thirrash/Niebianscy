using UnityEngine;

/***************************************************************
    Script containing all the data and functionality
    a single hexagon tile should have.
    It holds terrain type, location and information
    about avalability (legality to move) of a tile.
    Some of those currently implemented functions
    will probbably be scrapped after a propper
    terrain will cover hexfield.

***************************************************************/
public enum TerrainType
{
    Plains, Forest, Water
}

public class HexScript : MonoBehaviour
{
    public int x, y, z;
    public TerrainType terrain;
    public bool available;

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
        available = true;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public int Distance(HexScript hex)
    {
        return (Mathf.Abs(hex.x - x) + Mathf.Abs(hex.y - y) + Mathf.Abs(hex.z - z)) / 2;
    }
}
