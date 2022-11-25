using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewLevelNotification : MonoBehaviour
{
    private AudioSource _audi;

    [System.Serializable]
    public struct categoryName
    {
        public string name;
        public Sprite sprite;
    };

    public GameData currentGameData;
    public List<categoryName> categoryNames;
    public GameObject winNotification;
    public Image categoryNameImage;


    void Start()
    {
        _audi = GetComponent<AudioSource>();
        winNotification.SetActive(false);

        GameEvents.OnUnlockNextCategory += OnUnlockNextCategory;
    }

    private void OnDisable(){
        GameEvents.OnUnlockNextCategory -= OnUnlockNextCategory;
    }


    private void OnUnlockNextCategory()
    {
        bool puxaProx = false;
        foreach (var passandoNome in categoryNames)
        {
            if (puxaProx)
            {
                //categoryNameImage.sprite = passandoNome.sprite;
                puxaProx = false;
            }
            if(passandoNome.name == currentGameData.selectedCategoryName)
            {
                puxaProx = true;
            }
        }

        winNotification.SetActive(true);
        if (SoundManager.instance.FundoestaMutado() == false)
            _audi.Play();
    }





}
