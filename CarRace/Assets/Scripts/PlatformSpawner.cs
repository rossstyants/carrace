using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : Singleton<PlatformSpawner>
{
    [SerializeField] private float timeTweenPlatforms;
    [SerializeField] private float distanceToPlatform;
    [SerializeField] private int startPlatforms;
    float timer;

    public GameObject platform;

    public Transform lastPlatform;
    public Vector3 lastPosition;
    Vector3 newPos;

    bool stop;
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = lastPlatform.position;

        for (int i = 0; i < startPlatforms; i++)
        {
            GeneratePosition();
            Instantiate(platform, newPos, Quaternion.identity);
            lastPosition = newPos;
        }

        //Debug.Log("Start PLATFROM SPAWNER");

        //StartCoroutine(SpawnPlatforms());
    }

    //public void StartRacing()
    //{
    //    StartCoroutine(SpawnPlatforms());
    //}

    void Update()
    {
        //timer += Time.deltaTime;
        //if (timer >= timeTweenPlatforms)
        //{
        //    timer -= timeTweenPlatforms;

        //    BuildPlatform();
        //}
    }

    public void BuildPlatform()
    {
        GeneratePosition();
        Instantiate(platform, newPos, Quaternion.identity);
        lastPosition = newPos;
    }

    //IEnumerator SpawnPlatforms()
    //{
    //    while (!stop)
    //    {
    //        GeneratePosition();
    //        Instantiate(platform, newPos, Quaternion.identity);

    //        lastPosition = newPos;

    //        yield return new WaitForSeconds(0.1f);
    //    }
    //}

    void GeneratePosition()
    {
        newPos = lastPosition;

        int rand = Random.Range(0, 2);

        if(rand > 0)
        {
            newPos.x += 2f;
        }
        else
        {
            newPos.z += 2f;
        }
    }

}
