using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // list of variables
    [SerializeField] float delayInSeconds = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // list of coroutines
    IEnumerator waitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene(2);
    }

    // list of methods
    public void loadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void loadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void gameOverScene()
    {
        StartCoroutine(waitAndLoad());
    }
    //finish the gameOver scene then add some music and game is complete. 

}
/// Notes
/// The gameOver scene has a build index reference of 2. 