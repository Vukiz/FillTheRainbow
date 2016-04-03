using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuadGenerator : MonoBehaviour {
    public GameObject SpreadObj;
    LinkedList<GameObject> ObjectContainer;
    Vector3 lastRotation;
    void Awake()
    {
        ObjectContainer = new LinkedList<GameObject>();// placed in awake cause it cannot catch up with Controller Start() function( was null when it asked for it)
    }
	// Use this for initialization
	void Start () {
        ObjectContainer.AddLast(GameObject.Instantiate(SpreadObj));
        lastRotation = new Vector3(0, 0, 45);
    }

	// Update is called once per frame
	void Update () {
	    
	}
   
    public void generate(int zStart)
    {
        ObjectContainer.AddLast(GameObject.Instantiate(SpreadObj));
        ObjectContainer.Last.Value.GetComponent<Transform>().position = new Vector3(0, 0, zStart);
        ObjectContainer.Last.Value.GetComponent<Transform>().rotation = Quaternion.Euler(lastRotation);
        lastRotation += new Vector3(0, 0, 45);
    }
    public LinkedList<GameObject> getContainer
    {
        get
        {
            return ObjectContainer;
        }
    }

}
