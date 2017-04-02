using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowFollower : MonoBehaviour {
    private Transform player;

    public float moveSpeed = 7;

    private Vector3 offset = Vector3.zero;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        offset = transform.position - player.position;
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = player.position + offset;
        targetPos.y = 0.5f;
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }
}
