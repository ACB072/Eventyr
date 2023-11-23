using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenuGUI : MonoBehaviour
{
    public Image image;                 // Imagen de fade
    public Animator anim;               // Animacion de fade
    public Button loadButton;           // Boton Cargar

    private CameraMovement Camera;      // Objeto Camara
    private YsbridManager Ysbrid;       // Objeto Ysbrid
    private DewinManager Dewin;         // Objeto Dewin
    private CladdwydManager Cladd;      // Objeto Claddwyd

    void Start()
    {
        Camera = CameraMovement.cameraInstance;
        Ysbrid = YsbridManager.ysbridInstance;

        if (DewinManager.dewinInstance != null)
            Dewin = DewinManager.dewinInstance;

        if (CladdwydManager.claddInstance != null)
            Cladd = CladdwydManager.claddInstance;

        if (!File.Exists(Application.persistentDataPath + "/YsbridData.evy"))
            loadButton.interactable = false;
    }

    public void NewGame(int scene)
    {
        Debug.Log("Nueva Partida");

        FillValues();

        Ysbrid.DestroyData();

        if (Dewin != null)
            Dewin.DestroyData();

        if (Cladd != null)
            Cladd.DestroyData();

        StartCoroutine(Transition(scene));
    }

    public void LoadGame(int scene)
    {
        Debug.Log("Cargando Partida");

        StartCoroutine(Transition(scene));
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo");
        Application.Quit();
    }

    IEnumerator Transition(int scene)
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => image.color.a == 1);
        Ysbrid.transform.position = new Vector3(0, -10, -1);
        SceneManager.LoadScene(scene);
    }

    void FillValues()
    {
        Ysbrid.healthBar.minValue = 0;
        Ysbrid.healthBar.maxValue = 100;
        Ysbrid.manaBar.minValue = 0;
        Ysbrid.manaBar.maxValue = 100;
        Ysbrid.health = 100;
        Ysbrid.mana = 100;
        Ysbrid.coins = 0;
        Ysbrid.keys = 0;
        Ysbrid.charges = 0;

        if(Dewin != null)
        {
            Dewin.dewinHealth = 100;
            Dewin.dewinMana = 100;
            Dewin.dewinPhysic = 1;
            Dewin.dewinMagic = 1;
        }

        if(Cladd != null)
        {
            Cladd.claddwydHealth = 100;
            Cladd.claddwydMana = 100;
            Cladd.claddwydPhysic = 1;
            Cladd.claddwydMagic = 1;
        }
    }
}
