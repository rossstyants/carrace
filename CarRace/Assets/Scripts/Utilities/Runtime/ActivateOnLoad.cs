using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnLoad : MonoBehaviour {

    public bool activateOnLoad;
    public bool deactivateOnLoad;

    // Use this for initialization
    void Start () {
        if (deactivateOnLoad)
            gameObject.SetActive(false);
        if (activateOnLoad)
            gameObject.SetActive(true);
    }
}
