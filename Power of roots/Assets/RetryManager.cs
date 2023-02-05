using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RetryManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private RoundsController rc;
    // Start is called before the first frame update
    void Start()
    {
        rc = FindObjectOfType<RoundsController>();
        text.text = "You survived " + rc.roundCounter + " rounds";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene(1);
        }
    }
}
