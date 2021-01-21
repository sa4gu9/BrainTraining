using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public int cards;
    public float answertime;
    public float showtime;

    List<int> numbers = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        cards = 3;
        showtime = 4000;
        answertime = 4000;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ClickStart()
    {
        for (int i=0; i<cards; i++)
        {
            numbers.Add(Random.Range(1,21));
        }
    }
}
