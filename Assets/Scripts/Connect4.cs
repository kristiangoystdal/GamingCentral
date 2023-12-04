using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class Connect4 : MonoBehaviour
{
    public GameObject gameBoard;
    public List<GameObject> boxes;
    List<List<GameObject>> circels;
    public List<GameObject> tempList;
    public GameObject xText;
    public GameObject oText;
    public GameObject gameOverPanel;

    public string currPlayer;
    public Color currColor;
    GameObject currCircle;
    int currentBox;

    public Color r;
    public Color b;

    public bool win = false;
    public bool selecting;

    public Animator switchPlayer;

    // Start is called before the first frame update
    void Start()
    {
        currPlayer = "O";
        xText.SetActive(false);
        oText.SetActive(true);
        currColor = b;
        circels = new List<List<GameObject>>();
        gameOverPanel.SetActive(false);

        for (int i = 0; i<9; i++)
        {
            tempList = new List<GameObject>();
            for (int j = 0; j < 10; j++)
            {
                boxes[i].transform.GetChild(j).gameObject.SetActive(false);
                tempList.Add(boxes[i].transform.GetChild(j).gameObject);
            }

            circels.Add(tempList);
        }
    }

    void SwitchPlayer()
    {
        if (currPlayer == "O")
        {
            currPlayer = "X";
            xText.SetActive(true);
            oText.SetActive(false);
            currColor = r;
            if(SceneManager.GetActiveScene().name == "Connect Four")
            {
                switchPlayer.SetBool("PlayerState", true);
            } 
        }
        else
        {
            currPlayer = "O";
            xText.SetActive(false);
            oText.SetActive(true);
            currColor = b;
            if (SceneManager.GetActiveScene().name == "Connect Four")
            {
                switchPlayer.SetBool("PlayerState", false);
            }
        }
    }

    public void ButtonPress()
    {
        selecting = true;
        while (selecting)
        {
            MarkButton();
        }
        WinCheck();
        GameOver();
        if (!win)
        {
            SwitchPlayer();
        }  
    }

    void MarkButton()
    {
        GameObject pressedButton = EventSystem.current.currentSelectedGameObject.gameObject;
        
        for (int i = 0; i < 10; i++)
        {
            if (pressedButton.transform.GetChild(i).GetComponent<Image>().color != r && pressedButton.transform.GetChild(i).GetComponent<Image>().color != b)
            {
                if (pressedButton.transform.GetChild(9).gameObject.activeSelf == true)
                {
                    break;
                }
                else
                {
                    currCircle = pressedButton.transform.GetChild(i).gameObject;
                    pressedButton.transform.GetChild(i).gameObject.SetActive(true);
                    print(currColor);
                    pressedButton.transform.GetChild(i).GetComponent<Image>().color = currColor;
                    selecting = false;
                    break;
                } 
            }
        }
    }

    void WinCheck()
    {
        win = false;
        //Sjekk ruter ved siden av ruten som nettopp er plassert hvis de er i samme farge og om det er 4 på rad.

        for(int i = 0; i < 10; i++)
        {
            if (currCircle.GetComponentInParent<Transform>().name.Contains(i.ToString()))
            {
                currentBox = i;
            }
        }
        
        bool found = false;
        for (int i = 0; i < 9; i++)
        {
            for (int k = 0; k < 10; k++)
            {
                if (circels[i][k] == currCircle)
                {   
                    found = true;

                    //Check Straigth
                    if (k > 2)
                    {
                        for (int j = 1; j < 4; j++)
                        {
                            if (circels[i][k - j].GetComponent<Image>().color != currCircle.GetComponent<Image>().color)
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
                    if (win)
                    {
                        break;
                    }

                    //Check right
                    if (i > 2)
                    {
                        for (int j = 1; j < 4; j++)
                        {
                            if (circels[i-j][k].GetComponent<Image>().color != currCircle.GetComponent<Image>().color)
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
                    if (win)
                    {
                        break;
                    }
                    //Center checks
                    if (i > 1 && i < 8)
                    {
                        for (int j = -1; j < 3; j++)
                        {
                            if (circels[i - j][k].GetComponent<Image>().color != currCircle.GetComponent<Image>().color)
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
                    if (win)
                    {
                        break;
                    }
                    if (i > 0 && i < 7)
                    {
                        for (int j = -2; j < 2; j++)
                        {
                            if (circels[i - j][k].GetComponent<Image>().color != currCircle.GetComponent<Image>().color)
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
                    if (win)
                    {
                        break;
                    }

                    //Check left
                    if (i < 6)
                    {
                        for (int j = 1; j < 4; j++)
                        {
                            if (circels[i + j][k].GetComponent<Image>().color != currCircle.GetComponent<Image>().color)
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
                    if (win)
                    {
                        break;
                    }
                    //Center checks
                    if (i < 7 && i > 0)
                    {
                        for (int j = -1; j < 3; j++)
                        {
                            if (circels[i + j][k].GetComponent<Image>().color != currCircle.GetComponent<Image>().color)
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
                    if (win)
                    {
                        break;
                    }
                    if (i < 8 && i > 1)
                    {
                        for (int j = -2; j < 2; j++)
                        {
                            if (circels[i + j][k].GetComponent<Image>().color != currCircle.GetComponent<Image>().color)
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
                    if (win)
                    {
                        break;
                    }

                    //Check down left
                    if (i < 6 && k > 2)
                    {
                        for (int j = 1; j < 4; j++)
                        {
                            if (circels[i + j][k - j].GetComponent<Image>().color != currCircle.GetComponent<Image>().color)
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
                    if (win)
                    {
                        break;
                    }
                    //Center checks
                    if (i < 7 && k > 1 && i > 0 && k < 8)
                    {
                        for (int j = -1; j < 3; j++)
                        {
                            if (circels[i + j][k - j].GetComponent<Image>().color != currCircle.GetComponent<Image>().color)
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
                    if (win)
                    {
                        break;
                    }
                    if (i < 8 && k > 0 && i > 1 && k < 7)
                    {
                        for (int j = -2; j < 2; j++)
                        {
                            if (circels[i + j][k - j].GetComponent<Image>().color != currCircle.GetComponent<Image>().color)
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
                    if (win)
                    {
                        break;
                    }

                    //Check down right
                    if (i > 2 && k > 2)
                    {
                        for (int j = 1; j < 4; j++)
                        {
                            if (circels[i - j][k - j].GetComponent<Image>().color != currCircle.GetComponent<Image>().color)
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
                    if (win)
                    {
                        break;
                    }
                    //Center checks
                    if (i > 1 && k > 1 && i < 8 && k < 8)
                    {
                        for (int j = -1; j < 3; j++)
                        {
                            if (circels[i - j][k - j].GetComponent<Image>().color != currCircle.GetComponent<Image>().color)
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
                    if (win)
                    {
                        break;
                    }
                    if (i > 0 && k > 0 && i < 7 && k < 7)
                    {
                        for (int j = -2; j < 2; j++)
                        {
                            if (circels[i - j][k - j].GetComponent<Image>().color != currCircle.GetComponent<Image>().color)
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
                    if (win)
                    {
                        break;
                    }

                    //Check up left
                    if (i < 7 && k < 6)
                    {
                        for (int j = 1; j < 4; j++)
                        {
                            if (circels[i + j][k + j].GetComponent<Image>().color != currCircle.GetComponent<Image>().color)
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
                    if (win)
                    {
                        break;
                    }

                    //Check up right
                    if (i > 2 && k < 6)
                    {
                        for (int j = 1; j < 4; j++)
                        {
                            if (circels[i - j][k + j].GetComponent<Image>().color != currCircle.GetComponent<Image>().color)
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
                    if (win)
                    {
                        break;
                    }
                }
            }

            if (found)
            {
                break;
            }
        }
    }

    void GameOver()
    {
        if (win)
        {
            gameOverPanel.SetActive(true);

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
}
