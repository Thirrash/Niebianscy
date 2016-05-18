using UnityEngine;
using System.Collections;

public class UnitManagerScript : MonoBehaviour {
    HexfieldManagerScript hexfieldMan;
    UnitScript selectedUnit;
    HexScript selectedHex;
	// Use this for initialization
	void Start () {
        hexfieldMan = GetComponent<HexfieldManagerScript>();
	}
	
    public void SelectUnit(UnitScript unit)
    {
        if(unit.allied)
        {
            selectedUnit = unit;
            unit.Select();
            Debug.Log("Unit selected!");
        }
        else
            Debug.Log("Enemy unit!");
    }

    public void SelectHex(HexScript hex)
    {
        selectedHex = hex;
        Debug.Log("Hex selected!");
    }

    bool canMove()
    {
        int d = (Mathf.Abs(selectedUnit.x - selectedHex.x) + Mathf.Abs(selectedUnit.y - selectedHex.y)
            + Mathf.Abs(selectedUnit.z - selectedHex.z)) / 2;
        return d <= selectedUnit.GetMoveRange();
    }

    public void Move()
    {
        if (selectedUnit != null && selectedHex != null)
        {
            if (canMove())
            {
                selectedUnit.Move(selectedHex.x, selectedHex.y, selectedHex.z);
                selectedUnit = null;
                selectedHex = null;
            }
            else
                selectedHex = null;
        }
    }
}
