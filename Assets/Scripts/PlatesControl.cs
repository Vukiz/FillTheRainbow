using UnityEngine;
using System.Collections;

public class PlatesControl : MonoBehaviour {
    

    public GameObject PlateObj;
	// Use this for initialization
	void Start () {
        GameObject.Instantiate(PlateObj);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
   
}
