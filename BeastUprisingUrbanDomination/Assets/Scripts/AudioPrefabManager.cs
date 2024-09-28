using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPrefabManager: MonoBehaviour
{
    public GameObject bgmGo;
    public List<GameObject> gunshotsGo;
    public List<GameObject> meleeGo;
    public GameObject laserGo;

    public AudioSource bgm;
    public List<AudioSource> gunshots;
    public List<AudioSource> melee;
    public AudioSource laser;

    void Start()
    {
        ExtractAudioSource();
    }

    public void ExtractAudioSource()
    {
        GameObject bgmi = Instantiate(bgmGo);
        List<GameObject> gunshotsi = new List<GameObject>();
        List<GameObject> meleei = new List<GameObject>();
        GameObject laseri = Instantiate(laserGo);

        gunshots = new List<AudioSource>();
        melee = new List<AudioSource>();

        bgm = bgmi.GetComponent<AudioSource>();

        for (int i = 0; i < gunshotsGo.Count; i++)
        {
            gunshotsi.Add(Instantiate(gunshotsGo[i]));
            gunshots.Add(gunshotsi[i].GetComponent<AudioSource>());
        }

        for (int i = 0; i < meleeGo.Count; i++)
        {
            meleei.Add(Instantiate(meleeGo[i]));
            melee.Add(meleei[i].GetComponent<AudioSource>());
        }

        laser = laseri.GetComponent<AudioSource>();
    }
}
