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
    public LayerMask LM;
    public float RayCastDistance;
    public float AngleRecalculationDelay;
    public Orientation MirrorOrientation;
    public enum Orientation { Vertical, Horizontal };

    [Header("Unity Stuff")]
    public Light2D Light;
    public GameObject LightAnchor;

    [Header("Debug")]
    public List<GameObject> LightSources = new List<GameObject>();

    private Vector2 mirrorPosition;

    [Header("Debug")]
    public bool FishVisible;
    public bool HittingAnotherMirror;
    
    // Start is called before the first frame update
    void Start()
    {
        mirrorPosition = this.gameObject.transform.position;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;

        if ( tag.Equals("Light") )
        {
            LightSources.Add(collision.gameObject);
            LightAnchor.SetActive(true);
            FishVisible = true;
            StartCoroutine(CalculateMirror());
           
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag.Equals("Light"))
        {
            LightSources.Remove(collision.gameObject);

            if(LightSources.Count == 0)
            {
                FishVisible = false;
                LightAnchor.SetActive(false);
            }
            
        }
    }

    /// <summary>
    /// Constantly rotates the mirror to face away from the player. Like a real light bouncing off.
    /// Does a lot of cool math that I copied and pasted from online.
    /// </summary>
    private IEnumerator CalculateMirror()
    {
        MirrorBehavior targetMirror = null;

        while(LightSources.Count > 0)
        {
            for (int i = 0; i < LightSources.Count; i++)
            {
                Vector3 sourcePosition = LightSources[i].transform.position;

                if (MirrorOrientation.Equals(Orientation.Vertical))
                    sourcePosition = new Vector3(sourcePosition.x, -sourcePosition.y, 0);

                else // horizontal
                    sourcePosition = new Vector3(-sourcePosition.x, sourcePosition.y, 0);

                AngleMirror(sourcePosition);

                //Code that sends off a glorious beam of light:

                Vector3 origin = LightAnchor.transform.position;
                Vector3 direction = sourcePosition - origin;
                Debug.Log(direction);

                this.gameObject.layer = 0;

                var hit = Physics2D.Raycast(origin, direction, RayCastDistance, LM);
                Debug.DrawLine(origin, hit.point, Color.green, 0.5f);

                this.gameObject.layer = 11;

                if (hit)
                {
                    string tag = hit.collider.tag;
                    Debug.Log(tag);
                    if (tag.Equals("Mirror"))
                    {
                        targetMirror = HitMirror(hit.collider.gameObject);
                    }
                    else if (HittingAnotherMirror)
                    {
                        UnhitMirror(targetMirror);
                    }
                }
                
                    
            }
            
            yield return new WaitForSeconds(AngleRecalculationDelay);
        }
    }

    /// <summary>
    /// Rotates mirror away from light source
    /// </summary>
    private void AngleMirror(Vector3 sourcePosition)
    {
        //vector between two points
        Vector2 Direction = transform.position - sourcePosition;
        float angle = Vector2.SignedAngle(Vector2.right, Direction) + 90;

        Vector3 TargetRotation = new Vector3(0, 0, angle);
        LightAnchor.transform.rotation = Quaternion.RotateTowards(LightAnchor.transform.rotation, Quaternion.Euler(TargetRotation), 100);
    }

    /// <summary>
    /// code for when raycast hits a mirror.
    /// function finds target mirror and tells it that it was hit
    /// </summary>
    /// <param name="Mirror"></param>
    /// <returns>target mirrors MirrorBehavior</returns>
    private MirrorBehavior HitMirror(GameObject Mirror)
    {
        MirrorBehavior mb =  Mirror.GetComponent<MirrorBehavior>();
        mb.LightSources[0]=this.gameObject;
        HittingAnotherMirror = true;

        return mb;
    }

    private void UnhitMirror(MirrorBehavior Mirror)
    {
        if(Mirror != null)
        {
            Mirror.LightSources.Clear();

        }
    }

    public bool GorpVisible()
    {
        /*
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
        */
        return false;
    }

    /// <summary>
    /// Continously searches for parents of Child until it comes up null.
    /// Climbing up the family tree yk.
    /// </summary>
    /// <returns>The source of the Child</returns>
    public GameObject GetTopParent(GameObject Child) 
    {
        GameObject newParent = Child.transform.parent.gameObject;
        GameObject parent = Child;

        //climbing the ladder:
        while (newParent != null) 
        {
            parent = newParent;
            try
            {
                newParent = parent.transform.parent.gameObject;
            }
            catch
            {
                newParent = null;
            }
        }

        return parent;
    }
}
