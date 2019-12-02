﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;


public class RightArmIK : MonoBehaviour
{

    [SerializeField] private bool _inUse = false;

    [SerializeField] private TwoBoneIKConstraint _ikConstraint;
    
    [SerializeField] private Transform target;

    [SerializeField] private Transform hint;

    public static RightArmIK Instance;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (_inUse)
        {
            _ikConstraint.weight = Mathf.Lerp(_ikConstraint.weight, 0.9f, Time.deltaTime);
        }
        else
        {
            _ikConstraint.weight = Mathf.Lerp(_ikConstraint.weight, 0, Time.deltaTime * 10f);
        }
    }


    public void SetIkTargetAndHint(IKSettings _settings)
    {
        target.localPosition = _settings.targetPos;
        target.localRotation = _settings.targetRot;
        hint.localPosition = _settings.hintPos;
        _inUse = true;
    }

    public void StopIK()
    {
        _inUse = false;
    }
}

