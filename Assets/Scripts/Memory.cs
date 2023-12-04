using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class Memory : MonoBehaviour
{
    public GameObject gameBoard;
    public List<Sprite> shapes;
    public List<GameObject> buttons;
    public List<int> places;
    public GameObject xText;
    public GameObject oText;
    public Sprite shape1;
    public Sprite shape2;
    public GameObject blocker;

    public string currPlayer;
    public Color currColor;
    public GameObject currShape1;
    public GameObject currShape2;
    int flippedCards = 0;
    int pointsO = 0;
    int pointsX = 0;

    public Color r;
    public Color b;

    public bool win = false;
    bool turn = false;
    bool selecting;

    public Animator flipCard;

    // Start is called before the first frame update
    void Start()
    {
        currPlayer = "O";
        xText.SetActive(false);
        oText.SetActive(true);
        currColor = b;
        buttons = new List<GameObject>();

        for (int i = 0; i < 24; i++)
        {
            buttons.Add(gameBoard.transform.GetChild(0).GetChild(i).gameObject);
        }

        RandomPlacement();
    }

    void RandomPlacement()
    {
        List<int> num = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        for (int i = 0; i < 24; i++)
        {
            while (true)
            {
                int randShape = Random.Range(0, 12);

                if (num[randShape] < 2)
                {
                    buttons[i].transform.GetChild(1).GetComponent<Image>().sprite = shapes[randShape];
                    num[randShape]++;
                    break;
                }
            }
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
            flippedCards = 0;
        }
        else
        {
            currPlayer = "O";
            xText.SetActive(false);
            oText.SetActive(true);
            currColor = b;
            flippedCards = 0;
        }
    }

    public void ButtonPress()
    {
        selecting = true;
        while (selecting)
        {
            MarkButton();
        }

        if (flippedCards == 2)
        {
            PointCheck();
        }

        if (flippedCards == 2)
        {
            SwitchPlayer();
        }

        EndGame();
        GameOver();
    }

    void MarkButton()
    {
        GameObject pressedButton = EventSystem.current.currentSelectedGameObject.gameObject;

        if (pressedButton.GetComponent<Animator>().GetBool("Flipped") != true)
        {
            pressedButton.GetComponent<Animator>().SetBool("Flipped", true);
            selecting = false;
            if (flippedCards == 0)
            {
                currShape1 = pressedButton.transform.GetChild(1).gameObject;
            }
            else
            {
                currShape2 = pressedButton.transform.GetChild(1).gameObject;
            }
            flippedCards++;
        }
        else
        {
            print("try again...");
        }
    }

    void PointCheck()
    {
        shape1 = currShape1.GetComponent<Image>().sprite;
        shape2 = currShape2.GetComponent<Image>().sprite;

        if(shape1 == shape2)
        {
            if (currPlayer == "O")
            {
                pointsO++;
                currShape1.GetComponent<Image>().color = r;
                currShape2.GetComponent<Image>().color = r;
            }
            else
            {
                pointsX++;
                currShape1.GetComponent<Image>().color = b;
                currShape2.GetComponent<Image>().color = b;
            }
            flippedCards = 0;
        }
        else
        {
            StartCoroutine(delay(1));
        }
    }

    IEnumerator delay(float time)
    {
        print("Wait");
        yield return new WaitForSeconds(time);
        print("Run");
        blocker.SetActive(true);
        Flip();
        yield return new WaitForSeconds(time);
        blocker.SetActive(false);
    }

    void Flip()
    {
        currShape1.GetComponentInParent<Animator>().SetBool("Flipped", false);
        currShape2.GetComponentInParent<Animator>().SetBool("Flipped", false);
    }

    void EndGame()
    {
        if(pointsO+pointsX == 12)
        {
            win = true;
        }
    }

    void GameOver()
    {
        if (win)
        {
            xText.SetActive(true);
            oText.SetActive(true);

            if (pointsX > pointsO)
            {
                xText.GetComponent<TMP_Text>().text = "You Win!";
                oText.GetComponent<TMP_Text>().text = "You Lose...";
            }
            else if (pointsX < pointsO)
            {
                xText.GetComponent<TMP_Text>().text = "You Lose...";
                oText.GetComponent<TMP_Text>().text = "You Win!";
            }
            else
            {
                xText.GetComponent<TMP_Text>().text = "Tie";
                oText.GetComponent<TMP_Text>().text = "Tie";
            }
        }
    }

    private void Update()
    {
        EndGame();
        GameOver();
    }
}
