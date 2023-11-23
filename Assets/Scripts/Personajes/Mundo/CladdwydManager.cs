using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class CladdwydManager : MonoBehaviour, PersistentInterface
{
    [HideInInspector]
    public static CladdwydManager claddInstance;

    [HideInInspector]
    public int claddwydHealth, claddwydMana, claddwydLevel, claddwydPhysic, claddwydMagic; // Salud y mana del compañero

    private float claddwydSpeed; // Velocidad del compañero

    [HideInInspector]
    public SpriteRenderer claddwydSpriteRenderer;

    private Animator claddwydAnim; // Animacion de movimiento
    public Transform claddwydTarget; // Objetivo a seguir

    private Vector3 moveVector;

    void Start()
    {
        // Inicializador de atributos
        Initialize();
    }

    void Update()
    {
        PartnerMove();
    }

    void PartnerMove()
    {
        moveVector = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        // Si el compañero está más lejos que el radio de influencia del personaje principal
        if ((claddwydTarget.transform.position - transform.position).sqrMagnitude > 2 * 2)
            transform.position = Vector2.MoveTowards(transform.position, claddwydTarget.position, claddwydSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            claddwydSpeed = 10;
            claddwydAnim.speed = 3;
        }
        else
        {
            claddwydSpeed = 5;
            claddwydAnim.speed = 1;
        }

        // Establece las condiciones de animación
        claddwydAnim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        claddwydAnim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
    }

    void Initialize()
    {
        if (claddInstance == null)
        {
            DontDestroyOnLoad(gameObject);
            claddInstance = this;
        }
        else if (claddInstance != this)
            Destroy(gameObject);

        claddwydSpriteRenderer = GetComponent<SpriteRenderer>();
        claddwydHealth = 100;
        claddwydMana = 100;
        claddwydLevel = 1;
        claddwydPhysic = 1;
        claddwydMagic = 1;
        claddwydSpeed = 5;
        claddwydAnim = GetComponent<Animator>();
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/CladdData.evy");

        AllPersistentData data = new AllPersistentData();

        data.claddHealth_ = claddwydHealth;
        data.claddMana_ = claddwydMana;
        data.claddLevel_ = claddwydLevel;
        data.claddPhysicLevel_ = claddwydPhysic;
        data.claddMagicLevel_ = claddwydMagic;
        data.claddXPosition_ = transform.position.x;
        data.claddYPosition_ = transform.position.y;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/CladdData.evy"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/CladdData.evy", FileMode.Open);
            AllPersistentData data = (AllPersistentData)bf.Deserialize(file);
            file.Close();

            claddwydHealth = data.claddHealth_;
            claddwydMana = data.claddMana_;
            claddwydLevel = data.claddLevel_;
            claddwydPhysic = data.claddPhysicLevel_;
            claddwydMagic = data.claddMagicLevel_;

            transform.position = new Vector3(data.claddXPosition_, data.claddYPosition_, transform.position.z);
        }
    }

    public void DestroyData()
    {
        if (File.Exists(Application.persistentDataPath + "/CladdData.evy"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            File.Delete(Application.persistentDataPath + "/CladdData.evy");
        }
    }
}
