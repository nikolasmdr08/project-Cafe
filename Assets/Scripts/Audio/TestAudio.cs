using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.S_PlayMusic(SFXID.MUSIC_SCENE, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
