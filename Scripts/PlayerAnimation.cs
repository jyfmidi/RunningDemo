using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationState {
    Idle,Run,TurnLeft,TurnRight,Slide,Jump,Death
}

public class PlayerAnimation : MonoBehaviour {

    private new Animation animation;
    public AnimationState animationState = AnimationState.Idle;
    public Transform player;
    public PlayerMove playerMove;

	// Use this for initialization
	void Awake () {
        
        animation = player.GetComponent<Animation>();
        playerMove = player.GetComponent<PlayerMove>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameController.gameState == GameState.Menu)
            animationState = AnimationState.Idle;
        else if (GameController.gameState == GameState.Playing)
        {
            animationState = AnimationState.Run;
            
            if (playerMove.targetLaneIndex > playerMove.nowLaneIndex)
            {
                animationState = AnimationState.TurnRight;
            }
            if (playerMove.targetLaneIndex < playerMove.nowLaneIndex)
            {
                animationState = AnimationState.TurnLeft;
            }
            if (playerMove.isSliding)
            {
                animationState = AnimationState.Slide;
            }
            if (playerMove.isJumping)
            {
                animationState = AnimationState.Jump;
            }

        }
        else if (GameController.gameState == GameState.End)
        {
            animationState = AnimationState.Death;
        }
	}

    void LateUpdate()
    {
        switch (animationState)
        {
            case AnimationState.Idle:
                PlayAnimation("idle");
                break;
            case AnimationState.Run:
                PlayAnimation("run");
                break;
            case AnimationState.TurnLeft:
                animation["left"].speed = 2;
                PlayAnimation("left");
                break;
            case AnimationState.TurnRight:
                animation["right"].speed = 2;
                PlayAnimation("right");
                break;
            case AnimationState.Slide:
                PlayAnimation("slide");
                break;
            case AnimationState.Jump:
                PlayAnimation("jump");
                break;
            case AnimationState.Death:
                PlayDeath();
                break;
        }
    }

    private void PlayAnimation(string animName)
    {
        if (animation.IsPlaying(animName) == false)
        {
            animation.Play(animName);
        }
    }
    private bool havePlayDeath = false;
    private void PlayDeath()
    {
        if (!animation.IsPlaying("death") && havePlayDeath == false)
        {
            animation.Play("death");
            havePlayDeath = true;
        }
    }
}

