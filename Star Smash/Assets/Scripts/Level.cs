using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [Header("Number of Blocks in Scene")]
    // parameters
    public int breakableBlocks;

    [Header("Scene Loader Object")]
    //cached reference
    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void CountBlocks()
    {
        breakableBlocks++;
    }

    public void BlockDestroyed()
    {
        breakableBlocks--;
        if (breakableBlocks <= 0 && sceneLoader.lastLevel)
        {
            sceneLoader.LoadCongratsScene();
        }
        else if(breakableBlocks <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
}
