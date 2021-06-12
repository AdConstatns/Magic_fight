using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SensorToolkit.Example;
using System;

public class PlayerHolder : MonoBehaviour {
    public bool IsHeld;
    FixedJoint fixedjoint;

    // Start is called before the first frame update
    void Start() {
        fixedjoint = GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    void Update() {
        if (fixedjoint.connectedBody != null)
            IsHeld = fixedjoint.connectedBody.gameObject.GetComponent<Holdable>().IsHeld;

    }
}



