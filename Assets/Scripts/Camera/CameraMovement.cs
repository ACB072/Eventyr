using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour, PersistentInterface
{
    [HideInInspector]
    public static CameraMovement cameraInstance;                        // Instancia del objeto de la camara

    public Camera mainCamera;
	public Transform targetPlayer;                                      // Objetivo de la camara
    public Material defaultMaterial, darkMaterial;                      // Materiales de los personajes
    private YsbridManager Ysbrid;                                       // Objeto Ysbrid
    private DewinManager Dewin;                                         // Objeto Dewin
    private CladdwydManager Cladd;                                      // Objeto Claddwyd
    /*public AudioSource audioSource;                                     // Fuente de sonido
    public AudioClip menu, aldea, casa, cala, bosque, ciudad,       // Musica de las escenas
        cueva, alcantarilla, mazmorra, contraEnemigo, contraBoss;*/

    void Start()
    {
        if (cameraInstance == null)
        {
            DontDestroyOnLoad(gameObject);
            cameraInstance = this;
        }
        else if (cameraInstance != this)
            Destroy(gameObject);

        Ysbrid = YsbridManager.ysbridInstance;

        if (DewinManager.dewinInstance != null)
            Dewin = DewinManager.dewinInstance;

        if (CladdwydManager.claddInstance != null)
            Cladd = CladdwydManager.claddInstance;

        if (targetPlayer == null)
            targetPlayer = GameObject.FindGameObjectWithTag("Ysbrid").transform;

        /*audioSource.clip = menu;
        audioSource.Play();*/
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        mainCamera.orthographicSize = 7;

        targetPlayer = GameObject.FindGameObjectWithTag("Ysbrid").transform;

        if (scene.buildIndex == 4 || scene.buildIndex == 13 || scene.buildIndex == 18 || scene.buildIndex == 19 || scene.buildIndex == 20 
            || scene.buildIndex == 21 || scene.buildIndex == 22 || scene.buildIndex == 23 || scene.buildIndex == 24 || scene.buildIndex == 25 
            || scene.buildIndex == 26 || scene.buildIndex == 27 || scene.buildIndex == 28 || scene.buildIndex == 29 || scene.buildIndex == 30 
            || scene.buildIndex == 31 || scene.buildIndex == 32 || scene.buildIndex == 33 || scene.buildIndex == 34 || scene.buildIndex == 38)
        {
            Ysbrid.spriteRenderer.material = darkMaterial;

            if (Dewin != null)
                Dewin.dewinSpriteRenderer.material = darkMaterial;

            if (Cladd != null)
                Cladd.claddwydSpriteRenderer.material = darkMaterial;

            if (scene.buildIndex == 24 || scene.buildIndex == 25 || scene.buildIndex == 26 
                || scene.buildIndex == 27 || scene.buildIndex == 28 || scene.buildIndex == 29 
                || scene.buildIndex == 30 || scene.buildIndex == 31)
            {
                targetPlayer = null;
                transform.position = new Vector3(0, 0, -10);
            }
            else
                targetPlayer = GameObject.FindGameObjectWithTag("Ysbrid").transform;

        }
        else
        {
            Ysbrid.spriteRenderer.material = defaultMaterial;

            if (Dewin != null)
                    Dewin.dewinSpriteRenderer.material = defaultMaterial;

            if (Cladd != null)
                    Cladd.claddwydSpriteRenderer.material = defaultMaterial;
        }

        if (scene.buildIndex == 35)
        {
            targetPlayer = GameObject.FindGameObjectWithTag("Center").transform;
            mainCamera.orthographicSize = 5;
        }
    }

    void Update() {
        // La camara sigue al personaje principal constantemente
        if(targetPlayer != null)
		    transform.position = new Vector3(targetPlayer.position.x, targetPlayer.position.y, -10);
	}

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/CameraData.evy");

        AllPersistentData data = new AllPersistentData();

        data.cameraXPosition = transform.position.x;
        data.cameraYPosition = transform.position.y;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/CameraData.evy"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/CameraData.evy", FileMode.Open);
            AllPersistentData data = (AllPersistentData)bf.Deserialize(file);
            file.Close();

            transform.position = new Vector3(data.cameraXPosition, data.cameraYPosition, transform.position.z);
        }
    }

    public void DestroyData()
    {
        if (File.Exists(Application.persistentDataPath + "/CameraData.evy"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            File.Delete(Application.persistentDataPath + "/CameraData.evy");
        }
    }
}