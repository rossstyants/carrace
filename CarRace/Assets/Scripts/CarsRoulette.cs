using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsRoulette : MonoBehaviour
{
    public enum StateEnum
    {
        ChoosingCar,
        Chosen
    }

    [SerializeField] private Material _groundMat;
    public Color[] backColour = new Color[4];
    public Color[] trackColour = new Color[4];

    public Color targetColor;

    public StateEnum State;

    [SerializeField] private float _timeTweenCars;
    private float _swapTimer;

    public int currentCarIndex;
    public GameObject[] carsModules;
    // Start is called before the first frame update

    void Start()
    {
        StartChoosing();
    }

    private void ChooseCar(int carId)
    {
        currentCarIndex = carId;

        foreach (GameObject car in carsModules)
        {
            car.SetActive(false);
        }
        carsModules[currentCarIndex].SetActive(true);
        targetColor = backColour[currentCarIndex];
        _groundMat.SetColor("_Color", trackColour[currentCarIndex]);
    }

    public void StartChoosing()
    {
        State = StateEnum.ChoosingCar;
        currentCarIndex = 0;
        ChooseCar(currentCarIndex);
    }
    public void ChooseCurrentCar()
    {
        State = StateEnum.Chosen;
        PlayerPrefs.SetInt("SelectedCar", currentCarIndex);
    }    

    private void Update()
    {
        if (State == StateEnum.ChoosingCar)
        {
            _swapTimer += Time.deltaTime;
            if (_swapTimer >= _timeTweenCars)
            {
                _swapTimer -= _timeTweenCars;
                currentCarIndex++;
                if (currentCarIndex == carsModules.Length)
                {
                    currentCarIndex = 0;
                }
                ChooseCar(currentCarIndex);
            }
        }
    }
}
