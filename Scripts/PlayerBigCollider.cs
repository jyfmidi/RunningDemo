using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBigCollider : MonoBehaviour {
    private PlayerAnimation playerAnimation;
    public GameObject bigBox;
    public GameObject smallBox;
    private void Awake()
    {
        playerAnimation = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerAnimation>();

    }
    private void Update()
    {
        if (playerAnimation.playerMove.isSliding)
        {
            bigBox.SetActive(false);
            smallBox.SetActive(true);
        }
        else
        {
            bigBox.SetActive(true);
            smallBox.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.obstacles && GameController.gameState == GameState.Playing)
            GameController.gameState = GameState.End;
    }
}
