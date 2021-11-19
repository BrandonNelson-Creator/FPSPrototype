using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCollect : MonoBehaviour
{
    public TextMeshProUGUI countText;
    public TextMeshProUGUI goldText;
    public GameObject coinText;
    public GameObject mission;
    public GameObject mission1A;
    public GameObject textBub1;
    public GameObject textBub2;
    public GameObject gold;
    public GameObject winner;
    public GameObject goldCount;
    public GameObject mission1b;

    private int count;

    private void Start()
    {
        count = 0;
        SetGoldText();

        SetCountText();
        
        coinText.SetActive(false);
        
        

    }

    private void SetCountText()
    {
        countText.text = "Coin: " + count.ToString() + "/10";
        if(count >= 1)
        {
            coinText.SetActive(true);
        }
        if(count >= 10)
        {
            
            coinText.SetActive(false);
            mission.SetActive(false);
            mission1A.SetActive(true);
            textBub1.SetActive(false);
            textBub2.SetActive(true);
            gold.SetActive(true);
        }
        

        
            
    }
    private void SetGoldText()
    {
        goldText.text = "Gold " + count.ToString() + "/1";
        if (count >= 1)
        {
            goldCount.SetActive(true);
            
            winner.SetActive(true);
            
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
        if (other.gameObject.CompareTag("gold"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            winner.SetActive(true);
            mission1b.SetActive(false);
            mission1A.SetActive(false);
        }  
        
        
        
    }
}
