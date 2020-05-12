﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckPlate : MonoBehaviour
{
    [SerializeField] private bool _boulderPresent;

    private bool opened = false;

    [SerializeField] private bool player;

    [SerializeField] private UnityEvent openDoor;

    [SerializeField] private Animator _gateAnim;
    [SerializeField] private Animator _platformAnim;

    public Transform boulderParent;

    public ReturnBoulderPresent _ReturnBoulderPresent;
    
    private void OnTriggerEnter(Collider other)
    {
        PushObject _pushable = other.GetComponent<PushObject>();

        PlayerMovement _movement = other.GetComponent<PlayerMovement>();

        
        if (_pushable != null)
        {
           
        }
        
        if (_movement != null)
        {
            player = true;
        }
    }

    private void Update()
    {
        if(_gateAnim.isActiveAndEnabled)
            _gateAnim.SetBool("playerWeight", player);
        if (_platformAnim.isActiveAndEnabled)
        {
            _platformAnim.SetBool("playerWeight", player);
        }


        if (_ReturnBoulderPresent.Boulder)
        {
            if (!opened)
            {
                _ReturnBoulderPresent.pushableObj.SetParent(boulderParent, true);
                _ReturnBoulderPresent.pushableObj.GetComponent<Rigidbody>().velocity = Vector3.zero;

                Material mat = _ReturnBoulderPresent.pushableObj.GetComponentInChildren<Renderer>().material;

                InteractibleGlow _glow = API.GlobalReferences.PlayerRef.GetComponent<InteractibleGlow>();

                _glow.materials.Remove(mat);
                
                _glow.lerpObjects.Add(_ReturnBoulderPresent.pushableObj.gameObject);
                    
                
                Destroy(_ReturnBoulderPresent.pushableObj.GetComponent<GravityCheck>());
                Destroy(_ReturnBoulderPresent.pushableObj.GetComponent<Rigidbody>());
                Destroy(_ReturnBoulderPresent.pushableObj.GetComponent<PushObject>());
                
                openDoor.Invoke();
                API.GlobalReferences.PlayerRef.GetComponent<InteractWithObject>().StopInteracting();
                opened = true;
            } 
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        PushObject _pushable = other.GetComponent<PushObject>();

        PlayerMovement _movement = other.GetComponent<PlayerMovement>();
        

        if (_pushable != null)
        {
            _boulderPresent = false;
        }

        if (_movement != null)
        {
            player = false;
        }
    }
}
