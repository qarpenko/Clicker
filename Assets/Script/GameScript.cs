using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameScript : MonoBehaviour
{
    [SerializeField] int Score;
    [SerializeField] int BonusScore;
    [SerializeField] int Eco;
    [SerializeField] int Krem;

    public int[] sell;
    public TMP_Text[] selltext;
    private int Clicks = 1;
    public int[] TimeScore;


    [SerializeField] TMP_Text ScoreText;
    [SerializeField] TMP_Text BonusText;
    [SerializeField] TMP_Text EcoText;
    [SerializeField] TMP_Text KremText;

    public GameObject EcoShop;
    public GameObject BonusShop;
    public GameObject Shop;
    public GameObject TimeShop;
    public GameObject Factory;
    public GameObject Settings;

    private Save SV = new Save();

    private void Awake()
    {
        if(PlayerPrefs.HasKey("save"))
        {
            SV = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("save"));
            Score = SV.Score;
            Eco = SV.Eco;
            Krem = SV.Krem;
            BonusScore = SV.BonusScore;
            Clicks = SV.Clickers;

            for (int i = 0; i < 5; i++)
            {
                sell[i] = SV.sell[i];
                selltext[i].text = SV.sell[i].ToString();
            }

            for (int i = 0; i < 2; i++)
            {
                SV.TimeScore[i] = SV.TimeScore[i];
                selltext[i].text = SV.sell[i].ToString();

            }
        }
    }

    public void EcoShopShowAndHide()
    {
        EcoShop.SetActive(!EcoShop.activeSelf);
    }

    public void BonusMarketShowAndHide()
    {
        BonusShop.SetActive(!BonusShop.activeSelf);
    }

    public void ShopShowAndHide()
    {
        Shop.SetActive(!Shop.activeSelf);
    }

    public void TimeShopShowAndHide()
    {
        TimeShop.SetActive(!TimeShop.activeSelf);
    }

    public void factoryShowAndHide()
    {
        Factory.SetActive(!Factory.activeSelf);
    }

    public void SetShowAndHide()
    {
        Settings.SetActive(!Settings.activeSelf);
    }

    public void Clicker()
    {
        Score += Clicks;
        BonusScore++;
    }

    public void EcoBuy()
    {
        if(Score >= 25)
        {
            Eco += 1;
            Score -= 25;
        }
    }

    public void Bonus()
    {
        if(BonusScore >= 100)
        {
            BonusScore -= 100;
            Eco += 5;
        }
    }

    public void OneSell()
    {
        if(Eco >= sell[0])
        {
            Eco -= sell[0];
            sell[0] += 25;
            Clicks += 1;
            selltext[0].text = sell[0] + "Eco";
        }
    }

    public void TwoSell()
    {
        if (Eco >= sell[2])
        {
            Eco -= sell[2];
            sell[2] += 55;
            Clicks += 5;
            selltext[2].text = sell[2] + "Eco";
        }
    }

    public void TimeSell()
    {
        if(Eco >= sell[1])
        {
            Eco -= sell[1];
            TimeScore[0] += 1;
            sell[1] += 50;
            selltext[1].text = sell[1] + "Eco";
        }
    }

    public void TimeSell2()
    {
        if (Eco >= sell[3])
        {
            Eco -= sell[3];
            TimeScore[0] += 5;
            sell[3] += 100;
            selltext[3].text = sell[3] + "Eco";
        }
    }

    public void CremFactory()
    {
        if(Eco >= sell[4])
        {
            Eco -= sell[4];
            TimeScore[1] += 1;
            sell[4] += 100;
            selltext[4].text = sell[4] + "Eco";
        }
    }

    public void CremBuy()
    {
        if(Krem >= 1)
        {
            Krem -= 1;
            Eco += 3;
        }
    }

    public void OnApplicationQuit()
    {
        SV.Score = Score;
        SV.Clickers = Clicks;
        SV.Krem = Krem;
        SV.BonusScore = BonusScore;
        SV.Eco = Eco;
        SV.sell = new int[5];
        SV.TimeScore = new int[2];

        for(int i = 0; i < 5; i++)
        {
            SV.sell[i] = sell[i];
        }

        for (int i = 0; i < 2; i++)
        {
            SV.TimeScore[i] = TimeScore[i];
        }

        PlayerPrefs.SetString("save", JsonUtility.ToJson(SV));
    }
    IEnumerator Times()
    {
        while(true)
        {
            Score += TimeScore[0];
            Krem += TimeScore[1];
            yield return new WaitForSeconds(1);
        }
    }

    private void Start()
    {
        StartCoroutine(Times());
    }

    private void Update()
    {
        ScoreText.text = Score.ToString(); 
        EcoText.text = Eco.ToString();
        BonusText.text = BonusText.ToString();
        KremText.text = Krem.ToString();
    }

    [Serializable]
    public class Save
    {
        public int Score;
        public int BonusScore;
        public int Krem;
        public int Eco;

        public int[] TimeScore;
        public int[] sell;

        public int Clickers;


    }
   
}
