﻿using UnityEngine;
using System.Collections;


/***************************************************************
    For the logic behind hexagon management, please refer to:
    http://www.redblobgames.com/grids/hexagons/
    Generated field is odd-q type and is stored in 2d array.
    Indices of array are using offset coords.
    All the logics uses 3d-cube coords, but dependent coord
    is z, not y (only swapped letters).

***************************************************************/
public class HexfieldManagerScript : MonoBehaviour {
    public const uint vMax = 12;
    public const uint wMax = 12;
    public GameObject prefab;
    private GameObject[,] hexfield;

    void Start ()
    {
        hexfield = new GameObject[vMax, wMax];
        for (int v = 0; v < vMax; v++)
        {
            for (int w = 0; w < wMax; w++)
            {
                hexfield[v, w] = (GameObject)Instantiate(prefab, GetHexPosition(v, w), Quaternion.Euler(new Vector3(-90, 0, 0)));
                hexfield[v, w].GetComponent<HexScript>().Set(v, w, (TerrainType)Random.Range(0, 3));
            }
        }
        DeselectAll();
	}

    public void SelectHex(int x, int y, int z)
    {
        int v = x;
        int w = y + (x - (x%2)) / 2;
        if (v >= vMax || w >= wMax)
            return;
        if (v < 0 || w < 0)
            return;
        hexfield[v, w].GetComponent<HexScript>().highlight(Color.cyan);
    }

    public void SelectHexRange(int x, int y, int z, int r)
    {
        DeselectAll();
        for (int dx = -r; dx <= r; dx++)
        {
            for(int dy = Mathf.Max(-r, -dx-r); dy <= Mathf.Min(r, -dx+r); dy++)
            {
                int dz = -dx-dy;
                SelectHex(x+dx, y+dy, z+dz);
            }
        }
    }

    // null exception here will be fixed when propper map initialization will be introduced
    public void DeselectAll()
    {
        foreach (GameObject go in hexfield)
            go.GetComponent<HexScript>().Refresh();
    }

    private Vector3 GetHexPosition(int v, int w)
    {
        return new Vector3(v * 1.5f, 0, v % 2 * 0.865f + w * 1.73f);
    }

    // what to return if hex does not exist?
    public Vector3 GetHexPosition(int x, int y, int z)
    {
        return GetHexPosition(x, y + (x - (x % 2)) / 2);
    }
}