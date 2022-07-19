using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int price;
    public GameObject prefab;

    public TextMeshProUGUI costText;

    [Header("Image Stuff")]
    public Image background;

    public Image characterImage;

    public Sprite disabledBack;

    public Sprite disabledChar;

    private Sprite origChar;

    private Sprite origBack;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = price.ToString(CultureInfo.InvariantCulture);
        origBack = background.sprite;
        origChar = characterImage.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerStats.instance.money < price)
        {
            background.sprite = disabledBack;
            characterImage.sprite = disabledChar;
            GetComponent<Button>().interactable = false;
        }
        else
        {
            if (background.sprite != disabledBack) 
                return;
            background.sprite = origBack;
            characterImage.sprite = origChar;
            GetComponent<Button>().interactable = true;
        }
    }
}
