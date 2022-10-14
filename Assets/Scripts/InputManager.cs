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

    // ���������� ������ � TextMeshPro UI
    private void SetText(TextMeshProUGUI textUI, string text)
    {
        textUI.text = text;
    }

    // ���������� ������ �� �������
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

    // ��������� ������� ������ � �������(����������)
    public void EnterNewNum(string buttonNum)
    {
        if (!isNumberEnteredBefore(buttonNum[0]))
        {
            // ��� ��������, ������ ��� ������ ���������� ������ � ����� ��������(� ���� ������)
            currentNumber = StringUpdateByIndex(currentNumber, currentNumIndex, buttonNum[0]);

            SetText(InputNums[currentNumIndex], buttonNum); // ���������� ������ ������� ������

            if(currentNumIndex < countOfNums - 1)
                currentNumIndex++; 
        }
        else
        {
            ShowThatNumberEnteredBefore();
        }
    }
    // ��������, ������ �� ����� ��� ����� �����
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

    // �������� �� ��, ��� �� ������� �������
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

    // ����������� � ��������� ���������(�������� ���� �������� � ������ �����)
    private void DeleteAllInputNums()
    {
        foreach(var num in InputNums)
        {
            num.text = "";
        }

        currentNumber = "    ";
        currentNumIndex = 0;
    }

    // ��������� ������� ������������� ������
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
