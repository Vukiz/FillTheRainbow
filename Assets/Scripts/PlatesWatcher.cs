using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatesWatcher : MonoBehaviour {


    private int PlateZPos = -50;
    public GameObject PlateObj;
    private List<GameObject> PlateContainer;
	// Use this for initialization
	void Start () {
        PlateContainer = new List<GameObject>();
        PlateContainer.Add(GameObject.Instantiate(PlateObj));
        PlateContainer.Add(GameObject.Instantiate(PlateObj));
        PlateContainer.Add(GameObject.Instantiate(PlateObj));
        PlateContainer.Add(GameObject.Instantiate(PlateObj));

        PlateContainer[3].GetComponent<Renderer>().material.color = Color.blue;
        PlateContainer[2].GetComponent<Renderer>().material.color = Color.green;
        PlateContainer[1].GetComponent<Renderer>().material.color = Color.yellow;
        PlateContainer[0].GetComponent<Renderer>().material.color = Color.red;

        PlateContainer[0].transform.position = new Vector3(-0.25F, 0.25F, PlateZPos);
        PlateContainer[1].transform.position = new Vector3(0.25F, 0.25F, PlateZPos);
        PlateContainer[2].transform.position = new Vector3(0.25F, -0.25F, PlateZPos);
        PlateContainer[3].transform.position = new Vector3(-0.25F, -0.25F, PlateZPos);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
   
}
