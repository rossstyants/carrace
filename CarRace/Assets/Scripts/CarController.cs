using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speedMove;                     // car speed value
    bool movingLeft = true;
    bool firstInput = true;
    public GameObject pickUpEffect;          // reference to particle system
    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameStarted)           // check if the game started is true call move and chekInput functions
        {
            Move();
            CheckInput();
        }
        if(transform.position.y <= -2)                 // if car fall call game over screen
        {
            GameManager.instance.GameOver();
        }
        
    }
    void Move()
    {
        //Increase speed by time
        if (speedMove < 9f)
        {
            speedMove += 0.1f * Time.deltaTime;
        }
        
        transform.position += transform.forward * speedMove * Time.deltaTime;
        
    }

    void CheckInput()
    { // if firstInput then ignore to changedirection
        if (firstInput)
        {
            firstInput = false;
            return;
        }
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            ChangeDirection();
        }
    }
    void ChangeDirection()
    {
        if (movingLeft)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            movingLeft = false;
        }
        else
        {
            
            transform.rotation = Quaternion.Euler(0, 0, 0);
            movingLeft = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Coin")
        {
            GameManager.instance.IncrementScore();
            
            Instantiate(pickUpEffect, other.transform.position, pickUpEffect.transform.rotation);
            
            other.gameObject.SetActive(false);
           
            
        }
    }

}
