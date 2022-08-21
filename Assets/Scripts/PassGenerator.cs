using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassGenerator : MonoBehaviour
{

    private List<char> numbList = new List<char>() {'1', '2', '3','4', '5', '6', '7', '8', '9', '0'};

    public string GenegatePassword(int lenght)
    {
        string result = "";

        for(int i = 0; i < lenght; i++)
        {
            AddRandomNum(ref result);
        }

       return result;
    }

    private void AddRandomNum(ref string str) // »спользую ссылку на строку 
    {
        int randIndex = Random.Range(0, numbList.Count);
        char randNum = numbList[randIndex];

        numbList.RemoveAt(randIndex); // ”дал€ю символ из списка дл€ избежани€ повторений

        str = str + randNum;
    }

}
