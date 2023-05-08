/*******************************************************************************
// File Name :         JellyfishSound.cs
// Author(s) :         Toby Schamberger
// Creation Date :     5/7/2023 10:52 pm
//
// Brief Description : plays a sound when player gets in radius
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishSound : MonoBehaviour
{
    public JellyfishBehavior JellyFish;
    public AudioSource JellyAudio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            if (!JellyFish.GameManagerInstance.SFX)
                JellyAudio.mute = true;

            if (JellyFish.JellyState.ToString().Equals("Passive"))
            {
                JellyAudio.mute = false;
                JellyFish.WasMute = false;
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (JellyFish.JellyState.ToString().Equals("Passive"))
            {
                JellyAudio.mute = true;

            }
            JellyFish.WasMute = true;
        }
    }

}
