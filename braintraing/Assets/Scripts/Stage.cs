using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    public int cards;
    public float answertime;
    public float showtime;

    int fail;
    int success;
    int stage;

    int order;

    public GameObject cardprefab;
    public GameObject canvas;
    public Image timebar;

    public GameObject startButton;
    public GameObject answerPanel;
    public InputField answerfield;

    public Text resultText;

    List<GameObject> currentCards = new List<GameObject>();

    Stopwatch showWatch = new Stopwatch();
    Stopwatch answerWatch = new Stopwatch();

    List<int> numbers = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        order = 0;
        fail = 0;
        success = 0;
        cards = 3;
        showtime = 2000;
        answertime = 2000;
        answerPanel.SetActive(false);
        resultText.text = "";
        stage = 4;

    }

    // Update is called once per frame
    void Update()
    {

        if (showWatch.ElapsedMilliseconds > showtime)
        {
            if (order < cards-1)
            {
                ChangeShowcard(order);
                showWatch.Restart();
            }
            else
            {
                for(int i=0; i<currentCards.Count; i++)
                {
                    Destroy(currentCards[i].gameObject);
                }
                showWatch.Reset();

                order = 0;

                //답 입력 시작
                StartAnswer();

            }
    
        }

        if (answerWatch.IsRunning)
        {
            timebar.fillAmount =1-answerWatch.ElapsedMilliseconds/answertime;

            if (answerWatch.ElapsedMilliseconds > answertime)
            {
                if (numbers[numbers.Count-1-order].ToString() == answerfield.text)
                {
                    if (order < cards - 1)
                    {
                        answerfield.text = "";
                        order++;
                        answerWatch.Restart();
                    }
                    else
                    {
                        ResultCheck(true);
                    }
                }
                else
                {
                    ResultCheck(false);

                }
            }
        }
    }

    private void ResultCheck(bool pass)
    {
        answerfield.text = "";
        numbers.Clear();


        if (pass)
        {
            success++;

        }
        else
        {
            fail++;
        }

        answerWatch.Reset();
        order = 0;

        answerPanel.SetActive(false);
        startButton.SetActive(true);
        resultText.text = $"SUCCESS : {success}\nFAIL : {fail}";
        CheckTime();
    }

    private void CheckTime()
    {
        if (success+fail==4)
        {

            if (success == 4)
            {
                ChangeStage(3);
            }
            else if (success == 3)
            {
                ChangeStage(2);
            }
            else if (success == 2)
            {
                ChangeStage(1);
            }
            else if (success == 1)
            {
                //ChangeStage(0); 변동이 없으므로 주석 처리
            }
            else
            {
                ChangeStage(-1);
            }

            success = 0;
            fail = 0;
        }


    }

    private void ChangeStage(int v)
    {
        stage += v;

        if (stage > 4)
        {
            stage -= 4;
            cards++;
        }
        else if (stage < 1)
        {
            stage += 4;
            cards--;
        }

        if (stage == 1)
        {
            showtime = 4000;
            answertime = 4000;
        }
        else if (stage == 2)
        {
            showtime = 4000;
            answertime = 2000;

        }
        else if (stage == 3)
        {
            showtime = 2000;
            answertime = 4000;

        }
        else if (stage == 4)
        {
            showtime = 2000;
            answertime = 2000;

        }
    }

    public void StartAnswer()
    {
        currentCards.Clear();
        answerPanel.SetActive(true);
        answerfield.ActivateInputField();
        answerWatch.Start();


    }

    public void ClickStart()
    {
        resultText.text = "";

        startButton.SetActive(false);

        List<GameObject> showCards=new List<GameObject>();

        for (int i=0; i<cards; i++)
        {
            numbers.Add(Random.Range(1,21));

            GameObject curCard = Instantiate(cardprefab);
            currentCards.Add(curCard);

            curCard.GetComponent<RectTransform>().SetParent(canvas.transform);
            curCard.GetComponent<RectTransform>().anchoredPosition = new Vector3(80 + 230 * i, 0, 0);
            curCard.GetComponentInChildren<Text>().color = new Color(0,0,0,0);
            curCard.GetComponentInChildren<Text>().text = numbers[i].ToString();

        }

        print("testtesttesttse");
        currentCards[0].GetComponentInChildren<Text>().color = new Color(0,0,0,1);
        showWatch.Start();



    }


    public void ChangeShowcard(int i)
    {
        print("change");

        currentCards[i].GetComponentInChildren<Text>().color = new Color(0, 0, 0, 0);
        currentCards[i + 1].GetComponentInChildren<Text>().color = new Color(0, 0, 0, 1);
        order++;
    }
}
