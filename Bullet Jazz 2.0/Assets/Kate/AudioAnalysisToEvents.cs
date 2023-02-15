using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioAnalysisToEvents : MonoBehaviour
{
    public UnityEvent GeneralDetection;
    public  UnityEvent SubBass;
    public  UnityEvent Bass;
    public  UnityEvent LowerMidrange;
    public  UnityEvent Midrange;
    public  UnityEvent UpperMidrange;
    public  UnityEvent Presence;
    public  UnityEvent Brilliance;
    public UnityEvent[] Individualized;
    public int size = 1024;

    private void Awake()
    {
        Individualized = new UnityEvent[size];
    }

    public void handleEvents(AnalysisRunning.SpectralFluxInfo spectralFluxInfo)
    {
        if (spectralFluxInfo.isPeak)
        {
            GeneralDetection.Invoke();
        }
        if (spectralFluxInfo.isSBassPeak)
        {
            SubBass.Invoke();
        }
        if (spectralFluxInfo.isBassPeak)
        {
            Bass.Invoke();
        }
        if (spectralFluxInfo.isLMidPeak)
        {
            LowerMidrange.Invoke();
        }
        if (spectralFluxInfo.isMidPeak)
        {
            Midrange.Invoke();
        }
        if (spectralFluxInfo.isUMidPeak)
        {
            UpperMidrange.Invoke();
        }
        if (spectralFluxInfo.isPresPeak)
        {
            Presence.Invoke();
        }
        if (spectralFluxInfo.isBrilPeak)
        {
            Brilliance.Invoke();
        }
    }
}
