using System;
using UnityEngine;
using UnityEngine.UI;

public class StatUpgrader : MonoBehaviour
{
    private YsbridManager Ysbrid;                                               // Objeto Ysbrid
    private DewinManager Dewin;                                                 // Objeto Dewin
    private CladdwydManager Cladd;                                              // Objeto Claddwyd

    public GameObject altarUpgrader;                                            // Objeto del altar
    public Canvas upgraderPanel;                                                // Canvas del panel

    public Image dewinPhyImage, claddPhyImage, dewinMagImage, claddMagImage;    // Imagenes de los personajes

    public Button ysbridPhyBtn, dewinPhyBtn, claddPhyBtn,                       // Botones de los personajes
        ysbridMagBtn, dewinMagBtn, claddMagBtn; 

    public Text chargesYsbPhy, chargesDewPhy, chargesCladdPhy,                  // Texto de las cargas de los personajes
        chargesYsbMag, chargesDewMag, chargesCladdMag;

    private bool isPanelActive;

    void Start()
    {
        upgraderPanel.enabled = false;

        Ysbrid = YsbridManager.ysbridInstance;

        if (DewinManager.dewinInstance != null)
            Dewin = DewinManager.dewinInstance;
        else
        {
            dewinPhyImage.color = Color.black;
            dewinMagImage.color = Color.black;
            dewinPhyBtn.interactable = false;
            dewinMagBtn.interactable = false;
        }

        if (CladdwydManager.claddInstance != null)
            Cladd = CladdwydManager.claddInstance;
        else
        {
            claddPhyImage.color = Color.black;
            claddMagImage.color = Color.black;
            claddPhyBtn.interactable = false;
            claddMagBtn.interactable = false;
        }
    }

