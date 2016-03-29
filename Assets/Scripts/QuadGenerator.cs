﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuadGenerator : MonoBehaviour {
    public GameObject SpreadObj;
    LinkedList<GameObject> ObjectContainer;
    void Awake()
    {
        ObjectContainer = new LinkedList<GameObject>();// placed in awake cause it cannot catch up with Controller Start() function( was null when it asked for it)
    }
	// Use this for initialization
	void Start () {
        ObjectContainer.AddLast(GameObject.Instantiate(SpreadObj));
    }

	// Update is called once per frame
	void Update () {
	    
	}
    public void generate(int zStart)
    {
        ObjectContainer.AddLast(GameObject.Instantiate(SpreadObj));
        ObjectContainer.Last.Value.GetComponent<Transform>().position = new Vector3(0, 0, zStart);
    }
    public LinkedList<GameObject> getContainer
    {
        get
        {
            return ObjectContainer;
        }
    }
}
