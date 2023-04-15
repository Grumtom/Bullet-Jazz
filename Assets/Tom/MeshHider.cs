using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshHider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer myRenderer = GetComponent<MeshRenderer>();
        myRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