    void Update()
    {
        InitializePhyCost();
        InitializeMagCost();
        ManageButtons();

        if ((Ysbrid.transform.position - altarUpgrader.transform.position).sqrMagnitude < 2.5 * 2.5)
        {
            Debug.Log("In Range");
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isPanelActive)
                    Close();
                else
                {
                    isPanelActive = true;
                    Debug.Log("Panel Active");
                    upgraderPanel.enabled = true;
                    Time.timeScale = 0f;
                }
            }
        }
    }

    public void Close()
    {
        isPanelActive = false;
        upgraderPanel.enabled = false;
        Time.timeScale = 1f;
    }

    public void UpgradeYsbridPhy()
    {
        if (Ysbrid.charges <= Int32.Parse(chargesYsbPhy.text))
            Debug.Log("Te faltan cargas");
        else
        {
            Ysbrid.physicLevel += 1;
            Ysbrid.charges -= Int32.Parse(chargesYsbPhy.text);
        }
    }

    public void UpgradeDewinPhy()
    {
        if (Ysbrid.charges <= Int32.Parse(chargesDewPhy.text))
            Debug.Log("Te faltan cargas");
        else
        {
            Dewin.dewinPhysic += 1;
            Ysbrid.charges -= Int32.Parse(chargesDewPhy.text);
        }
    }

    public void UpgradeCladdPhy()
    {
        if (Ysbrid.charges <= Int32.Parse(chargesCladdPhy.text))
            Debug.Log("Te faltan cargas");
        else
        {
            Cladd.claddwydPhysic += 1;
            Ysbrid.charges -= Int32.Parse(chargesCladdPhy.text);
        }
    }

    public void UpgradeYsbridMag()
    {
        if (Ysbrid.charges <= Int32.Parse(chargesYsbMag.text))
            Debug.Log("Te faltan cargas");
        else
        {
            Ysbrid.magicLevel += 1;
            Ysbrid.charges -= Int32.Parse(chargesYsbMag.text);
        }
    }

    public void UpgradeDewinMag()
    {
        if (Ysbrid.charges <= Int32.Parse(chargesDewMag.text))
            Debug.Log("Te faltan cargas");
        else
        {
            Dewin.dewinMagic += 1;
            Ysbrid.charges -= Int32.Parse(chargesDewMag.text);
        }
    }

    public void UpgradeCladdMag()
    {
        if (Ysbrid.charges <= Int32.Parse(chargesCladdMag.text))
            Debug.Log("Te faltan cargas");
        else
        {
            Cladd.claddwydMagic += 1;
            Ysbrid.charges -= Int32.Parse(chargesCladdMag.text);
        }
    }

    void InitializePhyCost()
    {
        switch (Ysbrid.physicLevel)
        {
            case 1:
                chargesYsbPhy.text = "20";
                break;

            case 2:
                chargesYsbPhy.text = "40";
                break;

            case 3:
                chargesYsbPhy.text = "60";
                break;

            case 4:
                chargesYsbPhy.text = "80";
                break;

            case 5:
                chargesYsbPhy.text = "100";
                break;

            case 6:
                chargesYsbPhy.text = "120";
                break;

            case 7:
                chargesYsbPhy.text = "140";
                break;

            case 8:
                chargesYsbPhy.text = "160";
                break;

            case 9:
                chargesYsbPhy.text = "200";
                break;

            case 10:
                ysbridPhyBtn.interactable = false;
                chargesYsbPhy.text = "MAX";
                break;
        }

        if(Dewin != null)
        {
            switch (Dewin.dewinPhysic)
            {
                case 1:
                    chargesDewPhy.text = "20";
                    break;

                case 2:
                    chargesDewPhy.text = "40";
                    break;

                case 3:
                    chargesDewPhy.text = "60";
                    break;

                case 4:
                    chargesDewPhy.text = "80";
                    break;

                case 5:
                    chargesDewPhy.text = "100";
                    break;

                case 6:
                    chargesDewPhy.text = "120";
                    break;

                case 7:
                    chargesDewPhy.text = "140";
                    break;

                case 8:
                    chargesDewPhy.text = "160";
                    break;

                case 9:
                    chargesDewPhy.text = "200";
                    break;

                case 10:
                    dewinPhyBtn.interactable = false;
                    chargesDewPhy.text = "MAX";
                    break;
            }
        }

        if(Cladd != null)
        {
            switch (Cladd.claddwydPhysic)
            {
                case 1:
                    chargesCladdPhy.text = "20";
                    break;

                case 2:
                    chargesCladdPhy.text = "40";
                    break;

                case 3:
                    chargesCladdPhy.text = "60";
                    break;

                case 4:
                    chargesCladdPhy.text = "80";
                    break;

                case 5:
                    chargesCladdPhy.text = "100";
                    break;

                case 6:
                    chargesCladdPhy.text = "120";
                    break;

                case 7:
                    chargesCladdPhy.text = "140";
                    break;

                case 8:
                    chargesCladdPhy.text = "160";
                    break;

                case 9:
                    chargesCladdPhy.text = "200";
                    break;

                case 10:
                    claddPhyBtn.interactable = false;
                    chargesCladdPhy.text = "MAX";
                    break;
            }
        }
    }

    void InitializeMagCost()
    {
        switch (Ysbrid.magicLevel)
        {
            case 1:
                chargesYsbMag.text = "20";
                break;

            case 2:
                chargesYsbMag.text = "40";
                break;

            case 3:
                chargesYsbMag.text = "60";
                break;

            case 4:
                chargesYsbMag.text = "80";
                break;

            case 5:
                chargesYsbMag.text = "100";
                break;

            case 6:
                chargesYsbMag.text = "120";
                break;

            case 7:
                chargesYsbMag.text = "140";
                break;

            case 8:
                chargesYsbMag.text = "160";
                break;

            case 9:
                chargesYsbMag.text = "200";
                break;

            case 10:
                ysbridMagBtn.interactable = false;
                chargesYsbMag.text = "MAX";
                break;
        }

        if (Dewin != null)
        {
            switch (Dewin.dewinMagic)
            {
                case 1:
                    chargesDewMag.text = "20";
                    break;

                case 2:
                    chargesDewMag.text = "40";
                    break;

                case 3:
                    chargesDewMag.text = "60";
                    break;

                case 4:
                    chargesDewMag.text = "80";
                    break;

                case 5:
                    chargesDewMag.text = "100";
                    break;

                case 6:
                    chargesDewMag.text = "120";
                    break;

                case 7:
                    chargesDewMag.text = "140";
                    break;

                case 8:
                    chargesDewMag.text = "160";
                    break;

                case 9:
                    chargesDewMag.text = "200";
                    break;

                case 10:
                    dewinMagBtn.interactable = false;
                    chargesDewMag.text = "MAX";
                    break;
            }
        }

        if (Cladd != null)
        {
            switch (Cladd.claddwydMagic)
            {
                case 1:
                    chargesCladdMag.text = "20";
                    break;

                case 2:
                    chargesCladdMag.text = "40";
                    break;

                case 3:
                    chargesCladdMag.text = "60";
                    break;

                case 4:
                    chargesCladdMag.text = "80";
                    break;

                case 5:
                    chargesCladdMag.text = "100";
                    break;

                case 6:
                    chargesCladdMag.text = "120";
                    break;

                case 7:
                    chargesCladdMag.text = "140";
                    break;

                case 8:
                    chargesCladdMag.text = "160";
                    break;

                case 9:
                    chargesCladdMag.text = "200";
                    break;

                case 10:
                    claddMagBtn.interactable = false;
                    chargesCladdMag.text = "MAX";
                    break;
            }
        }
    }

    void ManageButtons()
    {
        if(Ysbrid.physicLevel < 10)
        {
            if (Ysbrid.charges <= Int32.Parse(chargesYsbPhy.text))
                ysbridPhyBtn.interactable = false;
        }

        if(Ysbrid.magicLevel < 10)
        {
            if (Ysbrid.charges <= Int32.Parse(chargesYsbMag.text))
                ysbridMagBtn.interactable = false;
        }

        if(Dewin.dewinPhysic < 10)
        {
            if (Ysbrid.charges <= Int32.Parse(chargesDewPhy.text))
                dewinPhyBtn.interactable = false;
        }

        if(Dewin.dewinMagic < 10)
        {
            if (Ysbrid.charges <= Int32.Parse(chargesDewMag.text))
                dewinMagBtn.interactable = false;
        }

        if(Cladd.claddwydPhysic < 10)
        {
            if (Ysbrid.charges <= Int32.Parse(chargesCladdPhy.text))
                claddPhyBtn.interactable = false;
        }

        if(Cladd.claddwydMagic < 10)
        {
            if (Ysbrid.charges <= Int32.Parse(chargesCladdMag.text))
                claddMagBtn.interactable = false;
        }
    }
}
