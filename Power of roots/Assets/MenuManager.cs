using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void loadScene(int i) {
        //FMODUnity.RuntimeManager.PauseAllEvents(true);
        SceneManager.LoadScene(i);
    }
}
