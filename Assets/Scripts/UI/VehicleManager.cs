using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VehicleManager : MonoBehaviour
{   
    public static VehicleManager instance;
    public GameObject[] vehiclePrefabs; 
    public Button[] vehicleButtons; 
    public CameraFollow cameraFollow; 
    public Transform vehicleSpawnPoint; 
    public GameObject uiPanel; 

    public GameObject PlatformSpawner; 
    
    private int selectedVehicleIndex = -1; 

    public GameObject currentVehicle; 

    private void Start()
    {
        for (int i = 0; i < vehicleButtons.Length; i++)
        {
            int vehicleIndex = i; 

            vehicleButtons[i].onClick.AddListener(() =>
            {
                SelectVehicle(vehicleIndex);
            });
        }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void SelectVehicle(int vehicleIndex)
    {
        selectedVehicleIndex = vehicleIndex;

        if (currentVehicle != null)
        {
            Destroy(currentVehicle);
        }

        currentVehicle = Instantiate(vehiclePrefabs[selectedVehicleIndex], vehicleSpawnPoint.position, vehicleSpawnPoint.rotation);

        cameraFollow.SetTarget(currentVehicle.transform);

        GameManager.instance.ReloadLevelAfterSelect();

        uiPanel.SetActive(false);
        
    }
}
