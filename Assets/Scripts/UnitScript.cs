using UnityEngine;
using System.Collections;

public class UnitScript : MonoBehaviour {
    public HexScript hex;
    public bool allied;
    public int mobility; // remaining moves left, should refresh in each turn

    public void Place(HexScript ihex, bool iallied)
    {
        if(hex != null)
            hex.available = true;
        hex = ihex;
        hex.available = false;
        allied = iallied;
        transform.position = hex.GetPosition();
        if (allied)
            transform.FindChild("Cylinder").GetComponent<MeshRenderer>().material.color = Color.green;
        else
            transform.FindChild("Cylinder").GetComponent<MeshRenderer>().material.color = Color.red;
        // just to refresh all the resources
        EndTurn();
    }

    public void EndTurn()
    {
        //should call unit statistics script to get actual number
        mobility = 2;
        return;
    }

    public void Move(HexScript ihex)
    {
        mobility -= hex.Distance(ihex);
        hex.available = true;
        ihex.available = false;
        hex = ihex;
        transform.position = hex.GetPosition();
    }

    public int getMoveCost(HexScript from, HexScript to)
    {
        return 1;
    }
}
