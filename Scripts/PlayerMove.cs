using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Dir
{
    None,
    Left,
    Right,
    Top,
    Bottom
}

public class PlayerMove : MonoBehaviour {
    public float moveSpeed = 100;
    public float moveHorizontalSpeed = 5;
    public int nowLaneIndex = 1;
    public int targetLaneIndex = 1;
    public bool isSliding = false;
    public float slideTime = 1.0f;
    public float jumpTime = 1.0f;
    public bool isJumping = false;
    public float jumpHeight = 20;
    public float jumpSpeed = 40;

    private float haveJumpHeight = 0;
    private bool isUp = true;
    private float slideTimer = 0;
    private float jumpTimer = 0;
    private EnvGenerator envGenerator;
    private Dir dir = Dir.None;
    private float moveHorizontal = 0;
    private float[] xOffset=new float[3]{-14,0,14};
    //private Transform boy;

    private void Awake()
    {
        envGenerator = Camera.main.GetComponent<EnvGenerator>();
        //boy = transform.Find("player").transform;
    }

    // Update is called once per frame
    void Update () {
        if (GameController.gameState == GameState.Playing)
        {
            Vector3 targetPos = envGenerator.forest1.GetNextTargetPoint();
            targetPos = new Vector3(targetPos.x + xOffset[targetLaneIndex],
                targetPos.y, targetPos.z);
            Vector3 moveDir = targetPos - transform.position;
            transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;

            MoveControl();
        }
	}
    private void MoveControl()
    {
        Dir dir = GetDir();
        if (targetLaneIndex != nowLaneIndex)
        {
            float moveLength = Mathf.Lerp(0, moveHorizontal, moveHorizontalSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x + moveLength,
                transform.position.y,
                transform.position.z);
            moveHorizontal -= moveLength;
            if (Mathf.Abs(moveHorizontal) < 0.5f)
            {
                transform.position = new Vector3(transform.position.x + moveHorizontal,
                transform.position.y,
                transform.position.z);
                moveHorizontal = 0;
                nowLaneIndex = targetLaneIndex;
            }
        }
        if (isSliding)
        {
            slideTimer += Time.deltaTime;
            if (slideTimer > slideTime)
            {
                slideTimer = 0;
                isSliding = false;
            }
        }

        if (isJumping)
        {
            jumpTimer += Time.deltaTime;
            float yMove = jumpSpeed * Time.deltaTime;
            if (isUp && haveJumpHeight <= jumpHeight)
            {

                transform.position = new Vector3(transform.position.x,
                    transform.position.y + yMove,
                    transform.position.z);
                haveJumpHeight += yMove;
                if (Mathf.Abs(jumpHeight - haveJumpHeight) < 0.5f)
                {
                    transform.position = new Vector3(transform.position.x,
                        transform.position.y + jumpHeight - haveJumpHeight,
                        transform.position.z);
                    isUp = false;
                    haveJumpHeight = jumpHeight;
                }
            }
            else if (isUp && haveJumpHeight > jumpHeight)
            {
                isUp = false;
                haveJumpHeight = jumpHeight;
            }
            else if (jumpTimer >= jumpTime)
            {
                isUp = false;
                isJumping = false;
                jumpTimer = 0;
            }
            else
            {
                if (transform.position.y - yMove >= 0)
                {
                    transform.position = new Vector3(transform.position.x,
                        transform.position.y - yMove,
                        transform.position.z);
                }
                haveJumpHeight -= yMove;
                if (Mathf.Abs(haveJumpHeight - 0) < 0.5f)
                {
                    if (transform.position.y - yMove >= 0)
                    {
                        transform.position = new Vector3(transform.position.x,
                            transform.position.y - haveJumpHeight,
                            transform.position.z);
                    }
                    isJumping = false;
                    jumpTimer = 0;
                    haveJumpHeight = 0;
                }
            }

        }

    }

    Dir GetDir()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isJumping == false)
            {
                isJumping = true;
                isUp = true;
                jumpTimer = 0;
                haveJumpHeight = 0;

            }
            return Dir.Top;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            isSliding = true;
            slideTimer = 0;
            return Dir.Bottom;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (targetLaneIndex > 0)
            {
                targetLaneIndex--;
                moveHorizontal = -14;
            }
            return Dir.Left;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (targetLaneIndex < 2)
            {
                targetLaneIndex++;
                moveHorizontal = 14;
            }
            return Dir.Right;
        }
        return Dir.None;
    }
}
