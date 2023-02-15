using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventToSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawn;
    [SerializeField] private int eventType = 0;
    // Start is called before the first frame update
    void OnEnable()
    {
        switch (eventType)
        {
            case 0:
                FindObjectOfType<AudioAnalysisToEvents>().GeneralDetection.AddListener(MakeItSo);
                break;
            case 1:
                FindObjectOfType<AudioAnalysisToEvents>().SubBass.AddListener(MakeItSo);
                break;
            case 2:
                FindObjectOfType<AudioAnalysisToEvents>().Bass.AddListener(MakeItSo);
                break;
            case 3:
                FindObjectOfType<AudioAnalysisToEvents>().LowerMidrange.AddListener(MakeItSo);
                break;
            case 4:
                FindObjectOfType<AudioAnalysisToEvents>().Midrange.AddListener(MakeItSo);
                break;
            case 5:
                FindObjectOfType<AudioAnalysisToEvents>().UpperMidrange.AddListener(MakeItSo);
                break;
            case 6:
                FindObjectOfType<AudioAnalysisToEvents>().Presence.AddListener(MakeItSo);
                break;
            case 7:
                FindObjectOfType<AudioAnalysisToEvents>().Brilliance.AddListener(MakeItSo);
                break;
            default:
                Destroy(gameObject);
                break;
        }
    }

    private void OnDisable()
    {
        if(FindObjectOfType<AudioAnalysisToEvents>()!=null)
        switch (eventType)
        {
            case 0:
                FindObjectOfType<AudioAnalysisToEvents>().GeneralDetection.RemoveListener(MakeItSo);
                break;
            case 1:
                FindObjectOfType<AudioAnalysisToEvents>().SubBass.RemoveListener(MakeItSo);
                break;
            case 2:
                FindObjectOfType<AudioAnalysisToEvents>().Bass.RemoveListener(MakeItSo);
                break;
            case 3:
                FindObjectOfType<AudioAnalysisToEvents>().LowerMidrange.RemoveListener(MakeItSo);
                break;
            case 4:
                FindObjectOfType<AudioAnalysisToEvents>().Midrange.RemoveListener(MakeItSo);
                break;
            case 5:
                FindObjectOfType<AudioAnalysisToEvents>().UpperMidrange.RemoveListener(MakeItSo);
                break;
            case 6:
                FindObjectOfType<AudioAnalysisToEvents>().Presence.RemoveListener(MakeItSo);
                break;
            case 7:
                FindObjectOfType<AudioAnalysisToEvents>().Brilliance.RemoveListener(MakeItSo);
                break;
            default:
                Destroy(gameObject);
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void MakeItSo()
    {
        GameObject Nube = Instantiate(spawn, transform);
        Nube.transform.localPosition = Vector3.zero;
        Destroy(Nube, 3f);
    }
}
