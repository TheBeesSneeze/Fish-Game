/*******************************************************************************
// File Name :         MirrorBehavior.cs
// Author(s) :         Toby Schamberger
// Creation Date :     4/1/2023
//
// This code was made on a whim. Searches for gorp, when gorp is visible, it
// searches for things affected by light (mirrors and enemies)
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.UI.Image;

public class MirrorBehavior : MonoBehaviour
{
    [Header("Settings")]
    public float AngleRecalculationDelay;
    public Orientation MirrorOrientation;
    public enum Orientation { Vertical, Horizontal };

    [Header("Unity Stuff")]
    public LayerMask LM;
    public Light2D Light;
    public GameObject LightAnchor;

    public GameObject gorp;
    private LightController gorpLightController;
    private PlayerController gorpController;

    private Vector2 mirrorPosition;

    [Header("Debug")]
    public bool FishVisible;
    
    // Start is called before the first frame update
    void Start()
    {
        mirrorPosition = this.gameObject.transform.position;
        StartCoroutine(SearchForGorp());
    }

    // Update is called once per frame
    public IEnumerator SearchForGorp()
    {
        //code for the time that gorp dont exist
        while (gorp == null)
        {
            gorp = GameObject.Find("Gorp");

            yield return new WaitForSeconds(1.00f);
        }
        Debug.Log("Found nemo");
        gorpController = gorp.GetComponent<PlayerController>();
        gorpLightController = gorp.GetComponent<LightController>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        Debug.Log(tag);
        if (tag.Equals("Light"))
        {
            LightAnchor.SetActive(true);
            FishVisible = true;
            StartCoroutine(AngleMirror());
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag.Equals("Light"))
        {
            FishVisible = false;
            LightAnchor.SetActive(false);
        }
    }

    public IEnumerator AngleMirror()
    {
        while(FishVisible)
        {
            Debug.Log("recalculating mirror");
            float angle = Vector2.Angle((Vector2)mirrorPosition, gorp.transform.position);
            Debug.Log(angle);

            Vector3 origin = LightAnchor.transform.position;
            Vector3 direction = gorp.transform.position - origin;

            //lightAnchor.transform.Rotate(0,0,angle);
            LightAnchor.transform.LookAt(gorp.transform);
            LightAnchor.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, direction.y));

            yield return new WaitForSeconds(AngleRecalculationDelay);
        }
        
    }

    public bool GorpVisible()
    {
        Vector3 origin = LightAnchor.transform.position;
        Vector3 direction = gorp.transform.position - origin;

        var hit = Physics2D.Raycast(origin, direction, gorpLightController.LightRadius, LM);
        Debug.DrawLine(origin, gorp.transform.position, Color.green, 0.5f);

        //Searching for a victim!
        if (hit)
        {
            string hitName = hit.collider.gameObject.name;
            Debug.Log(hitName);
            if (hitName.Equals("Gorp"))
            {
                FishVisible = true;
                return true;
            }
            else
            {
                FishVisible = false;
                return false;
            }
        }
        else
        { 
            FishVisible = false;
            return false;
        }
    }
}
