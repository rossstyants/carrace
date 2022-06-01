using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject coin;                                                      //reference to the coin
    // Start is called before the first frame update
    void Start()
    {
        int randCoin = Random.Range(0, 5);                                   //Random coin between 0 and 5
        Vector3 coinPos = transform.position;                                // coin position Ecual platform position
        coinPos.y += 1f;

        if(randCoin < 1)                                                     //check if randCoin less than 1 spawn random coin
        {
            // spawn random coin
            GameObject coinInstance = Instantiate(coin, coinPos, coin.transform.rotation);
            coinInstance.transform.SetParent(gameObject.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")           // check if the car exit from the platform invok fall 
        {
            Invoke("Fall", 0.2f);
        }
        
    }
    void Fall()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        Destroy(gameObject, 1f);

    }
}
