using System.Collections;
using System.Collections.Generic;
using Coords;
using UnityEngine;

public class CowController : MonoBehaviour {
    public PolarCoord PolarCoord;
    private Animator _napovedaAnimator;

    void Start () {
        // Radius of the planet
        PolarCoord.R = 0.64f;
        _napovedaAnimator = transform.Find("NapovedaNeedPapik").GetComponent<Animator>();
    }
	
	void Update () {
	    PolarCoord.Phi += 0.007f;

        transform.localPosition = PolarCoord.ToCartesian().ToVector3();
	    // nataceni spritu
	    transform.rotation = Quaternion.EulerRotation(0, 0, PolarCoord.Phi - Mathf.PI / 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "postava" && _napovedaAnimator) {
            _napovedaAnimator.SetBool("NeedPapik", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "postava" && _napovedaAnimator)
        {
            _napovedaAnimator.SetBool("NeedPapik", false);
        }
    }
}
