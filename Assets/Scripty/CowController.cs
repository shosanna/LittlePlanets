using System.Collections;
using System.Collections.Generic;
using Coords;
using UnityEngine;

public class CowController : MonoBehaviour {
    public PolarCoord _polarCoord;

    void Start () {
        // Radius of the planet
        _polarCoord.R = 0.64f;
    }
	
	void Update () {
	    _polarCoord.Phi += 0.007f;

        transform.localPosition = _polarCoord.ToCartesian().ToVector3();
	    // nataceni spritu
	    transform.rotation = Quaternion.EulerRotation(0, 0, _polarCoord.Phi - Mathf.PI / 2);
    }
}
