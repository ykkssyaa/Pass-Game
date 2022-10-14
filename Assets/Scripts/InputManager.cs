using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    private int countOfNums = 4;
    public TextMeshProUGUI[] InputNums = new TextMeshProUGUI[4];

    private int currentNumIndex = 0;
    private string currentNumber = "    ";

    private GameManager gameManager;
    [SerializeField] private string ExitSceneName;

    public GameObject helpScreen;
    private bool isActiveHelpScreen = false;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        DeleteAllInputNums();
    }

    // Обновление текста в TextMeshPro UI
    private void SetText(TextMeshProUGUI textUI, string text)
    {
        textUI.text = text;
    }

    // Обновление строки по индексу
    private string StringUpdateByIndex(string str, int index, char charToUpdate)
    {
        string result = "";

        for(int i = 0; i < str.Length; i++)
        {
            if (i != index) result += str[i];
            else result += charToUpdate;
        }

        return result;
    }

    // Обработка нажатий кнопок с числами(клавиатуры)
    public void EnterNewNum(string buttonNum)
    {
        if (!isNumberEnteredBefore(buttonNum[0]))
        {
            // Это работает, потому что кнопка отправляет строку с одним символом(в моем случае)
            currentNumber = StringUpdateByIndex(currentNumber, currentNumIndex, buttonNum[0]);

            SetText(InputNums[currentNumIndex], buttonNum); // Обновление текста текущей ячейки

            if(currentNumIndex < countOfNums - 1)
                currentNumIndex++; 
        }
        else
        {
            ShowThatNumberEnteredBefore();
        }
    }
    // Проверка, вводил ли Игрок это число ранее
    private bool isNumberEnteredBefore(char buttonNum)
    {
        bool res = false;

        foreach(var num in currentNumber)
        {
            if(num == buttonNum) res = true;
        }

        return res;
    }

    private void ShowThatNumberEnteredBefore()
    {

    }

    // Проверка на то, все ли символы введены
    private bool AllNumsAreEntered()
    {
        bool res = true;

        foreach(var num in InputNums)
        {
            if(string.IsNullOrEmpty(num.text))
            {
                res = false; break;
            }
        }

        return res;
    }

    // Возвращение к исходному положению(удаление всех символов в строке ввода)
    private void DeleteAllInputNums()
    {
        foreach(var num in InputNums)
        {
            num.text = "";
        }

        currentNumber = "    ";
        currentNumIndex = 0;
    }

    // Обработка клавиши подтверждения ответа
    public void ConfirmButton()
    {
        if (AllNumsAreEntered())
        {
            gameManager.MatchesOfInputAndPassword(currentNumber);

            DeleteAllInputNums();
        }

    }
    public void DeleteButton()
    {
        if(currentNumIndex > 0 && InputNums[currentNumIndex].text == "")
        {
            currentNumIndex--;
        }

        currentNumber = StringUpdateByIndex(currentNumber, currentNumIndex, ' ');
        SetText(InputNums[currentNumIndex], "");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene(ExitSceneName);
    }

    public void HelpButton()
    {
        gameManager.gameScreen.SetActive(isActiveHelpScreen);

        helpScreen.SetActive(!isActiveHelpScreen);

        isActiveHelpScreen = !isActiveHelpScreen;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
