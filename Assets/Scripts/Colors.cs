using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colors : MonoBehaviour
{
    public Color newColor; 
    private Color originalColor; 
    private Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();

        originalColor = renderer.material.color;

        renderer.material.color = newColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
