using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesDisplay : MonoBehaviour
{
    public GorpController GorpHealth;
    public float GorpHealthCounter;
    public TMP_Text GorpLives;

    public GlobbingtonAttackController GlobHealth;
    public float GlobHealthCounter;
    public TMP_Text GlobLives;

    void Start()
    {

    }

    void Update()
    {

        GorpHealth = GameObject.Find("Gorp").GetComponent<GorpController>();
        GorpHealthCounter = GorpHealth.Health;
        GorpLives.text = GorpHealthCounter.ToString();

        GlobHealth = GameObject.Find("Globbington").GetComponent<GlobbingtonAttackController>();
        GlobHealthCounter = GlobHealth.Health;
        GlobLives.text = GlobHealthCounter.ToString();

    }

}
