using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyDetection : MonoBehaviour
{
    public InputAction ToggleLight;

    public bool DarkVision = false;
    public bool InsideLight;

    public GameObject Gorp;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// Runs every 1.5 to 4.5 seconds, instantiates BallPrefab.
    /// </summary>
    public IEnumerator SearchForPlayer()
    {
        for (; ; )
        {
            
        }
    }
}
