using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public int currentPlayerIndex;                                  // car index
    public GameObject[] PlayersModules;                             // reference for cars
    public CarBlueprint[] cars;                                     // reference for cars parametre( name, index, price)
    public Button buyButton;



    // Start is called before the first frame update
    void Start()
    {
        foreach (CarBlueprint car in cars)
        {
            if (car.price == 0)                                                 // if car price equal 0, locked this car,
                car.isLocked = true;
            else
                car.isLocked = PlayerPrefs.GetInt(car.name, 0) == 0 ? false : true;     //unlocked cars deosn't equal 0
        }

        currentPlayerIndex = PlayerPrefs.GetInt("SelectedCar", 0);             // get the current car (selected car in shop)
        foreach (GameObject player in PlayersModules)
        {
            player.SetActive(false);                         // desactive all cars
        }
        PlayersModules[currentPlayerIndex].SetActive(true);   //active the selected car


    }

    // Update is called once per frame
    void Update()
    {
        UpdateUi();
    }
    public void Next()
    {
        PlayersModules[currentPlayerIndex].SetActive(false);              // desactive current car 
        currentPlayerIndex++;                                             // move the next car in the menu

        if (currentPlayerIndex == PlayersModules.Length)                  // if current car index equal the last car in the shop return To the first car =0
            currentPlayerIndex = 0;



        PlayersModules[currentPlayerIndex].SetActive(true);              //active the selected car
        CarBlueprint p = cars[currentPlayerIndex];
        if (!p.isLocked)                                          // check if the selected car is unlocked 
            return;
        PlayerPrefs.SetInt("SelectedCar", currentPlayerIndex);       // save the selected car

    }

    public void Previous()
    {
        PlayersModules[currentPlayerIndex].SetActive(false);             
        currentPlayerIndex--;

        if (currentPlayerIndex == -1)
            currentPlayerIndex = PlayersModules.Length - 1;



        PlayersModules[currentPlayerIndex].SetActive(true);
        CarBlueprint p = cars[currentPlayerIndex];
        if (!p.isLocked)
            return;
        PlayerPrefs.SetInt("SelectedCar", currentPlayerIndex);

    }

    public void UpdateUi()
    {
        CarBlueprint p = cars[currentPlayerIndex];              // check if the car is locked
        if (p.isLocked)
        {
            buyButton.gameObject.SetActive(false);            // desactive the buy button
        }
        else
        {
            buyButton.gameObject.SetActive(true);                                  // if the selected car is unlocked active the buy button
            buyButton.GetComponentInChildren<Text>().text = "Buy-" + p.price;      // get the price of the car
            if (p.price < PlayerPrefs.GetInt("Score", GameManager.points))         // check if the price less than collected cash 
            {
                buyButton.interactable = true;                                    // buy button equal true
            }
            else
            {
                buyButton.interactable = false;                                   // buy button equal false
            }
        }
    }

    public void UnlockPlayer()
    {
        CarBlueprint p = cars[currentPlayerIndex];                                    
        PlayerPrefs.SetInt(p.name, 1);
        PlayerPrefs.SetInt("SelectedCar", currentPlayerIndex);                                               //save the locked car
        p.isLocked = true;
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score", 0) - p.price);                               // munus the price car from yhe total cash an save 
    }

    public void PlayBtn()
    {
        SceneManager.LoadScene("Game");      
    }
}
