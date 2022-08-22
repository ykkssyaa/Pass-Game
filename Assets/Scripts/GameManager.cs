using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private InputManager inputManager;
    private PassGenerator passGenerator;

    [Space]
    [SerializeField] private string password;
    private string InputPass;
    [SerializeField] private int passwordLenght = 4;

    private int equalNumbers; // Количество совпадений чисел, но которые стоят не на своем месте(корова)
    private int equalNumbersWithSamePlace; // Полное совпадение чисел, которые стоят на своем месте(бык)

    [Space]
    private int IndicatorsCount = 4;
    public Image[] resultIndicators = new Image[4];
    [Space]
    public Material MatchIndicatorMaterial;
    public Material MatchWithPlaceIndicatorMaterial;
    public Material InvisibleMaterial;

    [Space]
    public GameObject gameScreen;
    public GameObject winScreen;

    public TextMeshProUGUI triesText;
    private int triesCount = 0;

    [Space]

    public GameObject ContentParent;
    public GameObject TryInHistory;

    private void Start()
    {
        inputManager = GetComponent<InputManager>();
        passGenerator = GetComponent<PassGenerator>();

        password = passGenerator.GenegatePassword(passwordLenght);
    }

    public void MatchesOfInputAndPassword(string InputNums)
    {
        InputPass = InputNums;

        triesCount++;
        equalNumbers = 0; equalNumbersWithSamePlace = 0;

        for(int i = 0; i < InputNums.Length; i++)
        {
            for(int j = 0; j < password.Length; j++)
            {
                if(password[j] == InputNums[i])
                {
                    if(i == j)
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
        if(equalNumbersWithSamePlace == passwordLenght)
        {
            WinScreen();
        }
        else
        {
            showResult();
            AddToHistory();
        }

    }
    // Убираем отображение индикаторов старой попытки
    private void MakeIndicatorsInvisible()
    {
        foreach(var indicator in resultIndicators)
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

        for(int i=0; i < IndicatorsCount; i++)
        {
            if(countFirst > 0)
            {
                resultIndicators[i].material = MatchWithPlaceIndicatorMaterial;
                countFirst--;
            }
            else if(countSecond > 0)
            {
                resultIndicators[i].material = MatchIndicatorMaterial;
                countSecond--;
            }
        }
    }
    // Добавление в историю ввёденных кодов
    public void AddToHistory()
    {
        LastTryConstructor LastTry = Instantiate(TryInHistory, ContentParent.transform).GetComponent<LastTryConstructor>();

        LastTry.SetNumbers(InputPass, passwordLenght);
        LastTry.SetIndicators(equalNumbersWithSamePlace, equalNumbers, IndicatorsCount);
    }
    // Показать экран победы
    private void WinScreen()
    {
        gameScreen.SetActive(false);

        winScreen.SetActive(true);

        triesText.text = "Tries count: " + triesCount;
    }
}
