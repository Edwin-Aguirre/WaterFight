using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoNotDestroyMusic : MonoBehaviour
{
    [SerializeField]
    private List<string> sceneNames;
    [SerializeField]
    private string instanceName;

    private void Start() 
    {
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckForDuplicateInstances();

        CheckIfSceneInList();
    }

    void CheckForDuplicateInstances()
    {
        DoNotDestroyMusic[] collection = FindObjectsOfType<DoNotDestroyMusic>();

        foreach(DoNotDestroyMusic obj in collection)
        {
            if(obj != this)
            {
                if(obj.instanceName == instanceName)
                {
                    DestroyImmediate(obj.gameObject);
                }
            }
        }
    }

    void CheckIfSceneInList()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if(sceneNames.Contains(currentScene))
        {

        }
        else
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            DestroyImmediate(this.gameObject);
        }
    }
}
