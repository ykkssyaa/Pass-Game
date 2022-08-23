using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LastTryConstructor : MonoBehaviour
{
    [SerializeField] private Image[] Indicators;
    [SerializeField] private TextMeshProUGUI[] Nums;

    private GameManager gameManager;

    public Material MatchIndicatorMaterial;
    public Material MatchWithPlaceIndicatorMaterial;
    public Material InvisibleMaterial;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
/*        InvisibleMaterial = gameManager.InvisibleMaterial;
        MatchIndicatorMaterial = gameManager.MatchIndicatorMaterial;
        MatchWithPlaceIndicatorMaterial = gameManager.MatchWithPlaceIndicatorMaterial;*/
    }

    public void SetNumbers(string InputNumbers, int NumCount)
    {
        for(int i = 0; i < NumCount; i++)
        {
            Nums[i].text = InputNumbers[i].ToString();
        }
    }

    private void MakeIndicatorsInvisible()
    {
        foreach (var indicator in Indicators)
        {
            indicator.material = InvisibleMaterial;
        }
    }

    public void SetIndicators(int equalNumbersWithSamePlace, int equalNumbers, int IndicatorsCount)
    {
        int countFirst = equalNumbersWithSamePlace;
        int countSecond = equalNumbers;

        MakeIndicatorsInvisible();

        for (int i = 0; i < IndicatorsCount; i++)
        {
            if (countFirst > 0)
            {
                Indicators[i].material = MatchWithPlaceIndicatorMaterial;
                countFirst--;
            }
            else if (countSecond > 0)
            {
                Indicators[i].material = MatchIndicatorMaterial;
                countSecond--;
            }
        }
    }
}
