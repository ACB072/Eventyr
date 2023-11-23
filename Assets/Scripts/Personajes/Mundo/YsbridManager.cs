using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class YsbridManager : MonoBehaviour, PersistentInterface
{
    public static YsbridManager ysbridInstance;
    public GameObject DewinCharacter, CladdCharacter;

    [HideInInspector]
    public int keys, coins, charges, health, mana, level, physicLevel, magicLevel;

    private float velocidad = 3f; // Velocidad del jugador
    private float xInput, yInput; // Ejes de movimiento x y
    private bool isMoving, isDoor, dewinDead, claddwydDead; // Comprobacion de movimiento

    [HideInInspector]
    public bool hasRedGem, hasBlueGem, hasSword, hasTorch;

    [HideInInspector]
    public Vector2 nextSpawn; // Posición donde aparecerá el personaje al cambiar de escena
    
    private DewinManager Dewin;
    private CladdwydManager Cladd;

    [HideInInspector]
    public SpriteRenderer spriteRenderer;

    public  int x; //Enemy Destroyer
    private Rigidbody2D body; // Cuerpo rigido del personaje
    private GameObject sword, sword1, sword2, sword3; // Objeto Espada
    public GameObject torch;
    public BattleManager BM;
    private Animator anim; // Animacion de movimiento
    public Animator noKeyAnimator, bloodPanel;
    public Text txtCoins, txtKeys, txtCharges, txtHealth, txtMana; // Textos del HUD
    public Text txtHealthStat, txtManaStat, txtLevelStat, txtPhysicStat, txtMagicStat;
    public Slider healthBar, manaBar; // Barras de salud y mana en el HUD
    public AudioSource audioSource;
    public AudioClip swordSwing;
    public Canvas statPanel, hud, bloodCanvas;
    private Vector3 moveVector;
    private int death = 1;
    public int ca = 0;
    public int da = 0;
    public int ddd = 0;

    [HideInInspector]
    public bool isDevilDewinDead = false;
    [HideInInspector]
    public bool isDevilCladdDead = false;


    // Start is called before the first frame update
    void Start()
    {
        // Inicializador de atributos
        Initializer();
    }

    // Update is called once per frame
    void Update()
    {
        // Movimiento
        Move();
        
        // Uso de la espada
        UseSword();

        // HUD
        HUDViewer();

        // Comprueba en todo momento si los personajes han muerto
        CheckIfDead();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Puerta")
            isDoor = true;
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
        hud.enabled = true;

        bloodCanvas.enabled = false;

        spriteRenderer.enabled = true;

        if (Dewin != null)
            Dewin.dewinSpriteRenderer.enabled = true;

        if (Cladd != null)
            Cladd.claddwydSpriteRenderer.enabled = true;

        if (isDoor)
        {
            transform.position = nextSpawn;

            if (Dewin != null)
                Dewin.transform.position = nextSpawn;

            if (Cladd != null)
                Cladd.transform.position = nextSpawn;
        }

        isDoor = false;

        if (scene.buildIndex == 5 && death==1)
        {
            Dewin.enabled = false;
            Cladd.enabled = false;
            DewinCharacter.SetActive(false);
            CladdCharacter.SetActive(false);
            da = 1;
            ca = 1; 

            death = 0;
        }

        if (isDevilDewinDead == true)
        {
            Dewin.enabled = true;
            DewinCharacter.SetActive(true);
            Dewin.transform.position = transform.position;
            da = 0;
        }

        if(isDevilCladdDead == true)
        {
            Cladd.enabled = true;
            CladdCharacter.SetActive(true);
            Cladd.transform.position = transform.position;
            ca = 0;
        }

        if (scene.buildIndex == 21 || scene.buildIndex == 22 || (scene.buildIndex == 13 && hasTorch))
            torch.SetActive(true);
        else
            torch.SetActive(false);

        if (scene.buildIndex == 35)
        {
            hud.enabled = false;

            spriteRenderer.enabled = false;

            if (Dewin != null)
                Dewin.dewinSpriteRenderer.enabled = false;

            if (Cladd != null)
                Cladd.claddwydSpriteRenderer.enabled = false;
        }

        if (scene.buildIndex == 39)
            hud.enabled = false;
    }

    void Move()
    {
        // Captura el eje horizontal
        xInput = Input.GetAxisRaw("Horizontal");

        // Captura el eje vertical
        yInput = Input.GetAxisRaw("Vertical");

        // Comprueba si el personaje se esta moviendo
        isMoving = (xInput != 0 || yInput != 0);

        // Si se esta moviendo
        if (isMoving)
        {
            moveVector = new Vector3(xInput, yInput, 0);

            // Mueve el personaje
            body.MovePosition(new Vector2((transform.position.x + moveVector.x * velocidad * Time.deltaTime),
                transform.position.y + moveVector.y * velocidad * Time.deltaTime));
        }

        // Para correr
        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocidad = 10;
            anim.speed = 3;
        }
        else
        {
            velocidad = 5;
            anim.speed = 1;
        }

        // Establece las condiciones de animacion
        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
    }

    void UseSword()
    {
        // Si la espada se ha desbloqueado
        if(hasSword)
        {
            // Si se pulsa la tecla espacio
            if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && anim.GetFloat("MoveY") > 0 || (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && this.anim.GetCurrentAnimatorStateInfo(0).IsName("Player-IdleUp"))
                StartCoroutine(Romper(sword1)); // Lanza la accion de romper
            else if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && anim.GetFloat("MoveY") < 0 || (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && this.anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdle"))
                StartCoroutine(Romper(sword2)); // Lanza la accion de romper
            else if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && anim.GetFloat("MoveX") > 0 || (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && this.anim.GetCurrentAnimatorStateInfo(0).IsName("Player-IdleRight"))
                StartCoroutine(Romper(sword)); // Lanza la accion de romper
            else if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && anim.GetFloat("MoveX") < 0 || (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && this.anim.GetCurrentAnimatorStateInfo(0).IsName("Player-IdleLeft"))
                StartCoroutine(Romper(sword3)); // Lanza la accion de romper

            // Si la espada esta activa
            if (sword.activeInHierarchy)
                StartCoroutine(Ocultar(sword)); // Oculta la espada
            else if (sword1.activeInHierarchy)
                StartCoroutine(Ocultar(sword1)); // Oculta la espada
            else if (sword2.activeInHierarchy)
                StartCoroutine(Ocultar(sword2)); // Oculta la espada
            else if (sword3.activeInHierarchy)
                StartCoroutine(Ocultar(sword3)); // Oculta la espada
        }
    }

    void HUDViewer()
    {
        healthBar.value = health;
        manaBar.value = mana;
        txtHealth.text = health + "%";
        txtMana.text = mana + "%";
        txtCoins.text = coins + "";
        txtKeys.text = keys + "";
        txtCharges.text = charges + "";

        txtHealthStat.text = health + " / " + healthBar.maxValue;
        txtManaStat.text = mana + " / " + manaBar.maxValue;
        txtLevelStat.text = level + "";
        txtPhysicStat.text = physicLevel + "";
        txtMagicStat.text = magicLevel + "";

        if (Input.GetKey(KeyCode.I))
            StartCoroutine(OcultarStats());
    }

    void CheckIfDead()
    {
        if (Dewin != null)
        {
            if (Cladd != null)
            {
                if (health <= 0 && Dewin.dewinHealth <= 0 && Cladd.claddwydHealth <= 0)
                    StartCoroutine(ManageDead());
            }
            else
            {
                if (health <= 0 && Dewin.dewinHealth <= 0)
                    StartCoroutine(ManageDead());
            }
        }
        else
        {
            if (health <= 0)
                StartCoroutine(ManageDead());
        }
    }

    void Initializer()
    {
        if (ysbridInstance == null)
        {
            DontDestroyOnLoad(gameObject);
            ysbridInstance = this;
        }
        else if (ysbridInstance != this)
            Destroy(gameObject);

        healthBar.minValue = 0;
        healthBar.maxValue = 100;
        manaBar.minValue = 0;
        manaBar.maxValue = 100;
        health = 100;
        mana = 100;
        keys = 0;
        coins = 0;
        charges = 0;
        level = 1;
        physicLevel = 1;
        magicLevel = 1;
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (DewinManager.dewinInstance != null)
        {
            Dewin = DewinManager.dewinInstance;
            Debug.Log("Dewin is active");
        }

        Cladd = FindObjectOfType<CladdwydManager>();

        sword = GameObject.FindGameObjectWithTag("Espada");
        sword1 = GameObject.FindGameObjectWithTag("Espada1");
        sword2 = GameObject.FindGameObjectWithTag("Espada2");
        sword3 = GameObject.FindGameObjectWithTag("Espada3");
        sword.SetActive(false);
        sword1.SetActive(false);
        sword2.SetActive(false);
        sword3.SetActive(false);
        isMoving = false;
        hasRedGem = false;
        hasBlueGem = false;
        hasSword = false;
        statPanel.enabled = false;
        isDoor = false;
    }

    IEnumerator Romper(GameObject obj)
    {
        yield return null;
        audioSource.clip = swordSwing;
        audioSource.Play();
        obj.SetActive(true);
    }

    IEnumerator Ocultar(GameObject obj)
    {
        yield return new WaitForSeconds(.1f);
        obj.SetActive(false);
    }

    IEnumerator OcultarStats()
    {
        statPanel.enabled = true;
        yield return new WaitForSeconds(3);
        statPanel.enabled = false;
    }

    IEnumerator ManageDead()
    {
        bloodCanvas.enabled = true;
        bloodCanvas.sortingOrder = 5;
        Time.timeScale = 0.5f;
        bloodPanel.SetBool("Dead", true);
        yield return new WaitForSeconds(5f);
        Time.timeScale = 1f;
        bloodPanel.SetBool("Dead", false);
        bloodCanvas.sortingOrder = 0;
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/YsbridData.evy");

        AllPersistentData data = new AllPersistentData();

        data.ysbridHealth_ = health;
        data.ysbridMana_ = mana;
        data.ysbridCoins_ = coins;
        data.ysbridKeys_ = keys;
        data.ysbridCharges_ = charges;
        data.ysbridLevel_ = level;
        data.ysbridPhysicLevel_ = physicLevel;
        data.ysbridMagicLevel_ = magicLevel;
        data.hasSword_ = hasSword;
        data.hasTorch_ = hasTorch;
        data.ysbridXPosition_ = transform.position.x;
        data.ysbridYPosition_ = transform.position.y;

        data.currentScene_ = SceneManager.GetActiveScene().buildIndex;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/YsbridData.evy"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/YsbridData.evy", FileMode.Open);
            AllPersistentData data = (AllPersistentData)bf.Deserialize(file);
            file.Close();

            health = data.ysbridHealth_;
            mana = data.ysbridMana_;
            coins = data.ysbridCoins_;
            keys = data.ysbridKeys_;
            charges = data.ysbridCharges_;
            level = data.ysbridLevel_;
            physicLevel = data.ysbridPhysicLevel_;
            magicLevel = data.ysbridMagicLevel_;
            hasSword = data.hasSword_;
            hasTorch = data.hasTorch_;

            transform.position = new Vector3(data.ysbridXPosition_, data.ysbridYPosition_, transform.position.z);

            SceneManager.LoadScene(data.currentScene_);
        }
    }

    public void DestroyData()
    {
        if (File.Exists(Application.persistentDataPath + "/YsbridData.evy"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            File.Delete(Application.persistentDataPath + "/YsbridData.evy");
        }
    }
}
