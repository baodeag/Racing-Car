using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CarController : MonoBehaviour
{
    public float MoveSpeed;
    bool movingLeft = true;
    bool firstInput = true;

    private float originalSpeed; 
    private float currentSpeed; 
    private bool speedingUp; 
    private bool hasFallen = false; 

    public delegate void StarCollected();
    public static event StarCollected OnStarCollected;

    void Start()
    {
        originalSpeed = MoveSpeed;
    }

    void Update()
    {
        if (GameManager.instance.gameStarted )
        {
            Move();
            CheckInput();
        }

        if (!hasFallen && transform.position.y <= -2)
        {

        hasFallen = true; 
        GameManager.instance.GameOver();

        }

        if (speedingUp)
        {
            currentSpeed -= Time.deltaTime * 4f; 
            MoveSpeed = currentSpeed;

            if (currentSpeed <= originalSpeed)
            {
                speedingUp = false;
                MoveSpeed = originalSpeed; 
            }
        }
    }

    void Move()
    {
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;
    }

    private bool IsPointerOverUIObject() 
{
    PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
    eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    List<RaycastResult> results = new List<RaycastResult>();
    EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
    return results.Count > 0;
}


    void CheckInput()
{   
   
    if (OptionsMenu.isOptionsPanelActive) return; 
    if (firstInput)
    {
        firstInput = false;
        return;
    }
    if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
    {
        ChangeDirection();
        
    }
}

    void ChangeDirection()
    {
        if (movingLeft)
        {
            movingLeft = false;
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            movingLeft = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void SpeedUp(float speedBoost)
    {
        speedingUp = true;
        currentSpeed = MoveSpeed + speedBoost;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Star"))
        {
            SpeedUp(0f);
            Destroy(other.gameObject);

            AudioManager.instance.PlayCoinSound();
            
            if (OnStarCollected != null)
            {
                OnStarCollected();
            }
        }
        
    }
}
