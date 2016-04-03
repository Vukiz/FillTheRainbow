using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatesWatcher : MonoBehaviour {


    private int PlateZPos = -50;
    public GameObject PlateObj;
    private List<GameObject> PlateContainer;
    private Color[] colors = new Color[4] { Color.blue, Color.green, Color.yellow, Color.red };

    
    void Start () {
        PlateContainer = new List<GameObject>();
        PlateContainer.Add(GameObject.Instantiate(PlateObj));
        PlateContainer.Add(GameObject.Instantiate(PlateObj));
        PlateContainer.Add(GameObject.Instantiate(PlateObj));
        PlateContainer.Add(GameObject.Instantiate(PlateObj));

        PlateContainer[3].GetComponent<Renderer>().material.color = colors[0];
        PlateContainer[2].GetComponent<Renderer>().material.color = colors[1];
        PlateContainer[1].GetComponent<Renderer>().material.color = colors[2];
        PlateContainer[0].GetComponent<Renderer>().material.color = colors[3];

        PlateContainer[0].transform.position = new Vector3(-0.25F, 0.25F, PlateZPos);
        PlateContainer[1].transform.position = new Vector3(0.25F, 0.25F, PlateZPos);
        PlateContainer[2].transform.position = new Vector3(0.25F, -0.25F, PlateZPos);
        PlateContainer[3].transform.position = new Vector3(-0.25F, -0.25F, PlateZPos);
    }
    public Color[] getColors
    {
        get
        {
            return colors;
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
   
}
