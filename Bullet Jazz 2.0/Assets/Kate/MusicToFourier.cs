using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicToFourier : MonoBehaviour
{
    [SerializeField] private GameObject bar;
    [SerializeField] private AnalysisRunning analysis;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 1024; i++)
        {
            GameObject Nube = Instantiate(bar, transform);
            Nube.transform.position += Vector3.right * (0.1f * i);
            Nube.GetComponent<Rigidbody2D>().gravityScale = 0;
            Nube.name = "Fourier Band " + i*23.4375f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 1024; i++)
        {
            transform.GetChild(i).localScale = new Vector3 (1,analysis.prevWorkingSamples[i]*500,1);
        }
    }
}
