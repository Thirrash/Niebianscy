using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************************
    Script controlling state of all units in game.
    It is responsible for movement checks, damage exchange,
    effect application to units, regeneration...
    This one should be called most often by Input Manager,
    and this sends a ton of information to GUI.

***************************************************************/
public class UnitManagerScript : MonoBehaviour {
    List<UnitScript> unitList;
    HexfieldManagerScript hexfieldMan;
    GUIManagerScript GUIMan;
    public UnitScript selectedUnit;
    HexScript selectedHex;

	void Awake ()
    {
        hexfieldMan = GetComponent<HexfieldManagerScript>();
        GUIMan = GetComponent<GUIManagerScript>();
        GameObject[] tempUnitList = GameObject.FindGameObjectsWithTag("Unit");
        unitList = new List<UnitScript>();
        foreach (GameObject go in tempUnitList)
            unitList.Add(go.GetComponent<UnitScript>());
    }

    void Start()
    {
        for(int i = 0; i<unitList.Count; i++)
            unitList[i].GetComponent<UnitScript>().Place(hexfieldMan.GetRandomEmptyHex(), i%2==1);
    }
	
    public void Deselect()
    {
        selectedUnit = null;
        selectedHex = null;
        hexfieldMan.DeselectAll();
    }

    public void EndTurn()
    {
        foreach (UnitScript unit in unitList)
            unit.EndTurn();
    }

    public void SelectUnit(UnitScript unit)
    {
        if (unit.allied)
        {
            selectedUnit = unit;
            hexfieldMan.SelectHexRange(selectedUnit.hex, selectedUnit.mobility);
        }
    }

    public void SelectHex(HexScript hex)
    {
        selectedHex = hex;
        // maybe draw pretty path to this tile?
    }

    public string GetGUIData()
    {
        if (selectedUnit == null)
            return "";
        if (selectedUnit.allied)
            return "Allied unit!";
        else
            return "Enemy unit!";
    }

    public void Move()
    {
        if (selectedUnit != null && selectedHex != null)
        {
            if (selectedHex.Distance(selectedUnit.hex) <= selectedUnit.mobility)
            {
                selectedUnit.Move(selectedHex);
                selectedUnit = null;
                selectedHex = null;
                hexfieldMan.DeselectAll();
            }
            else
                selectedHex = null;
        }
    }
}
