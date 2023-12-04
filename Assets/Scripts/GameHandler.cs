using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{

    public GameObject gameBoard;
    public List<GameObject> boxes;
    public List<GameObject> covers;
    public List<string> texts = new List<string>();
    public GameObject xText;
    public GameObject oText;
    
    public List<string> largeSymbol;

    public string currPlayer;
    public string currButtonName;

    public bool win = false;
    public bool gameOver = false;
    public bool TTT = false;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "TTT" || SceneManager.GetActiveScene().name == "TTT 1")
        {
            TTT = true;
        }
        currPlayer = "O";
        xText.SetActive(false);
        oText.SetActive(true);
        for (int i = 0; i < 9; i++)
        {
            largeSymbol.Add("-");
            boxes.Add(gameBoard.transform.GetChild(i).gameObject);
            boxes[i].transform.GetChild(2).gameObject.SetActive(false);
            covers[i].SetActive(false);
        }
    }

    void SwitchPlayer()
    {
        if (currPlayer == "O")
        {
            currPlayer = "X";
            xText.SetActive(true);
            oText.SetActive(false);
        }
        else
        {
            currPlayer = "O";
            xText.SetActive(false);
            oText.SetActive(true);
        }
    }

    public void ButtonPress()
    {
        MarkButton();
        print(TTT);
        if (!TTT)
        {
            Endgame();
        }
        else
        {
            InputLargeBox();
        }
        
        LargeBox();

        if (!gameOver)
        {
            if (!TTT)
            {
                UnlockedBox();
            }
           
            SwitchPlayer();
        }
    }

    void MarkButton()
    {
        GameObject pressedButton = EventSystem.current.currentSelectedGameObject.gameObject;
        pressedButton.GetComponent<Button>().interactable = false;
        pressedButton.transform.GetChild(0).GetComponent<TMP_Text>().text = currPlayer;

        currButtonName = pressedButton.name;
    }

    void UnlockedBox()
    {
        List<char> num = new List<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        for (int i = 0; i < 9; i++)
        {
            if (currButtonName.Contains(num[i]))
            {
                if(largeSymbol[i] != "-")
                {
                    UnlockOpen();
                    break;
                }
                else
                {
                    covers[i].SetActive(false);
                }
            }
            else
            {
                covers[i].SetActive(true);
            }
        }
    }

    void Endgame()
    {
        for (int i = 0; i < 9; i++)
        {
            texts.Clear();

            for (int j = 0; j < 9; j++)
            {
                //print(boxes[i].transform.GetChild(0).transform.GetChild(j).name);
                texts.Add(boxes[i].transform.GetChild(0).transform.GetChild(j).transform.GetChild(0).GetComponent<TMP_Text>().text);
            }

            WinCheck(texts);

            if (win)
            {
                SmallGame(i);
            }
        }
    }

    void WinCheck(List<string> set)
    {
        win = false;
        List<List<string>> winStates = new List<List<string>>();
        List<string> temp = new List<string>();

        for (int i = 0; i < 3; i++)
        {
            temp = new List<string>() { set[3 * i], set[3 * i + 1], set[3 * i + 2] };
            winStates.Add(temp);
        }

        for (int i = 0; i < 3; i++)
        {
            temp = new List<string>() { set[i], set[i + 3], set[i + 3 * 2] };
            winStates.Add(temp);
        }
        temp = new List<string>() { set[0], set[4], set[8] };
        winStates.Add(temp);
        temp = new List<string>() { set[2], set[4], set[6] };
        winStates.Add(temp);

        for (int i = 0; i < winStates.Capacity; i++)
        {
            if (win)
            {
                break;
            }

            for (int j = 0; j < 3; j++)
            {
                if (winStates[i][j] != currPlayer)
                {
                    win = false;
                    break;
                }
                else
                {
                    win = true;
                }
            }
        }

        winStates.Clear();
        temp.Clear();
    }

    void SmallGame(int box)
    {
        if (covers[box].activeSelf == false)
        {
            largeSymbol[box] = currPlayer;
            boxes[box].transform.GetChild(2).gameObject.SetActive(true);
            boxes[box].transform.GetChild(2).GetComponent<TMP_Text>().text = currPlayer;
        }

        if (largeSymbol[box] != "-")
        {
            covers[box].SetActive(true);
        }

        UnlockOpen();
    }

    void UnlockOpen()
    {
        for (int i = 0; i < 9; i++)
        {
            if (largeSymbol[i] == "-")
            {
                covers[i].SetActive(false);
            }
            else
            {
                covers[i].SetActive(true);
            }
        }
    }

    void LargeBox()
    {
        win = false;
        WinCheck(largeSymbol);
        print(largeSymbol);

        if (win)
        {
            gameOver = true;

            for (int i = 0; i < 9; i++)
            {
                covers[i].SetActive(true);
            }

            xText.SetActive(true);
            oText.SetActive(true);

            if (currPlayer == "X")
            {
                xText.GetComponent<TMP_Text>().text = "You Win!";
                oText.GetComponent<TMP_Text>().text = "You Lose...";
            }
            else
            {
                xText.GetComponent<TMP_Text>().text = "You Lose...";
                oText.GetComponent<TMP_Text>().text = "You Win!";
            }
        }
    }

    void InputLargeBox()
    {
        for (int i = 0; i < 9; i++)
        {
            string text = boxes[i].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text;
            if(text != "")
            {
                largeSymbol[i] = text;
            }
        }
    }
}
