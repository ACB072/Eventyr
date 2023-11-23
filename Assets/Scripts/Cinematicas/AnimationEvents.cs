using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEvents : MonoBehaviour
{
    public GameObject image;

    public void LoadIntro(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadIntro2(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadMainMenu(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadGameOverScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadGame(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadSaveState()
    {
        CameraMovement Camera = CameraMovement.cameraInstance;
        YsbridManager Ysbrid = YsbridManager.ysbridInstance;

        Camera.Load();
        Ysbrid.Load();

        if (DewinManager.dewinInstance != null)
        {
            DewinManager Dewin = DewinManager.dewinInstance;
            Dewin.Load();
        }

        if (CladdwydManager.claddInstance != null)
        {
            CladdwydManager Cladd = CladdwydManager.claddInstance;
            Cladd.Load();
        }
    }

    public void DisableThisObject()
    {
        image.SetActive(false);
    }
}
