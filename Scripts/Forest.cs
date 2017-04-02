using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : MonoBehaviour {
    public GameObject[] obstacles;
    public float startLength = 50;
    public float minLength = 100;
    public float maxLength = 200;

    private Transform player;
    private WayPoints wayPoints;
    private int targetWayPointIndex;
    private EnvGenerator envGenerator;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        wayPoints = transform.Find("wayPoints").GetComponent<WayPoints>();
        targetWayPointIndex = wayPoints.points.Length - 2;
        envGenerator = Camera.main.GetComponent<EnvGenerator>();
    }
    // Use this for initialization
    void Start () {
        GenerateObstacle();
	}
	
	// Update is called once per frame
	void Update () {
        //if (player.position.z > transform.position.z+100)
        //{
        //    Camera.main.SendMessage("GenerateForest");
        //    GameObject.Destroy(this.gameObject);
        //}	
	}
    Vector3 GetWayPosByZ(float z)
    {
        Transform[] points = wayPoints.points;
        int index = 0;
        for (int i = 0; i < wayPoints.points.Length - 1; i++)
        {
            if (z <= points[i].position.z && z >= points[i + 1].position.z)
            {
                index = i;
                break;
            }
        }
        //index z index+1
        return Vector3.Lerp(points[index + 1].position,
            points[index].position,
            (z - points[index + 1].position.z) / (points[index].position.z - points[index + 1].position.z));
    }


    void GenerateObstacle()
    {
        float startZ = transform.position.z - 3000;
        float endZ = startZ + 3000;
        float z = startZ+startLength;
        while (true)
        {
            z += Random.Range(minLength, maxLength);
            if (z > endZ)
                break;
            else
            {
                Vector3 position = GetWayPosByZ(z);
                //create obstacles
                //1.generate obstacle
                //2.randomly set type
                int obsIndex = Random.Range(0, obstacles.Length);
                GameObject go = GameObject.Instantiate(obstacles[obsIndex], position, Quaternion.identity) as GameObject;
                go.transform.parent = this.transform;
            }
        }
    }

    public Vector3 GetNextTargetPoint()
    {
        while (true)
        {
            if ((wayPoints.points[targetWayPointIndex].position.z - player.position.z) < 10)
            {
                targetWayPointIndex--;
                if (targetWayPointIndex < 0)
                {
                    envGenerator.GenerateForest();
                    Destroy(this.gameObject, 5);
                    return envGenerator.forest1.GetNextTargetPoint();
                }
            }
            else
            {
                return wayPoints.points[targetWayPointIndex].position;
            }
        }
    }
 
}
