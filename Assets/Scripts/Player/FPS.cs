using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    private void Update()
    {
        float fps = 1.0f / Time.deltaTime;
        GetComponent<Text>().text = "FPS: " + fps;
    }
}
