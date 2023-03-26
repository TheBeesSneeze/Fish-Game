using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyDetection : MonoBehaviour
{
    [Header("Settings")]
    public float SightDistance = 8f;
    public LayerMask LM;

    [Header("Debug")]
    public bool DarkVision = false;
    public bool InsideLight;
    private bool currentlyPursuing = false;
    public float Speed = 1f; 

    public GameObject currentTarget;

    public InputAction ToggleLight;

    private GameObject gop;
    private GameObject globbington;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        gop = GameObject.Find("Gorp");
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(SearchForPlayer());
    }

    /// <summary>
    /// Runs every 1.5 to 4.5 seconds, instantiates BallPrefab.
    /// </summary>
    public IEnumerator SearchForPlayer()
    {
        for (; ; )
        {
            // Get the origin and direction of the raycast
            Vector3 origin = gameObject.transform.position;
            Vector3 direction = gop.transform.position - origin;

            // Cast the raycast and get the hit information
            var hit = Physics2D.Raycast(origin, direction, SightDistance, LM);
            if (hit)
            {
                Debug.DrawLine(origin, gop.transform.position, Color.green, 0.5f);

                string hitName = hit.collider.gameObject.name;

                if (hitName.Equals("Gorp"))
                {
                    currentlyPursuing = true;
                    currentTarget = hit.collider.gameObject;
                    StartCoroutine(PursueTarget());
                }

                else if (hitName.Equals("Globbington"))
                {
                    currentlyPursuing = true;
                    currentTarget = hit.collider.gameObject;
                    StartCoroutine(PursueTarget());
                }

                else
                {
                    currentlyPursuing = false;
                }

            }
            yield return new WaitForSeconds(0.15f);
        }
    }

    public IEnumerator PursueTarget()
    {
        
        while(currentlyPursuing)
        {
            Debug.Log("die");
            Vector2 AAA = Vector2.MoveTowards(transform.position, gop.transform.position, Speed);
            Vector2 BAA = AAA - (Vector2)transform.position;
            rb.velocity = BAA;
            Debug.Log(BAA);

            yield return new WaitForSeconds(0.1f);
        }
        currentTarget = null;
        rb.velocity = Vector2.zero;
    }
}
