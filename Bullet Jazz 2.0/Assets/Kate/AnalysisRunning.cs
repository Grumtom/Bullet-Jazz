using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AnalysisRunning : MonoBehaviour
{
    [Header("Editable Variables")]
    [Tooltip("Number of frames of analysis before results given (see notes in script)")]
    [SerializeField] private int accuracy = 6;
    /*
     so, in order to detect a beat, we need to know some frame of info, a point in time + and - a period
     at 6, we're waiting 6 Fixed updates before analysis, or rather, the audio that says whether or not a beat is detected is 0.02*7 seconds in the past (fixedUpdate delay times accuracy+1)
     human reaction time to sound is roughly 0.14-0.16 seconds so at 6, we get the most accuracy without compromising on delay
     in the pre-analysis version, we can set it to like 15 and get better results because we'll have our samples in advance
     also, we won't be able to analyse the first accuracy or last accuracy frames of audio

    * A HIGHER DEGREE OF ACCURACY IS ACHIEVEABLE IN UPDATE INSTEAD OF FIXEDUPDATE *
     */
    [Tooltip("Detection is easier with this at this for most songs")]
    [SerializeField] private float thresholdMultipiler = 1.5f;

    [Header("Display Values (for Tom)")]
    [SerializeField] private float latestTime;
    [SerializeField] private float latestFlux;
    [SerializeField] private float latestSubBass;
    [SerializeField] private float latestBass;
    [SerializeField] private float latestLowMid;
    [SerializeField] private float latestMid;
    [SerializeField] private float latestUpperMid;
    [SerializeField] private float latestPres;
    [SerializeField] private float latestBril;

    [Header("Display Values (for Tom) these are a few frames behind")]
    [SerializeField] private float latestFluxThresh;
    [SerializeField] private float latestSubBassThresh;
    [SerializeField] private float latestBassThresh;
    [SerializeField] private float latestLowMidThresh;
    [SerializeField] private float latestMidThresh;
    [SerializeField] private float latestUpperMidThresh;
    [SerializeField] private float latestPresThresh;
    [SerializeField] private float latestBrilThresh;

    [Header("Frequency Ranges for Detections (Standard values in script)")]
    [SerializeField] private float subBassLower = 20;
    [SerializeField] private float subBassUpper = 60;
    [SerializeField] private float bassLower = 60;
    [SerializeField] private float bassUpper = 250;
    [SerializeField] private float lowerMidLower = 250;
    [SerializeField] private float lowerMidUpper = 500;
    [SerializeField] private float midLower = 500;
    [SerializeField] private float midUpper = 2000;
    [SerializeField] private float upperMidLower = 2000;
    [SerializeField] private float upperMidUpper = 4000;
    [SerializeField] private float presLower = 4000;
    [SerializeField] private float presUpper = 6000;
    [SerializeField] private float brilLower = 6000;
    [SerializeField] private float brilUpper = 200000;

    [Header("Standard Variables (only adjust in rare cases)")]
    [Tooltip("The \"power of 2\" size of the samples taken")]
    [SerializeField] private int sampleSize = 10;

    [Header("Automatic Variables (Change these and nothing happens (or it all breaks))")]
    [SerializeField] private bool firstSampled = false;
    [Tooltip("The AudioSource")]
    [SerializeField] private new AudioSource audio;
    [Tooltip("Standard sample rate of a track is 44100 or 48000 samples/second of audio")]
    [SerializeField] private int sampleRate;
    [Tooltip("The Actual size of the array of samples")]
    [SerializeField] private int sampleArrayLength = 1;
    [Tooltip("Sampling Array")]
    [SerializeField] private float[] currWorkingSamples;
    [Tooltip("Previous Sample Array, for comparison (Onset is based on the difference between samples)")]
    [SerializeField] public float[] prevWorkingSamples;
    [Tooltip("Once the sample is Fourier'd, each band will be this range large")]
    [SerializeField] private float freqBandSize;


    //[SerializeField]
    private List<SpectralFluxInfo> spectralFluxInfos;
    //[SerializeField]
    private SpectralFluxInfo latestData;
    [Serializable]
    public class SpectralFluxInfo
    {
        public float time;
        public float spectralFlux; //positive regions only
        public float threshold;
        public float prunedSpectralFlux;

        public float subBassSpectralFlux; //superlow frequency (typically unheard of) 20-60
        public float bassSpectralFlux; //typical bass, like, what gets boosted kind 60-250
        public float lowMidSpectralFlux;//bass, but smoother 250-500
        public float midSpectralFlux;//horns 500-2000
        public float upperMidSpectralFlux;//vocals (typically) 2000-4000
        public float presSpectralFlux; //clarity? 4000-6000
        public float brilSpectralFlux; //"sparkle and air" god music people are pretentious 6000-20000

        public float subBassThresh;
        public float bassThresh;
        public float lowMidThresh;
        public float midThresh;
        public float upperMidThresh;
        public float presThresh;
        public float brilThresh;

        public float prunedSBSpectralFlux;
        public float prunedBSpectralFlux;
        public float prunedLMSpectralFlux;
        public float prunedMSpectralFlux;
        public float prunedUMSpectralFlux;
        public float prunedPSpectralFlux;
        public float prunedBrilSpectralFlux;

        public bool isSBassPeak;
        public bool isBassPeak;
        public bool isLMidPeak;
        public bool isMidPeak;
        public bool isUMidPeak;
        public bool isPresPeak;
        public bool isBrilPeak;

        public bool isPeak;

        public float[] Fluxen;
        public bool[] Beatsen;
        public float[] Prunen;

        public SpectralFluxInfo()
        {
            Fluxen = new float[1024];
            Beatsen = new bool[1024];
            Prunen = new float[1024];
        }

        public SpectralFluxInfo(int size)
        {
            Fluxen = new float[size];
            Beatsen = new bool[size];
            Prunen = new float[size];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        sampleRate = audio.clip.frequency;
        sampleArrayLength = 1;
        for (int i = 0; i < sampleSize; i++)
        {
            sampleArrayLength *= 2;
        }
        currWorkingSamples = new float[sampleArrayLength];
        prevWorkingSamples = new float[sampleArrayLength];
        freqBandSize = sampleRate / (float)sampleArrayLength / 2f;
        spectralFluxInfos = new List<SpectralFluxInfo>();
    }

    // FixedUpdate is called once per Physics Update
    void FixedUpdate()
    {
        prevWorkingSamples = currWorkingSamples;
        currWorkingSamples = new float[sampleArrayLength];
        audio.GetSpectrumData(currWorkingSamples, 0, FFTWindow.BlackmanHarris); //channel 0 is the average of the left and right channels, BlackmanHarris is the most complex but accurate version of the FTT window

        

        if (sampleArrayLength / sampleRate > Time.deltaTime)
        {
            //if the game drops enough frames some parts of the music might not get analysed, leading to missed beats
            Debug.Log("Data Loss Detected: " + Time.deltaTime + " time is too long for " + sampleArrayLength + " samples per frame");
        }

        if (firstSampled)
            addSamplesToFluxArray();
        else
            firstSampled = true;
        if (spectralFluxInfos.Count >= accuracy * 2 + 1)//if we've at least 13 samples
        {
            calculateValues(spectralFluxInfos.Count - accuracy - 1);
            pruneValues(spectralFluxInfos.Count - accuracy - 1);
            detectPeaks(spectralFluxInfos.Count - accuracy - 2);
            triggerEvents(spectralFluxInfos.Count - accuracy - 2);
        }
    }
    private void triggerEvents(int index)
    {
        FindObjectOfType<AudioAnalysisToEvents>().handleEvents(spectralFluxInfos[index]);
    }

    private void detectPeaks(int index)
    {
        spectralFluxInfos[index].isPeak = (spectralFluxInfos[index].prunedSpectralFlux > spectralFluxInfos[index + 1].prunedSpectralFlux && spectralFluxInfos[index].prunedSpectralFlux > spectralFluxInfos[index - 1].prunedSpectralFlux);
        spectralFluxInfos[index].isSBassPeak = (spectralFluxInfos[index].prunedSBSpectralFlux > spectralFluxInfos[index + 1].prunedSBSpectralFlux && spectralFluxInfos[index].prunedSBSpectralFlux > spectralFluxInfos[index - 1].prunedSBSpectralFlux);
        spectralFluxInfos[index].isBassPeak = (spectralFluxInfos[index].prunedBSpectralFlux > spectralFluxInfos[index + 1].prunedBSpectralFlux && spectralFluxInfos[index].prunedBSpectralFlux > spectralFluxInfos[index - 1].prunedBSpectralFlux);
        spectralFluxInfos[index].isLMidPeak = (spectralFluxInfos[index].prunedLMSpectralFlux > spectralFluxInfos[index + 1].prunedLMSpectralFlux && spectralFluxInfos[index].prunedLMSpectralFlux > spectralFluxInfos[index - 1].prunedLMSpectralFlux);
        spectralFluxInfos[index].isMidPeak = (spectralFluxInfos[index].prunedMSpectralFlux > spectralFluxInfos[index + 1].prunedMSpectralFlux && spectralFluxInfos[index].prunedMSpectralFlux > spectralFluxInfos[index - 1].prunedMSpectralFlux);
        spectralFluxInfos[index].isUMidPeak = (spectralFluxInfos[index].prunedUMSpectralFlux > spectralFluxInfos[index + 1].prunedUMSpectralFlux && spectralFluxInfos[index].prunedUMSpectralFlux > spectralFluxInfos[index - 1].prunedUMSpectralFlux);
        spectralFluxInfos[index].isPresPeak = (spectralFluxInfos[index].prunedPSpectralFlux > spectralFluxInfos[index + 1].prunedPSpectralFlux && spectralFluxInfos[index].prunedPSpectralFlux > spectralFluxInfos[index - 1].prunedPSpectralFlux);
        spectralFluxInfos[index].isBrilPeak = (spectralFluxInfos[index].prunedBrilSpectralFlux > spectralFluxInfos[index + 1].prunedBrilSpectralFlux && spectralFluxInfos[index].prunedBrilSpectralFlux > spectralFluxInfos[index - 1].prunedBrilSpectralFlux);
    }

    private void pruneValues(int index)
    {
        spectralFluxInfos[index].prunedSpectralFlux = Mathf.Max(0f, spectralFluxInfos[index].spectralFlux - spectralFluxInfos[index].threshold);
        spectralFluxInfos[index].prunedSBSpectralFlux = Mathf.Max(0f, spectralFluxInfos[index].subBassSpectralFlux - spectralFluxInfos[index].threshold);
        spectralFluxInfos[index].prunedBSpectralFlux = Mathf.Max(0f, spectralFluxInfos[index].bassSpectralFlux - spectralFluxInfos[index].threshold);
        spectralFluxInfos[index].prunedLMSpectralFlux = Mathf.Max(0f, spectralFluxInfos[index].lowMidSpectralFlux - spectralFluxInfos[index].threshold);
        spectralFluxInfos[index].prunedMSpectralFlux = Mathf.Max(0f, spectralFluxInfos[index].midSpectralFlux - spectralFluxInfos[index].threshold);
        spectralFluxInfos[index].prunedUMSpectralFlux = Mathf.Max(0f, spectralFluxInfos[index].upperMidSpectralFlux - spectralFluxInfos[index].threshold);
        spectralFluxInfos[index].prunedPSpectralFlux = Mathf.Max(0f, spectralFluxInfos[index].presSpectralFlux - spectralFluxInfos[index].threshold);
        spectralFluxInfos[index].prunedBrilSpectralFlux = Mathf.Max(0f, spectralFluxInfos[index].brilSpectralFlux - spectralFluxInfos[index].threshold);
    }

    private void calculateValues(int index)
    {
        int startIndex = Math.Max(0, index - accuracy);
        int endIndex = Math.Min(spectralFluxInfos.Count - 1, index + accuracy);

        float sum = 0f;
        float sbsum = 0f;
        float bsum = 0f;
        float lmsum = 0f;
        float msum = 0f;
        float umsum = 0f;
        float psum = 0f;
        float brilsum = 0f;
        for (int i = startIndex; i < endIndex; i++)
        {
            sum += spectralFluxInfos[i].spectralFlux;
            sbsum += spectralFluxInfos[i].subBassSpectralFlux;
            bsum += spectralFluxInfos[i].bassSpectralFlux;
            lmsum += spectralFluxInfos[i].lowMidSpectralFlux;
            msum += spectralFluxInfos[i].midSpectralFlux;
            umsum += spectralFluxInfos[i].upperMidSpectralFlux;
            psum += spectralFluxInfos[i].presSpectralFlux;
            brilsum += spectralFluxInfos[i].brilSpectralFlux;
        }

        spectralFluxInfos[index].threshold = sum / (endIndex - startIndex) * thresholdMultipiler;
        spectralFluxInfos[index].subBassThresh = sbsum / (endIndex - startIndex) * thresholdMultipiler;
        spectralFluxInfos[index].bassThresh = bsum / (endIndex - startIndex) * thresholdMultipiler;
        spectralFluxInfos[index].lowMidThresh = lmsum / (endIndex - startIndex) * thresholdMultipiler;
        spectralFluxInfos[index].midThresh = msum / (endIndex - startIndex) * thresholdMultipiler;
        spectralFluxInfos[index].upperMidThresh = umsum / (endIndex - startIndex) * thresholdMultipiler;
        spectralFluxInfos[index].presThresh = psum / (endIndex - startIndex) * thresholdMultipiler;
        spectralFluxInfos[index].brilThresh = brilsum / (endIndex - startIndex) * thresholdMultipiler;


        latestFluxThresh = spectralFluxInfos[index].threshold;
        latestSubBassThresh = spectralFluxInfos[index].subBassThresh;
        latestBassThresh = spectralFluxInfos[index].bassThresh;
        latestLowMidThresh = spectralFluxInfos[index].lowMidThresh;
        latestMidThresh = spectralFluxInfos[index].midThresh;
        latestUpperMidThresh = spectralFluxInfos[index].upperMidThresh;
        latestPresThresh = spectralFluxInfos[index].presThresh;
        latestBrilThresh = spectralFluxInfos[index].brilThresh;
    }

    private void addSamplesToFluxArray()
    {
        latestData = new SpectralFluxInfo();
        latestData.time = audio.time;
        for (int i = 0; i < sampleArrayLength; i++)
        {
            float bandFlux = Mathf.Max(0, currWorkingSamples[i] - prevWorkingSamples[i]);
            latestData.spectralFlux += bandFlux;
            if (i * freqBandSize > subBassLower && i * freqBandSize < subBassUpper)
            {
                latestData.subBassSpectralFlux += bandFlux;
            }
            if (i * freqBandSize > bassLower && i * freqBandSize < bassUpper)
            {
                latestData.bassSpectralFlux += bandFlux;
            }
            if (i * freqBandSize > lowerMidLower && i * freqBandSize < lowerMidUpper)
            {
                latestData.lowMidSpectralFlux += bandFlux;
            }
            if (i * freqBandSize > midLower && i * freqBandSize < midUpper)
            {
                latestData.midSpectralFlux += bandFlux;
            }
            if (i * freqBandSize > upperMidLower && i * freqBandSize < upperMidUpper)
            {
                latestData.upperMidSpectralFlux += bandFlux;
            }
            if (i * freqBandSize > presLower && i * freqBandSize < presUpper)
            {
                latestData.presSpectralFlux += bandFlux;
            }
            if (i * freqBandSize > brilLower && i * freqBandSize < brilUpper)
            {
                latestData.brilSpectralFlux += bandFlux;
            }
        }


        if(spectralFluxInfos.Count == 0 || (latestData.time != spectralFluxInfos[spectralFluxInfos.Count-1].time))
        spectralFluxInfos.Add(latestData);

        latestTime = latestData.time;
        latestFlux = latestData.spectralFlux;
        latestSubBass = latestData.subBassSpectralFlux;
        latestBass = latestData.bassSpectralFlux;
        latestLowMid = latestData.lowMidSpectralFlux;
        latestMid = latestData.midSpectralFlux;
        latestUpperMid = latestData.upperMidSpectralFlux;
        latestPres = latestData.presSpectralFlux;
        latestBril = latestData.brilSpectralFlux;
    }
}
