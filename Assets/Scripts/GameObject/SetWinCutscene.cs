using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetWinCutscene : MonoBehaviour
{
    [SerializeField] public GameObject ImageInput;

    [SerializeField] public Sprite Apple;
    [SerializeField] public Sprite Banana;
    [SerializeField] public Sprite Guarana;
    [SerializeField] public Sprite Pineapple;

    Image image;

    void Start()
    {
        image = ImageInput.GetComponent<Image>();
        Podium podium = PodiumStore.Instance.GetPlayerPodium();
        switch(podium.cart._kartSelected)
        {
            case 1:
                image.sprite = Apple;
                break;
            case 2:
                image.sprite = Banana;
                break;
            case 3:
                image.sprite = Guarana;
                break;
            case 4:
                image.sprite = Pineapple;
                break;
        }

        image.SetNativeSize();
    }

}
