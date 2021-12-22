using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]//garantir que ter� "AudioSource" quando for referenciado
public class SoundManager : MonoBehaviour
{

    private bool _muteBackgMusic = false;
    private bool _muteSoundFx = false;
    public static SoundManager instance;

    private AudioSource _audioSource;
    private void Awake()//esta fun��o n�o permite ter mais de um soundmanager no jogo caso tenha ele ser� destruido;
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
    }

    public void MudaSomFundo()
    {
        _muteBackgMusic = !_muteBackgMusic;

        if (_muteBackgMusic)
        {
            _audioSource.Stop();
        }
        else
        {
            _audioSource.Play();
        }
    }

    public void MudaSomEfeito()
    {
        _muteSoundFx = !_muteSoundFx;
        GameEvents.OnMudaSomEfeitoMethod();
    }

    public bool FundoestaMutado()
    {
        return _muteBackgMusic;
    }

    public bool EfeitoestaMutado(){
        return _muteSoundFx;
    }
    public void MutaBackMusic(bool mudo)
    {
        if (_muteBackgMusic == false)
        {
            if (mudo)
                _audioSource.volume = 0f;
            else
                _audioSource.volume = 1f;
        }
    }



}
