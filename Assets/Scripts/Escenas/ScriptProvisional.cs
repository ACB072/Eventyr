using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptProvisional : MonoBehaviour
{
    public int level;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ysbrid")
            SceneManager.LoadScene(level);
    }
}
