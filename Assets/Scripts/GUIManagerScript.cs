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
        canvas.GetComponentInChildren<Text>().text = unitMan.GetGUIData();
    }
}
