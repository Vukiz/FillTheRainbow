using UnityEngine;
using System.Collections;

public class PlateClick : MonoBehaviour {
    int counter = 0;
    GameController currController;
    private int PlateZPos = -50;
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
            counter = Random.Range(0, 4);
            switch (counter)
            {
                case 3:
                    gameObject.transform.position = new Vector3(-0.25F, 0.25F, PlateZPos);
                    break;
                case 0:
                    gameObject.transform.position = new Vector3(0.25F, 0.25F, PlateZPos);
                    break;
                case 1:
                    gameObject.transform.position = new Vector3(0.25F, -0.25F, PlateZPos);
                    break;
                case 2:
                    gameObject.transform.position = new Vector3(-0.25F, -0.25F, PlateZPos);
                    break;
            }
            currController.Spread();
        }
    }
}
