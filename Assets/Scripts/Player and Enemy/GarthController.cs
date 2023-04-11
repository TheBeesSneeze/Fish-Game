using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
/*******************************************************************************
// File Name :         GarthController.cs
// Author(s) :         Sky Beal, Jay Embry
// Creation Date :     4/6/2023
//
// Brief Description : Controls when and how Garth speaks !!
*****************************************************************************/

public class GarthController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] [TextArea] public List<string> TextList = new List<string>();
    [SerializeField] public float ScrollSpeed;
    [SerializeField] private TextMeshProUGUI itemInfoText;
    private int currentDisplayingText = 0;

    [Header("Unity Stuff")]
    private PlayerController gorp;
    private bool typing;
    public GameObject TextBox;

    /// <summary>
    /// when on speaking box, can start text
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag.Equals("Player"))
        {
            gorp = collision.GetComponent<PlayerController>();
            gorp.Select.started += ActivateSpeech;
        }
    }

    /// <summary>
    /// when off speaking box, no start text
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;

        if (tag.Equals("Player"))
        {
            gorp = collision.GetComponent<PlayerController>();
            gorp.Select.started -= ActivateSpeech;
        }

        TextBox.SetActive(false);
    }

    /// <summary>
    /// starts the coroutine
    /// </summary>
    /// <param name="obj"></param>
    public void ActivateSpeech(InputAction.CallbackContext obj)
    {
        if (!typing)
            StartCoroutine(StartText());
    }

    /// <summary>
    /// coroutine that does the scrolly text
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartText()
    {
        typing = true;
        TextBox.SetActive(true);
        for (int i = 0; i < TextList[currentDisplayingText].Length + 1; i++)
        {
            itemInfoText.text = TextList[currentDisplayingText].Substring(0, i);
            yield return new WaitForSeconds(ScrollSpeed);
        }

        typing = false;
        currentDisplayingText++;

        if (currentDisplayingText == TextList.Count)
            currentDisplayingText = 0;
    }
}
