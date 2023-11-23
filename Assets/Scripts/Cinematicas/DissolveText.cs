using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DissolveText : MonoBehaviour
{
    public GameObject textAlan, textAntonio, textMiguel;
    public ParticleSystem particleAlan, particleAntonio, particleMiguel;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndPlay());       
    }

    IEnumerator WaitAndPlay()
    {
        yield return new WaitForSeconds(8f);
        textAlan.SetActive(false);
        particleAlan.Emit(9999);

        yield return new WaitForSeconds(4f);
        textAntonio.SetActive(false);
        particleAntonio.Emit(9999);

        yield return new WaitForSeconds(4f);
        textMiguel.SetActive(false);
        particleMiguel.Emit(9999);

        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(1);
    }
}
