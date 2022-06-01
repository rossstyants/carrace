using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsSelector : MonoBehaviour
{

    public int currentCarIndex;
    public GameObject[] carsModules;
    // Start is called before the first frame update
    void Start()
    {
        currentCarIndex = PlayerPrefs.GetInt("SelectedCar", 0);
        foreach (GameObject car in carsModules)
        {
            car.SetActive(false);
        }
        carsModules[currentCarIndex].SetActive(true);


    }
}
