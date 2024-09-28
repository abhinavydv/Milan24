using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPrefabManager: MonoBehaviour
{
    public GameObject bgmGo;
    public GameObject[] gunshotsGo;
    public GameObject[] meleeGo;
    public GameObject laserGo;

    public AudioSource bgm;
    public AudioSource[] gunshots;
    public AudioSource [] melee;
    public AudioSource laser;

    void Start()
    {
        ExtractAudioSource();
    }

    public void ExtractAudioSource()
    {
        GameObject bgmi = Instantiate(bgmGo);
        GameObject[] gunshotsi = new GameObject[gunshotsGo.Length];
        GameObject[] meleei = new GameObject[meleeGo.Length];
        GameObject laseri = Instantiate(laserGo);

        gunshots = new AudioSource[gunshotsGo.Length];
        melee = new AudioSource[meleeGo.Length];

        bgm = bgmi.GetComponent<AudioSource>();

        for (int i = 0; i < gunshotsGo.Length; i++)
        {
            gunshotsi[i] = Instantiate(gunshotsGo[i]);
            gunshots[i] = gunshotsi[i].GetComponent<AudioSource>();
        }

        for (int i = 0; i < meleeGo.Length; i++)
        {
            meleei[i] = Instantiate(meleeGo[i]);
            melee[i] = meleei[i].GetComponent<AudioSource>();
        }

        laser = laseri.GetComponent<AudioSource>();
    }
}
