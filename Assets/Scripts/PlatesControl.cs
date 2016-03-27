using UnityEngine;
using System.Collections;

public class PlatesControl : MonoBehaviour {
    int counter = 0;
    private int PlateZPos = -50;

    public GameObject PlateObj;
    GameController currController;
    GameObject curPlate;
	// Use this for initialization
	void Start () {
        currController = GameObject.Find("Controller").GetComponent<GameController>();
        curPlate = GameObject.Instantiate(PlateObj);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            counter = Random.Range(0, 4);
            switch (counter)
            {
                case 3:
                    curPlate.transform.position = new Vector3(-0.25F, 0.25F, PlateZPos);
                    break;
                case 0:                    
                    curPlate.transform.position = new Vector3(0.25F, 0.25F, PlateZPos);
                    break;
                case 1:
                    curPlate.transform.position = new Vector3(0.25F, -0.25F, PlateZPos);
                    break;
                case 2:
                    curPlate.transform.position = new Vector3(-0.25F, -0.25F, PlateZPos);
                    break;
            }
            currController.Spread();
        }
    }
}
