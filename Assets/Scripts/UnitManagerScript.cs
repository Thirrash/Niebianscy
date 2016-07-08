using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

/***************************************************************
    Script controlling state of all units in game.
    It is responsible for movement checks, damage exchange,
    effect application to units, regeneration...
    This one should be called most often by Input Manager,
    and this sends a ton of information to GUI.

***************************************************************/
public class UnitManagerScript : MonoBehaviour {
    public enum Mode
    {
        None, Movement, Attack, Magick
    };
    public Mode mode;
    List<UnitScript> unitList;
    HexfieldManagerScript hexfieldMan;
    GUIManagerScript GUIMan;
    public UnitScript selectedUnit;
    HexScript selectedHex;

	void Awake ()
    {
        mode = Mode.None;
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
        if (unit != selectedUnit)
            StartNone();
        unit.hex.highlight(Color.yellow);
        selectedUnit = unit;
        GUIMan.refreshUnitData();
    }

    public void SelectHex(HexScript hex)
    {
        selectedHex = hex;
        // maybe draw pretty path to this tile?
    }

    public void Move()
    {
        if (mode != Mode.Movement)
            return;
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

    void GetMoveRange()
    {
        List<HexScript> hexesList = new List<HexScript>();
        List<int> costsList = new List<int>();
        HashSet<HexScript> hexesVisited = new HashSet<HexScript>();
        hexesList.Add(selectedUnit.hex);
        costsList.Add(0);
        for (int i = 0; i < hexesList.Count; i++)
        {
            HexScript hexCurrent = hexesList[i];
            int costCurrent = costsList[i];

            if (!hexesVisited.Add(hexCurrent))
                break;
            if (costCurrent >= selectedUnit.mobility)
                continue;

            List<HexScript> adjacentHexes = hexfieldMan.GetHexNeighbours(hexCurrent);
            for (int j = adjacentHexes.Count - 1; j >= 0; --j)
                if (hexesVisited.Contains(adjacentHexes[j]))
                    adjacentHexes.RemoveAt(j);

            foreach (HexScript ah in adjacentHexes)
            {
                int costNew = costCurrent + selectedUnit.getMoveCost(hexCurrent, ah);
                if (hexesList.Contains(ah))
                {
                    int costOld = costsList[hexesList.IndexOf(hexCurrent, i)];
                    if (costNew < costOld)
                        costsList[hexesList.IndexOf(hexCurrent, i)] = costNew;
                }
                else
                {
                    hexesList.Add(ah);
                    costsList.Add(costNew);
                }
            }

            foreach (HexScript hex in hexesList)
                hex.highlight(Color.red);
        }
    }

    public void StartNone()
    {
        mode = Mode.None;
        hexfieldMan.DeselectAll();
    }

    public void StartMovement()
    {
        if (!selectedUnit.allied)
            return;
        Debug.Log("Starting movement");
        mode = Mode.Movement;
        GetMoveRange();
    }
}
