using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DewinManager : MonoBehaviour, PersistentInterface
{
    [HideInInspector]
    public static DewinManager dewinInstance;

    [HideInInspector]
    public int dewinHealth, dewinMana, dewinLevel, dewinPhysic, dewinMagic; // Salud y mana del compañero

    private float dewinSpeed; // Velocidad del compañero

    [HideInInspector]
    public SpriteRenderer dewinSpriteRenderer;

    private Animator dewinAnim; // Animacion de movimiento
    public Transform dewinTarget; // Objetivo a seguir

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
        if ((dewinTarget.transform.position - transform.position).sqrMagnitude > 2 * 2)
            transform.position = Vector2.MoveTowards(transform.position, dewinTarget.position, dewinSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            dewinSpeed = 10;
            dewinAnim.speed = 3;
        }
        else
        {
            dewinSpeed = 5;
            dewinAnim.speed = 1;
        }

        // Establece las condiciones de animación
        dewinAnim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        dewinAnim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
    }

    void Initialize()
    {
        if (dewinInstance == null)
        {
            DontDestroyOnLoad(gameObject);
            dewinInstance = this;
        }
        else if (dewinInstance != this)
            Destroy(gameObject);

        dewinSpriteRenderer = GetComponent<SpriteRenderer>();
        dewinHealth = 100;
        dewinMana = 100;
        dewinLevel = 1;
        dewinPhysic = 1;
        dewinMagic = 1;
        dewinSpeed = 5;
        dewinAnim = GetComponent<Animator>();
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/DewinData.evy");

        AllPersistentData data = new AllPersistentData();

        data.dewinHealth_ = dewinHealth;
        data.dewinMana_ = dewinMana;
        data.dewinLevel_ = dewinLevel;
        data.dewinPhysicLevel_ = dewinPhysic;
        data.dewinMagicLevel_ = dewinMagic;
        data.dewinXPosition_ = transform.position.x;
        data.dewinYPosition_ = transform.position.y;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/DewinData.evy"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/DewinData.evy", FileMode.Open);
            AllPersistentData data = (AllPersistentData)bf.Deserialize(file);
            file.Close();

            dewinHealth = data.dewinHealth_;
            dewinMana = data.dewinMana_;
            dewinLevel = data.dewinLevel_;
            dewinPhysic = data.dewinPhysicLevel_;
            dewinMagic = data.dewinMagicLevel_;

            transform.position = new Vector3(data.dewinXPosition_, data.dewinYPosition_, transform.position.z);

        }
    }

    public void DestroyData()
    {
        if (File.Exists(Application.persistentDataPath + "/DewinData.evy"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            File.Delete(Application.persistentDataPath + "/DewinData.evy");
        }
    }
}
