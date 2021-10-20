using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCollect : MonoBehaviour
{
    public TextMeshProUGUI countText;
    public GameObject key;
    public GameObject coinText;
    public GameObject mission;
    public GameObject mission1A;
    public GameObject textBub1;
    public GameObject textBub2;

    private int count;

    private void Start()
    {
        count = 0;

        SetCountText();
        key.SetActive(false);
        coinText.SetActive(false);
    }

    private void SetCountText()
    {
        countText.text = "Coin: " + count.ToString();
        if(count >= 1)
        {
            coinText.SetActive(true);
        }
        if(count >= 10)
        {
            key.SetActive(true);
            coinText.SetActive(false);
            mission.SetActive(false);
            mission1A.SetActive(true);
            textBub1.SetActive(false);
            textBub2.SetActive(true);
            
        }
        

        
            
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }   

        if (other.gameObject.CompareTag("Key"))
        {
            other.gameObject.SetActive(false);
            
            SetCountText();
        }
            
        
        
        
    }
}
