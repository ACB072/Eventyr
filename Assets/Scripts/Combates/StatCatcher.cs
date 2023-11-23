using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatCatcher : MonoBehaviour
{
    [HideInInspector]
    public int enemyHealth, enemyLevel, enemyPhysic, enemyMagic, enemyCharges, currentScene;

    [HideInInspector]
    public string enemyName, battleBackground;

    private EnemyManager Enemy;
    public string tago;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy1") { 
            tago = "Enemy1";
            DisplayBattle(tag);
        }
        else if (collision.gameObject.tag == "Enemy2") { 
            tago = "Enemy2";
            DisplayBattle(tag);
        }
        else if (collision.gameObject.tag == "Enemy3") { 
            tago = "Enemy3";
            DisplayBattle(tag);
        }
        else if (collision.gameObject.tag == "Enemy4") { 
            tago = "Enemy4";
            DisplayBattle(tag);
        }
        else if (collision.gameObject.tag == "Enemy5") { 
            tago = "Enemy5";
            DisplayBattle(tag);
        }
        else if (collision.gameObject.tag == "DDD")
        {
            tago = "DDD";
            DisplayBattle(tag);
        }
        else if (collision.gameObject.tag == "DCD")
        {
            tago = "DCD";
            DisplayBattle(tag);
        }
    }

    void DisplayBattle(string tago)
    {
        Enemy = FindObjectOfType<EnemyManager>();

        Debug.Log(tag);
        enemyHealth = Enemy.enemyHealth;
        enemyLevel = Enemy.enemyLevel;
        enemyPhysic = Enemy.enemyPhysic;
        enemyMagic = Enemy.enemyMagic;
        enemyCharges = Enemy.enemyCharges;
        enemyName = Enemy.enemyName;
        currentScene = SceneManager.GetActiveScene().buildIndex;

        Debug.Log("Catch Health: " + enemyHealth);
        Debug.Log("Catch Name: " + enemyName);

        if (SceneManager.GetActiveScene().buildIndex == 4)
            battleBackground = "Aldea";
        if(SceneManager.GetActiveScene().buildIndex == 7 || SceneManager.GetActiveScene().buildIndex == 8)
            battleBackground = "Bosque";
        if (SceneManager.GetActiveScene().buildIndex == 18)
            battleBackground = "Cueva";
        if (SceneManager.GetActiveScene().buildIndex == 20)
            battleBackground = "CuevaProfunda";
        if (SceneManager.GetActiveScene().buildIndex == 21 || SceneManager.GetActiveScene().buildIndex == 22 || SceneManager.GetActiveScene().buildIndex == 23
            || SceneManager.GetActiveScene().buildIndex == 24 || SceneManager.GetActiveScene().buildIndex == 25 || SceneManager.GetActiveScene().buildIndex == 26
            || SceneManager.GetActiveScene().buildIndex == 27 || SceneManager.GetActiveScene().buildIndex == 28 || SceneManager.GetActiveScene().buildIndex == 29
            || SceneManager.GetActiveScene().buildIndex == 30 || SceneManager.GetActiveScene().buildIndex == 31 || SceneManager.GetActiveScene().buildIndex == 32)
            battleBackground = "Mazmorra";
        if (SceneManager.GetActiveScene().buildIndex == 34)
            battleBackground = "Final";

        Debug.Log("Current Scene: " + battleBackground);

        SceneManager.LoadScene(35);
    }
}
