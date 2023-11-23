using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    private static int YSBRID_TURN = 1;                                                          // Constante del turno de Ysbrid
    private static int DEWIN_TURN = 2;                                                           // Constante del turno de Dewin
    private static int CLADD_TURN = 3;                                                           // Constante del turno de Claddwyd
    private static int ENEMY_TURN = 4;                                                           // Constante del turno del enemigo

    private StatCatcher Enemy;                                                                   // Objeto Enemigo
    private YsbridManager Ysbrid;                                                                // Objeto Ysbrid
    private DewinManager Dewin;                                                                  // Objeto Dewin
    private CladdwydManager Cladd;                                                               // Objeto Claddwyd
    private BattleGraphicsManager GFX;

    public GameObject turnForYsbrid, turnForDewin, turnForCladd;                                 // Llamita que indica de quien es el turno
    public GameObject YsbridCharacter, DewinCharacter, CladdCharacter;                           // Objetos de los compañeros en el combate
    public Canvas YUI, DUI, CUI;                                                                 // Panel de interacción de cada personaje;
    public Canvas YHandM, DHandM, CHandM;                                                        // Barras de vida y mana de cada personaje
    private int turn, turn2;                                                                     // Turno

    private float posCladdX, posCladdY;                                                          // Posición de Claddwyd (Ataques físicos)

    public Animator YsbridAttack, DewinAttack, CladAttack;                                       // Animadores de los personajes
    public Text Log;                                                                             // Mensajes del combate

    public int x;

    public Sprite ysbrid_wounded, dewin_wounded, cladd_wounded;
    //private bool ysbrid_dead, dewin_dead, cladd_dead;

    void Start()
    {
        BattleInitializer();
    }

    void Update()
    {
        // Comprueba si se ha ganado o se ha perdido el combate
        ManageCases();
    }

    IEnumerator EnemyAtk()
    {
        Debug.Log("Enemy Attacks");

        RandomEnemyChoice();

        yield return new WaitForSeconds(1.5f);

        GFX.EnemyCharacter.GetComponent<Animator>().SetBool("EnemyAttack", false);

        GFX.EnemyCharacter.transform.position = new Vector2(GFX.posEnemyX, GFX.posEnemyY);

        if (Ysbrid.health <= 0)
        {
            YsbridCharacter.GetComponent<Animator>().runtimeAnimatorController = null;
            YsbridCharacter.GetComponent<SpriteRenderer>().sprite = ysbrid_wounded;
           // ysbrid_dead = true;
        }

        if(Dewin != null)
        {
            if (Dewin.dewinHealth <= 0)
            {
                DewinCharacter.GetComponent<Animator>().runtimeAnimatorController = null;
                DewinCharacter.GetComponent<SpriteRenderer>().sprite = dewin_wounded;
                // dewin_dead = true;
            }
        }

        if(Cladd != null)
        {
            if (Cladd.claddwydHealth <= 0)
            {
                CladdCharacter.GetComponent<Animator>().runtimeAnimatorController = null;
                CladdCharacter.GetComponent<SpriteRenderer>().sprite = cladd_wounded;
                // cladd_dead = true;
            }
        }
        
        if (turn2 == YSBRID_TURN)
        {
            if (Dewin != null)
            {
                Debug.Log("Turn for Dewin");

                //Se le añade el fueguecillo a Dewin
                turnForDewin.SetActive(true);

                //Activamos la UI de Dewin
                DUI.enabled = !DUI.enabled;
                DHandM.enabled = !DHandM.enabled;

                //Se pasa al turno de Dewin
                turn = DEWIN_TURN;
            }
            else if (Cladd != null)
            {
                Debug.Log("Turn for Cladd");

                turnForCladd.SetActive(true);

                CUI.enabled = !CUI.enabled;
                CHandM.enabled = !CHandM.enabled;

                turn = CLADD_TURN;
            }
            else
            {
                turnForYsbrid.SetActive(true);

                YUI.enabled = !YUI.enabled;
                YHandM.enabled = !YHandM.enabled;

                turn = YSBRID_TURN;
            }

        }
        else if (turn2 == DEWIN_TURN)
        {
            if (Cladd != null)
            {
                turnForCladd.SetActive(true);

                CUI.enabled = !CUI.enabled;
                CHandM.enabled = !CHandM.enabled;

                turn = CLADD_TURN;
            }
            else
            {
                turnForYsbrid.SetActive(true);

                YUI.enabled = !YUI.enabled;
                YHandM.enabled = !YHandM.enabled;

                turn = YSBRID_TURN;
            }

        }
        else if (turn2 == CLADD_TURN)
        {
            turnForYsbrid.SetActive(true);

            YUI.enabled = !YUI.enabled;
            YHandM.enabled = !YHandM.enabled;

            turn = YSBRID_TURN;
        }
    }

    IEnumerator Attack()
    {
        if (turn == YSBRID_TURN)
        {
            Debug.Log("Turn for Ysbrid");

            //Escondemos la UI de Ysbrid
            YUI.enabled = !YUI.enabled;
            YHandM.enabled = !YHandM.enabled;

            Debug.Log("Ysbrid Attacks");

            //Activamos la animacion del ataque
            YsbridAttack.SetBool("YBlock", false);
            YsbridAttack.SetBool("YAttack", true);

            //Se espera para que la animacion pueda acabar
            yield return new WaitForSeconds(1);

            //Termina el ataque
            YsbridAttack.SetBool("YAttack", false);

            // Resta la salud al enemigo
            Enemy.enemyHealth -= 10 + Ysbrid.physicLevel;
            Type("Ysbrid ataca fisicamente y resta " + (10 + Ysbrid.physicLevel));
            Debug.Log("Ysbrid ataca fisicamente y resta " + (10 + Ysbrid.physicLevel));
            Debug.Log("Vida del enemigo " + Enemy.enemyHealth);

            yield return new WaitForSeconds(2);

            //Se le quita el fueguecillo a Ysbrid
            turnForYsbrid.SetActive(false);
            turn2 = YSBRID_TURN;
            Debug.Log("Turn for Enemy");

            turn = ENEMY_TURN;

            if (turn == ENEMY_TURN)
                StartCoroutine(EnemyAtk());
        }
        else if (turn == DEWIN_TURN)
        {
            DUI.enabled = !DUI.enabled;
            DHandM.enabled = !DHandM.enabled;

            DewinAttack.SetBool("DBlock", false);
            DewinAttack.SetBool("DAttack", true);

            yield return new WaitForSeconds(1);

            Debug.Log("Dewin Attacks");

            DewinAttack.SetBool("DAttack", false);

            Enemy.enemyHealth -= 5 + Dewin.dewinPhysic;
            Type("Dewin ataca fisicamente y resta " + (5 + Dewin.dewinPhysic));
            Debug.Log("Dewin ataca fisicamente y resta " + (5 + Dewin.dewinPhysic));
            Debug.Log("Vida del enemigo " + Enemy.enemyHealth);

            yield return new WaitForSeconds(2);

            turnForDewin.SetActive(false);
            turn2 = DEWIN_TURN;
            Debug.Log("Turn for Enemy");

            turn = ENEMY_TURN;
            if (turn == ENEMY_TURN)
                StartCoroutine(EnemyAtk());
        }
        else if (turn == CLADD_TURN)
        {
            CUI.enabled = !CUI.enabled;
            CHandM.enabled = !CHandM.enabled;

            Debug.Log("Cladd Attacks");

            CladAttack.SetBool("CBlock", false);
            CladAttack.SetBool("CAttack", true);

            CladdCharacter.transform.position = new Vector2(GFX.EnemyCharacter.transform.position.x - 3, CladdCharacter.transform.position.y);

            yield return new WaitForSeconds(2);

            CladdCharacter.transform.position = new Vector2(posCladdX, posCladdY);

            CladAttack.SetBool("CAttack", false);

            Enemy.enemyHealth -= 15 + Cladd.claddwydPhysic;
            Type("Claddwyd ataca fisicamente y resta " + (15 + Cladd.claddwydPhysic));
            Debug.Log("Claddwyd ataca fisicamente y resta " + (15 + Cladd.claddwydPhysic));
            Debug.Log("Vida del enemigo " + Enemy.enemyHealth);

            yield return new WaitForSeconds(1);

            turnForCladd.SetActive(false);

            turn2 = CLADD_TURN;
            Debug.Log("Turn for Enemy");

            turn = ENEMY_TURN;
            if (turn == ENEMY_TURN)
                StartCoroutine(EnemyAtk());
        }
    }

    IEnumerator Magic()
    {
        if (turn == YSBRID_TURN)
        {
            Debug.Log("Turn for Ysbrid");

            //Escondemos la UI de Ysbrid
            YUI.enabled = !YUI.enabled;
            YHandM.enabled = !YHandM.enabled;

            Debug.Log("Ysbrid Attacks");

            //Activamos la animacion del ataque
            YsbridAttack.SetBool("YBlock", false);
            YsbridAttack.SetBool("YAttack", true);

            //Se espera para que la animacion pueda acabar
            yield return new WaitForSeconds(1);

            //Termina el ataque
            YsbridAttack.SetBool("YAttack", false);

            // Resta la salud al enemigo
            Enemy.enemyHealth -= 10 + Ysbrid.magicLevel;
            Debug.Log("Ysbrid ataca fisicamente y resta " + (10 + Ysbrid.magicLevel));
            Debug.Log("Vida del enemigo " + Enemy.enemyHealth);

            yield return new WaitForSeconds(2);

            //Se le quita el fueguecillo a Ysbrid
            turnForYsbrid.SetActive(false);
            turn2 = YSBRID_TURN;
            Debug.Log("Turn for Enemy");

            turn = ENEMY_TURN;

            if (turn == ENEMY_TURN)
                StartCoroutine(EnemyAtk());
        }
        else if (turn == DEWIN_TURN)
        {
            DUI.enabled = !DUI.enabled;
            DHandM.enabled = !DHandM.enabled;

            DewinAttack.SetBool("DBlock", false);
            DewinAttack.SetBool("DAttack", true);

            yield return new WaitForSeconds(1);

            Debug.Log("Dewin Attacks");

            DewinAttack.SetBool("DAttack", false);

            Enemy.enemyHealth -= 5 + Dewin.dewinMagic;
            Debug.Log("Dewin ataca fisicamente y resta " + (5 + Dewin.dewinMagic));
            Debug.Log("Vida del enemigo " + Enemy.enemyHealth);

            yield return new WaitForSeconds(2);

            turnForDewin.SetActive(false);
            turn2 = DEWIN_TURN;
            Debug.Log("Turn for Enemy");

            turn = ENEMY_TURN;

            if (turn == ENEMY_TURN)
                StartCoroutine(EnemyAtk());
        }
        else if (turn == CLADD_TURN)
        {
            CUI.enabled = !CUI.enabled;
            CHandM.enabled = !CHandM.enabled;

            Debug.Log("Cladd Attacks");

            CladAttack.SetBool("CBlock", false);
            CladAttack.SetBool("CAttack", true);

            CladdCharacter.transform.position = new Vector2(GFX.EnemyCharacter.transform.position.x - 3, CladdCharacter.transform.position.y);

            yield return new WaitForSeconds(2);

            CladdCharacter.transform.position = new Vector2(posCladdX, posCladdY);

            CladAttack.SetBool("CAttack", false);

            Enemy.enemyHealth -= 15 + Cladd.claddwydMagic;
            Debug.Log("Claddwyd ataca fisicamente y resta " + (15 + Cladd.claddwydMagic));
            Debug.Log("Vida del enemigo " + Enemy.enemyHealth);

            yield return new WaitForSeconds(1);

            turnForCladd.SetActive(false);

            turn2 = CLADD_TURN;
            Debug.Log("Turn for Enemy");

            turn = ENEMY_TURN;

            if (turn == ENEMY_TURN)
                StartCoroutine(EnemyAtk());
        }
    }

    IEnumerator Block()
    {
        if (turn == YSBRID_TURN)
        {
            Debug.Log("Turn for Ysbrid");

            //Escondemos la UI de Ysbrid
            YUI.enabled = !YUI.enabled;
            YHandM.enabled = !YHandM.enabled;

            Debug.Log("Ysbrid Defend");

            //Activamos la animacion del defensa
            YsbridAttack.SetBool("YBlock", true);

            //Se espera para que la animacion pueda acabar
            yield return new WaitForSeconds(1);


            //Se le quita el fueguecillo a Ysbrid
            turnForYsbrid.SetActive(false);
            turn2 = YSBRID_TURN;
            Debug.Log("Turn for Enemy");

            turn = ENEMY_TURN;

            if (turn == ENEMY_TURN)
                StartCoroutine(EnemyAtk());
        }
        else if (turn == DEWIN_TURN)
        {
            DUI.enabled = !DUI.enabled;
            DHandM.enabled = !DHandM.enabled;

            DewinAttack.SetBool("DBlock", true);

            Debug.Log("Dewin Defend");

            yield return new WaitForSeconds(2);

            turnForDewin.SetActive(false);
            turn2 = DEWIN_TURN;
            Debug.Log("Turn for Enemy");

            turn = ENEMY_TURN;

            if (turn == ENEMY_TURN)
                StartCoroutine(EnemyAtk());
        }
        else if (turn == CLADD_TURN)
        {

            CUI.enabled = !CUI.enabled;
            CHandM.enabled = !CHandM.enabled;

            Debug.Log("Cladd Defend");

            CladAttack.SetBool("CBlock", true);

            yield return new WaitForSeconds(2);

            turnForCladd.SetActive(false);

            turn2 = CLADD_TURN;
            Debug.Log("Turn for Enemy");

            turn = ENEMY_TURN;

            if (turn == ENEMY_TURN)
                StartCoroutine(EnemyAtk());
        }
    }

    public void PhysicAttack()
    {
        StopAllCoroutines();

        StartCoroutine(Attack());
    }

    public void MagicAttack()
    {
        StopAllCoroutines();

        StartCoroutine(Magic());
    }

    public void BlockAction()
    {
        StopAllCoroutines();

        StartCoroutine(Block());
    }

    public void Escape()
    {
        StopAllCoroutines();

        if (Enemy.enemyName == "devilDewin" || Enemy.enemyName == "devilCladd" || Enemy.enemyName == "Guardian" || Enemy.enemyName == "Hodoo")
            Debug.Log("No puedes escapar");
        /*else
        {
            if (Ysbrid.level > Enemy.enemyLevel)
            {
                // Huye
                // Carga la anterior escena con la vida y el maná como se quedaron en el combate
                Debug.Log("Me piro");
            }
            else if (Ysbrid.level == Enemy.enemyLevel)
            {
                // Cara o cruz
                // Si no puedes, dar el turno al enemigo
                // Si puedes, carga la anterior escena con la vida y el maná como se quedaron en el combate
                int caraCruz = Random.Range(0, 2);

                if (caraCruz == 1)
                    Debug.Log("Me piro");
                else
                    StartCoroutine(EnemyAtk());
            }
            else
                Debug.Log("El enemigo es demasiado fuerte");
        }*/
    }

    private void ManageCases()
    {
        if (PlayersWin())
        {
            // Otorga las cargas correspondientes
            Ysbrid.charges += Enemy.enemyCharges;

            Debug.Log("Win!");
            if (Enemy.tago == "Enemy1")
            {
                x = 1;
                Ysbrid.x = x;

            }
            else if (Enemy.tago == "Enemy2")
            {
                x = 2;
                Ysbrid.x = x;

            }
            else if (Enemy.tago == "Enemy3")
            {
                x = 3;
                Ysbrid.x = x;

            }
            else if (Enemy.tago == "Enemy4")
            {
                x = 4;
                Ysbrid.x = x;

            }
            else if (Enemy.tago == "Enemy5")
            {
                x = 5;
                Ysbrid.x = x;

            }
            else if (Enemy.tago == "DDD")
                Ysbrid.isDevilDewinDead = true;
            else if (Enemy.tago == "DCD")
                Ysbrid.isDevilCladdDead = true;

            // Carga la anterior escena
            SceneManager.LoadScene(Enemy.currentScene);
        }

        if (PlayersLose())
        {
            // Carga el anterior punto guardado
            Debug.Log("Lose!");
            Ysbrid.Load();
        }
    }

    private bool PlayersWin() { return Enemy.enemyHealth <= 0; }

    private bool PlayersLose() { return Ysbrid.health <= 0 && Dewin.dewinHealth <= 0 && Cladd.claddwydHealth <= 0; }

    private void RandomEnemyChoice()
    {
        int rand = Random.Range(0, 2);

        Debug.Log("choice: " + rand);

        if (rand == 0)
            RandomEnemyPhysicAttack();
        else
            RandomEnemyMagicAttack();
    }

    private void RandomEnemyPhysicAttack()
    {
        int rand, players = 1;

        if (Dewin != null)
            players++;

        if (Cladd != null)
            players++;

        rand = Random.Range(0, players);

        if (rand == 0)
        {
            GFX.EnemyCharacter.transform.position = new Vector2(YsbridCharacter.transform.position.x + 5, GFX.EnemyCharacter.transform.position.y);
            GFX.EnemyCharacter.GetComponent<Animator>().SetBool("EnemyAttack", true);
            Ysbrid.health -= 10 + Enemy.enemyPhysic;
            YsbridAttack.SetBool("YHurt", true);
            // Corrutina
            Debug.Log("Enemigo ataca fisicamente a Ysbrid y le resta " + (10 + Enemy.enemyPhysic));
            Type("Enemigo ataca fisicamente a Ysbrid y le resta " + (10 + Enemy.enemyPhysic));
        }

        else if (rand == 1)
        {
            GFX.EnemyCharacter.transform.position = new Vector2(DewinCharacter.transform.position.x + 5, GFX.EnemyCharacter.transform.position.y + 1);
            GFX.EnemyCharacter.GetComponent<Animator>().SetBool("EnemyAttack", true);
            Dewin.dewinHealth -= 10 + Enemy.enemyPhysic;
            Debug.Log("Enemigo ataca fisicamente a Dewin y le resta " + (10 + Enemy.enemyPhysic));
            Type("Enemigo ataca fisicamente a Dewin y le resta " + (10 + Enemy.enemyPhysic));
        }

        else
        {
            GFX.EnemyCharacter.transform.position = new Vector2(CladdCharacter.transform.position.x + 5, GFX.EnemyCharacter.transform.position.y - 1);
            GFX.EnemyCharacter.GetComponent<Animator>().SetBool("EnemyAttack", true);
            Cladd.claddwydHealth -= 10 + Enemy.enemyPhysic;
            Debug.Log("Enemigo ataca fisicamente a Cladd y le resta " + (10 + Enemy.enemyPhysic));
            Type("Enemigo ataca fisicamente a Cladd y le resta " + (10 + Enemy.enemyPhysic));
        }

    }

    private void RandomEnemyMagicAttack()
    {
        int rand, players = 1;

        if (Dewin != null)
            players++;

        if (Cladd != null)
            players++;

        rand = Random.Range(0, players);

        if (rand == 0)
        {
            Ysbrid.health -= 10 + Enemy.enemyMagic;
            YsbridAttack.SetBool("YHurt", true);
            // Corrutina
            Debug.Log("Enemigo ataca magicamente a Ysbrid y le resta " + (10 + Enemy.enemyMagic));
            Type("Enemigo ataca magicamente a Ysbrid y le resta " + (10 + Enemy.enemyMagic));
        }

        else if (rand == 1)
        {
            Dewin.dewinHealth -= 10 + Enemy.enemyMagic;
            Debug.Log("Enemigo ataca magicamente a Dewin y le resta " + (10 + Enemy.enemyMagic));
            Type("Enemigo ataca magicamente a Dewin y le resta " + (10 + Enemy.enemyMagic));
        }

        else
        {
            Cladd.claddwydHealth -= 10 + Enemy.enemyMagic;
            Debug.Log("Enemigo ataca magicamente a Cladd y le resta " + (10 + Enemy.enemyMagic));
            Type("Enemigo ataca magicamente a Cladd y le resta " + (10 + Enemy.enemyMagic));
        }
    }

    void BattleInitializer()
    {
        Ysbrid = YsbridManager.ysbridInstance;
        Enemy = FindObjectOfType<StatCatcher>();
        GFX = FindObjectOfType<BattleGraphicsManager>();
        Debug.Log(Enemy.tago);

        // ysbrid_dead = false;
        GFX.EnemyCharacter.gameObject.tag = Enemy.tago;

        // Si Dewin esta activo
        if (Ysbrid.da!=1)
        {
            DUI.enabled = !DUI.enabled;
            DHandM.enabled = !DHandM.enabled;
            Dewin = DewinManager.dewinInstance;
            Debug.Log("Dewin is active");
          //  dewin_dead = false;
        }
        else
            DewinCharacter.SetActive(false);

        // Si Claddwyd esta activo
        if (Ysbrid.ca != 1)
        {
            CUI.enabled = !CUI.enabled;
            CHandM.enabled = !CHandM.enabled;
            Cladd = CladdwydManager.claddInstance;
            posCladdX = CladdCharacter.transform.position.x;
            posCladdY = CladdCharacter.transform.position.y;
            Debug.Log("Claddwyd is active");
           // cladd_dead = false;
        }
        else
            CladdCharacter.SetActive(false);

        turn = YSBRID_TURN;
    }

    void Type(string msg)
    {
        StartCoroutine(TypeCoroutine(msg));
    }

    IEnumerator TypeCoroutine(string msg)
    {
        Log.text = "";

        foreach (char letter in msg)
        {
            Log.text += letter;
            yield return null;
        }

        yield return new WaitForSeconds(2.5f);
    }
}