﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour {
    public Transform[] points;

    private void OnDrawGizmos()
    {
        iTween.DrawPath(points);
    }
}
