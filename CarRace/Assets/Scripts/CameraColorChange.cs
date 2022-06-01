using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColorChange : MonoBehaviour
{
    public Color[] colors;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ColorChange");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ColorChange()
    {
        while (true)
        {
            int randColor = Random.Range(0,6);
            Camera.main.backgroundColor = colors[randColor];
            yield return new WaitForSeconds(10f);
        }
    }
}
