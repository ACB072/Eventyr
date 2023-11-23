using UnityEngine;

public class BattleGraphicsManager : MonoBehaviour
{
    private StatCatcher Enemy;
    public GameObject EnemyCharacter;

    public RuntimeAnimatorController goblinCont, batCont, golemCont, skeletonCont,
                                        devilDewinCont, devilCladdCont, guardianCont, hodooCont;  // Animator Controllers de los enemigos

    public GameObject background;                                                                 // Renderizador de sprites del fondo y del enemigo
    public Sprite village, forest, cave, deepCave, dungeon, endgame;                              // Fondos de cada escena de combate
    public Sprite goblinSprite, batSprite, rockGolemSprite, iceGolemSprite, fireGolemSprite,      // Sprites de los enemigos
                    skeletonSprite, devilDewinSprite, devilCladdSprite,
                    guardianSprite, hodooSprite;

    public float posEnemyX, posEnemyY;

    // Start is called before the first frame update
    void Start()
    {
        Enemy = FindObjectOfType<StatCatcher>();

        SpriteInitializer();
    }

    void SpriteInitializer()
    {
        switch (Enemy.battleBackground)
        {
            case "Aldea":
                background.GetComponent<SpriteRenderer>().sprite = village;
                background.GetComponent<Transform>().position = new Vector2(0.14f, 3.05f);
                background.GetComponent<Transform>().localScale = new Vector2(1.5f, 1.8f);
                break;

            case "Bosque":
                background.GetComponent<SpriteRenderer>().sprite = forest;
                background.GetComponent<Transform>().position = new Vector2(0, 2);
                background.GetComponent<Transform>().localScale = new Vector2(2.56f, 2.15f);
                break;

            case "Cueva":
                background.GetComponent<SpriteRenderer>().sprite = cave;
                background.GetComponent<Transform>().position = new Vector2(0, 3.6f);
                background.GetComponent<Transform>().localScale = new Vector2(2.23f, 2f);
                break;

            case "CuevaProfunda":
                background.GetComponent<SpriteRenderer>().sprite = deepCave;
                background.GetComponent<Transform>().position = new Vector2(0, 5.4f);
                background.GetComponent<Transform>().localScale = new Vector2(3, 4);
                break;

            case "Mazmorra":
                background.GetComponent<SpriteRenderer>().sprite = dungeon;
                background.GetComponent<Transform>().position = new Vector2(0, 2);
                background.GetComponent<Transform>().localScale = new Vector2(3, 2.5f);
                break;

            case "Final":
                background.GetComponent<SpriteRenderer>().sprite = endgame;
                background.GetComponent<Transform>().position = new Vector2(0, 4.6f);
                background.GetComponent<Transform>().localScale = new Vector2(2, 2);
                break;
        }

        switch (Enemy.enemyName)
        {
            case "Goblin":
                EnemyCharacter.GetComponent<SpriteRenderer>().sprite = goblinSprite;
                EnemyCharacter.GetComponent<Animator>().runtimeAnimatorController = goblinCont;
                EnemyCharacter.transform.localScale = new Vector2(2f, 1.7f);
                EnemyCharacter.transform.position = new Vector2(4f, -2.8f);
                posEnemyX = 4;
                posEnemyY = -2.8f;
                break;

            case "Bat":
                EnemyCharacter.GetComponent<SpriteRenderer>().sprite = batSprite;
                EnemyCharacter.GetComponent<Animator>().runtimeAnimatorController = batCont;
                EnemyCharacter.transform.localScale = new Vector2(3, 2.5f);
                EnemyCharacter.transform.position = new Vector2(5.75f, -1.4f);
                posEnemyX = 5.75f;
                posEnemyY = -1.4f;
                break;

            case "RockGolem":
                EnemyCharacter.GetComponent<SpriteRenderer>().sprite = rockGolemSprite;
                EnemyCharacter.GetComponent<Animator>().runtimeAnimatorController = golemCont;
                EnemyCharacter.transform.localScale = new Vector2(2.6f, 2.45f);
                EnemyCharacter.transform.position = new Vector2(6.17f, -1.2f);
                posEnemyX = 6.17f;
                posEnemyY = -1.2f;
                break;

            case "IceGolem":
                EnemyCharacter.GetComponent<SpriteRenderer>().sprite = iceGolemSprite;
                EnemyCharacter.GetComponent<Animator>().runtimeAnimatorController = golemCont;
                EnemyCharacter.transform.localScale = new Vector2(2.6f, 2.45f);
                EnemyCharacter.transform.position = new Vector2(6.17f, -1.2f);
                posEnemyX = 6.17f;
                posEnemyY = -1.2f;
                break;

            case "FireGolem":
                EnemyCharacter.GetComponent<SpriteRenderer>().sprite = fireGolemSprite;
                EnemyCharacter.GetComponent<Animator>().runtimeAnimatorController = golemCont;
                EnemyCharacter.transform.localScale = new Vector2(2.6f, 2.45f);
                EnemyCharacter.transform.position = new Vector2(6.17f, -1.2f);
                posEnemyX = 6.17f;
                posEnemyY = -1.2f;
                break;

            case "Skeleton":
                EnemyCharacter.GetComponent<SpriteRenderer>().sprite = skeletonSprite;
                EnemyCharacter.GetComponent<Animator>().runtimeAnimatorController = skeletonCont;
                EnemyCharacter.transform.localScale = new Vector2(3.2f, 3f);
                EnemyCharacter.transform.position = new Vector2(5.12f, -1.5f);
                posEnemyX = 5.12f;
                posEnemyY = -1.5f;
                break;

            case "DevilDewin":
                EnemyCharacter.GetComponent<SpriteRenderer>().sprite = devilDewinSprite;
                EnemyCharacter.GetComponent<Animator>().runtimeAnimatorController = devilDewinCont;
                EnemyCharacter.transform.localScale = new Vector2(1, 1);
                EnemyCharacter.transform.position = new Vector2(6.2f, -2);
                posEnemyX = 6.2f;
                posEnemyY = -2f;
                break;

            case "DevilCladd":
                EnemyCharacter.GetComponent<SpriteRenderer>().sprite = devilCladdSprite;
                EnemyCharacter.GetComponent<Animator>().runtimeAnimatorController = devilCladdCont;
                EnemyCharacter.transform.localScale = new Vector2(4.3f, 4.3f);
                EnemyCharacter.transform.position = new Vector2(5.57f, -2.1f);
                posEnemyX = 5.57f;
                posEnemyY = -2.1f;
                break;

            case "Guardian":
                EnemyCharacter.GetComponent<SpriteRenderer>().sprite = guardianSprite;
                EnemyCharacter.GetComponent<Animator>().runtimeAnimatorController = guardianCont;
                EnemyCharacter.transform.localScale = new Vector2(3.2f, 3f);
                EnemyCharacter.transform.position = new Vector2(4.8f, -1);
                posEnemyX = 4.8f;
                posEnemyY = -1;
                break;

            case "Hodoo":
                EnemyCharacter.GetComponent<SpriteRenderer>().sprite = hodooSprite;
                EnemyCharacter.GetComponent<Animator>().runtimeAnimatorController = hodooCont;
                EnemyCharacter.transform.localScale = new Vector2(4.3f, 4.13f);
                EnemyCharacter.transform.position = new Vector2(4.3f, 0);
                posEnemyX = 4.3f;
                posEnemyY = 0;
                break;
        }
    }
}
