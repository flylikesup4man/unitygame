using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagerScript : MonoBehaviour
{

    public TMP_Text scoreText;
    public Button clickButton;
    public Button upgradeButton;

    private double score = 0;
    private int autoClickLevel = 0;
    private double autoClickCost = 50;
    private double autoClickPower = 1;
    private float autoClickTimer = 1f;

    public AudioSource audioSource;

    public AudioClip clickSound;
    public AudioClip upgradeSound;


    void Start()
    {
        clickButton.onClick.AddListener(OnClickCoin);
        upgradeButton.onClick.AddListener(BuyAutoClick);
        UpdateUI();
        InvokeRepeating(nameof(AutoClick), 1f, autoClickTimer);

    }
    
    void OnClickCoin()
    {
        score += 1;
        audioSource.PlayOneShot(clickSound);

        UpdateUI();
    }

    void BuyAutoClick()
    {
        if (score >= autoClickCost)
        {
            score -= autoClickCost;
            autoClickLevel++;
            autoClickPower += 1;
            autoClickCost *= 1.5;
            audioSource.PlayOneShot(upgradeSound);

            UpdateUI();
        }
    }

    void AutoClick()
    {
        if (autoClickLevel > 0)
        {
            score += autoClickPower * autoClickLevel;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        scoreText.text = $"Coins: {score:F0}";
        upgradeButton.GetComponentInChildren<TMP_Text>().text = 
            $"Update ({autoClickCost:F0})";
    }
    void Update()
    {
        
    }
}
