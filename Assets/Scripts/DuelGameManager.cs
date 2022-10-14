using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DuelGameManager : MonoBehaviour
{
    [Space]
    private bool isFirstPlayerNow = true;
    private bool isPasswordInputed;
    

    [SerializeField] private string firstPassword;
    [SerializeField] private string secondPassword;

    private string InputPass;

    [SerializeField] private const int passwordLenght = 4;

    private int equalNumbers; // Количество совпадений чисел, но которые стоят не на своем месте(корова)
    private int equalNumbersWithSamePlace; // Полное совпадение чисел, которые стоят на своем месте(бык)

    [Space]
    private int IndicatorsCount = 4;
    public Image[] resultIndicators = new Image[4];

    [Space(20)]
    [Header("Materials")]
    public Material MatchIndicatorMaterial;
    public Material MatchWithPlaceIndicatorMaterial;
    public Material InvisibleMaterial;

    [Space]
    [Header("Screens")]
    public GameObject gameScreen;
    public GameObject winScreen;
    public TextMeshProUGUI winnerName;

    [Header("History Settings")]
    [Space]

    public TextMeshProUGUI triesText;
    private int triesCount = 0;

    [Space]
    public GameObject FirstHistory;
    public GameObject SecondHistory;
    [Space]
    public GameObject ContentParentFirst;
    public GameObject ContentParentSecond;

    public GameObject TryInHistory;

    [Header("Player Switch Settings")]
    public TextMeshProUGUI playerTurnText;
    public GameObject pleaseInputPasswordText;

    public string playerFirstName = "Player 1";
    public string playerSecondName = "Player 2";

    public GameObject helpButton;

    private void Start()
    {
        isPasswordInputed = false;
    }

    private void PlayerSwitch()
    {
        isFirstPlayerNow = !isFirstPlayerNow;

        if (isFirstPlayerNow) playerTurnText.text = playerFirstName;
        else playerTurnText.text = playerSecondName;
    }

    public void InputProcessing(string InputStr)
    {
        if (isPasswordInputed)
        {
            MatchesOfInputAndPassword(InputStr);
        }
        else
        {
            if (isFirstPlayerNow)
                firstPassword = InputStr;
            else
                secondPassword = InputStr;

            if (!string.IsNullOrEmpty(firstPassword) && !string.IsNullOrEmpty(secondPassword))
            { 
                isPasswordInputed = true;
                pleaseInputPasswordText.SetActive(false); 
            }
        }

        PlayerSwitch();
    }

    private void MatchesOfInputAndPassword(string InputNums)
    {

        string password;

        if(isFirstPlayerNow) password = secondPassword;
        else password = firstPassword;

        InputPass = InputNums;

        if(isFirstPlayerNow)
            triesCount++;

        equalNumbers = 0; equalNumbersWithSamePlace = 0;

        for (int i = 0; i < InputNums.Length; i++)
        {
            for (int j = 0; j < password.Length; j++)
            {
                if (password[j] == InputNums[i])
                {
                    if (i == j)
                    {
                        equalNumbersWithSamePlace++;
                    }
                    else
                    {
                        equalNumbers++;
                    }
                }
            }
        }


        processingOfResults();
    }
    // Обработка результатов
    private void processingOfResults()
    {
        if (equalNumbersWithSamePlace == passwordLenght)
        {
            WinScreen();
        }
        else
        {
            showResult();
            SwitchPlayerHistory();
            AddToHistory();
        }

    }
    // Убираем отображение индикаторов старой попытки
    private void MakeIndicatorsInvisible()
    {
        foreach (var indicator in resultIndicators)
        {
            indicator.material = InvisibleMaterial;
        }
    }

    // Отображение коров и быков для пользователя
    public void showResult()
    {
        int countFirst = equalNumbersWithSamePlace;
        int countSecond = equalNumbers;

        MakeIndicatorsInvisible();

        for (int i = 0; i < IndicatorsCount; i++)
        {
            if (countFirst > 0)
            {
                resultIndicators[i].material = MatchWithPlaceIndicatorMaterial;
                countFirst--;
            }
            else if (countSecond > 0)
            {
                resultIndicators[i].material = MatchIndicatorMaterial;
                countSecond--;
            }
        }
    }
    // Добавление в историю ввёденных кодов
    public void AddToHistory()
    {
        if (isFirstPlayerNow)
        {
            LastTryConstructor LastTry = Instantiate(TryInHistory, ContentParentFirst.transform).GetComponent<LastTryConstructor>();
            LastTry.SetNumbers(InputPass, passwordLenght);
            LastTry.SetIndicators(equalNumbersWithSamePlace, equalNumbers, IndicatorsCount);
        }
        else
        {
            LastTryConstructor LastTry = Instantiate(TryInHistory, ContentParentSecond.transform).GetComponent<LastTryConstructor>();
            LastTry.SetNumbers(InputPass, passwordLenght);
            LastTry.SetIndicators(equalNumbersWithSamePlace, equalNumbers, IndicatorsCount);
        }

    }

    public void SwitchPlayerHistory()
    {
        if (!isFirstPlayerNow)
        {
            FirstHistory.SetActive(true);
            SecondHistory.SetActive(false);
        }

        else
        {
            FirstHistory.SetActive(false);
            SecondHistory.SetActive(true);
        }
    }

    // Показать экран победы
    private void WinScreen()
    {
        if (isFirstPlayerNow)
            winnerName.text = playerFirstName;
        else
            winnerName.text = playerSecondName;

        gameScreen.SetActive(false);
        helpButton.SetActive(false);

        winScreen.SetActive(true);

        triesText.text = "Tries count: " + triesCount;
    }
}
