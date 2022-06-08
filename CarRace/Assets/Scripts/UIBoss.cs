using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBoss : Singleton<UIBoss>
{
    public Color darkBlueCol, brightWhiteCol;
    public GameObject boatPanel, lorryPanel, planePanel, trainPanel;
    public GameObject vehiclesChangingBoatPanel, vehiclesChangingRestPanel;
    public Text scoreText;

    public void ShowRouletteScreen(int carId)
    {
        vehiclesChangingBoatPanel.SetActive((GameManager.VehicleType)carId == GameManager.VehicleType.Boat);
        vehiclesChangingRestPanel.SetActive((GameManager.VehicleType)carId != GameManager.VehicleType.Boat);
    }
    public void ShowWaitingToStartScreen()
    {
        boatPanel.SetActive(GameManager.Instance.SelectedVehicle == GameManager.VehicleType.Boat);
        lorryPanel.SetActive(GameManager.Instance.SelectedVehicle == GameManager.VehicleType.Lorry);
        planePanel.SetActive(GameManager.Instance.SelectedVehicle == GameManager.VehicleType.Aeroplane);
        trainPanel.SetActive(GameManager.Instance.SelectedVehicle == GameManager.VehicleType.Train);

        if (GameManager.Instance.SelectedVehicle == GameManager.VehicleType.Boat || GameManager.Instance.SelectedVehicle == GameManager.VehicleType.Aeroplane)
        {
            scoreText.color = darkBlueCol;
        }
        else
        {
            scoreText.color = brightWhiteCol;
        }
    }
}
