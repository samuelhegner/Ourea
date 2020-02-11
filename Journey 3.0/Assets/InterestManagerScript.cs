﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterestManagerScript : MonoBehaviour
{
    private Queue<Transform> currentObjectsOfInterst;

    public int maximumInterestCount;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNewPointsOfInterest(Scene scene)
    {
        GameObject[] sceneOjbects = scene.GetRootGameObjects();
        

        for (int i = 0; i < sceneOjbects.Length; i++)
        {
            ObjectToLookAt rootCheck = sceneOjbects[i].GetComponent<ObjectToLookAt>();

            if (rootCheck != null)
            {
                currentObjectsOfInterst.Enqueue(rootCheck.transform);
            }

            ObjectToLookAt[] tempObjs = sceneOjbects[i].GetComponentsInChildren<ObjectToLookAt>();

            if (tempObjs.Length > 0)
            {
                for (int j = 0; j < tempObjs.Length; j++)
                {
                    currentObjectsOfInterst.Enqueue(tempObjs[j].transform);
                }
            }
        }

        while (currentObjectsOfInterst.Count > maximumInterestCount)
        {
            currentObjectsOfInterst.Dequeue();
        }

        API.GlobalReferences.PlayerRef.GetComponent<InterestFinder>().TransformsOfInterst.Clear();

        for (int i = 0; i < currentObjectsOfInterst.Count; i++)
        {
            API.GlobalReferences.PlayerRef.GetComponent<InterestFinder>().TransformsOfInterst.Add(currentObjectsOfInterst[i]);
        }
    }
}
