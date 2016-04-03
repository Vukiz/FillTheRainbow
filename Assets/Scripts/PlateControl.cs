using UnityEngine;
using System.Collections;

public class PlateControl : MonoBehaviour {
    GameController currController;
    // Use this for initialization
    void Start () {
        currController = GameObject.Find("Controller").GetComponent<GameController>();
    }
	
	// Update is called once per frame
	void Update () {
	}
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currController.changeLastColor = gameObject.GetComponent<Renderer>().material.color;
        }
    }
}
