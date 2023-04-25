using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesDisplay : MonoBehaviour
{
    public GorpController GorpHealth;
    public float GorpHealthCounter;
    //public int GlobHealthCounter;

    void Start()
    {

        GorpHealth = GameObject.Find("Gorp").GetComponent<GorpController>();
        GorpHealthCounter = GorpHealth.Health;

    }

}
