using UnityEngine;
using UnityEngine.UI; // For UI components
using TMPro;

public class ButtonChanger : MonoBehaviour
{
    public Button slasherButton;       
    public Button gunnerButton;
    public Button easyButton;
    public Button normalButton;
    public Button hardButton;
    public TMP_Text skillText;
    public TMP_Text difficultyText;
    public Image characterImage;       
    public Sprite slasherSprite;
    public Sprite gunnerSprite;
    public string slasherText = "Slasher\r\nBasic Attack\r\nSkill 1: Heal Up (Q)\r\nSkill 2: Blade Stomp (E)";
    public string gunnerText = "Gunner\r\nBasic Attack\r\nSkill 1: Heal Up (Q)\r\nSkill 2: Spread Shot (E)\r\n";
    public string easyText = "Easy: Friendly to Newbie";
    public string normalText = "Normal: Good to play";
    public string hardText = "Hard: Challenging";

    void Start()
    {
        if (slasherButton || gunnerButton != null)
        {
            // Get the Button component and add a listener to call ChangeTextAndImage when clicked
            slasherButton.onClick.AddListener(ChangeTextAndImage1);
            gunnerButton.onClick.AddListener(ChangeTextAndImage2);
        }
        if (easyButton || normalButton || hardButton != null)
        {
            // Get the Button component and add a listener to call ChangeText when clicked
            easyButton.onClick.AddListener(ChangeText1);
            normalButton.onClick.AddListener(ChangeText2);
            hardButton.onClick.AddListener(ChangeText3);
        }
    }

    void ChangeTextAndImage1()
    {
        if (skillText != null)
        {
            skillText.text = slasherText; // Change the text
        }

        if (characterImage != null)
        {
            characterImage.sprite = slasherSprite; // Change the image
        }
    }
    void ChangeTextAndImage2()
    {
        if (skillText != null)
        {
            skillText.text = gunnerText; // Change the text
        }

        if (characterImage != null)
        {
            characterImage.sprite = gunnerSprite; // Change the image
        }
    }

    void ChangeText1()
    {
        if (difficultyText != null)
        {
            difficultyText.text = easyText; // Change the text
        }
    }
    void ChangeText2()
    {
        if (difficultyText != null)
        {
            difficultyText.text = normalText; // Change the text
        }
    }
    void ChangeText3()
    {
        if (difficultyText != null)
        {
            difficultyText.text = hardText; // Change the text
        }
    }
}
