using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {

    public AudioSource fail;
    public AudioSource jump;
    private bool havePlayMusic;
    //private PlayerAnimation playerAnimation;

    //private void Awake()
    //{
    //    playerAnimation = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerAnimation>();
    //}

    void Update()
    {
        if (havePlayMusic == false && GameController.gameState == GameState.End)
        {
            fail.Play();
            havePlayMusic = true;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
            jump.Play();

    }
}
