using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/***************************************************************
    Script is meant to display information for user.

***************************************************************/
public class GUIManagerScript : MonoBehaviour{
    public Canvas canvas;
    private UnitManagerScript unitMan;

    void Start()
    {
        unitMan = GetComponent<UnitManagerScript>();
    }

	void OnGUI()
    {
    }

    public void refreshUnitData()
    {
        string str = "";
        if (unitMan.selectedUnit.allied)
        {
            str = "Allied unit:\nMove points left: " + unitMan.selectedUnit.mobility.ToString();
        }
        else
            str = "Enemy unit!";
        canvas.GetComponentInChildren<Text>().text = str;
    }
}
