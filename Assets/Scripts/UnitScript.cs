using UnityEngine;
using System.Collections;

public class UnitScript : MonoBehaviour {
    // TO DO better collider (hexagonal) for units needed
    // is reference to hexfield manager really needed?
    HexfieldManagerScript Hexfield;
    public int x, y, z;
    public bool allied;

    // Use this for initialization
    void Start () {
        Hexfield = GameObject.Find("ManagerScripts").GetComponent<HexfieldManagerScript>();
        allied = true;
        Move(4, 4, -8);
	}

    public void Select()
    {
        Hexfield.SelectHexRange(x, y, z, GetMoveRange());
    }

    public int GetMoveRange()
    {
        //should call unit statistics script to get actual number
        return 2;
    }

    public void Move(int ix, int iy, int iz)
    {
        x = ix;
        y = iy;
        z = iz;
        transform.position = Hexfield.GetHexPosition(x, y, z);
        Hexfield.DeselectAll();
    }
}
