using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointManager : MonoBehaviour
{
    public ParticleSystem particle;     // Sistema de partículas
    public Light lantern;               // Luz del farol
    public AudioSource audioSource;     // Fuente de sonido
    public AudioClip saveSound;         // Sonido al guardar
    private bool isSaved;               // Comprueba si se ha guardado la partida

    private CameraMovement Camera;      // Objeto camara
    private YsbridManager Ysbrid;       // Objeto Ysbrid
    private DewinManager Dewin;         // Objeto Dewin
    private CladdwydManager Cladd;      // Objeto Claddwyd

    // Start is called before the first frame update
    void Start()
    {
        particle.Stop();
        lantern.enabled = false;
        isSaved = false;
        Camera = CameraMovement.cameraInstance;
        Ysbrid = YsbridManager.ysbridInstance;

        if(DewinManager.dewinInstance != null)
            Dewin = DewinManager.dewinInstance;

        if(CladdwydManager.claddInstance != null)
            Cladd = CladdwydManager.claddInstance;
    }

    // Update is called once per frame
    void Update()
    {
        // Si Ysbrid entra en el radio de influencia del objeto
        if ((Ysbrid.transform.position - transform.position).sqrMagnitude < 2.5 * 2.5)
        {
            if(!isSaved)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    EnableCheckpoint();
                    isSaved = true;
                }
            }       
        }
    }

    void EnableCheckpoint()
    {
        particle.Play();
        lantern.enabled = true;
        SaveAllData();
        audioSource.clip = saveSound;
        audioSource.Play();
    }

    void SaveAllData()
    {
        Camera.Save();
        Ysbrid.Save();

        if(Dewin != null)
            Dewin.Save();

        if(Cladd != null)
            Cladd.Save();
    }
}
