using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryText : MonoBehaviour
{
    public float storySpeed = 5;
    public float storySizeZ;
    private Vector2 startPos;

    private void Start()
    {
        startPos = this.GetComponent<RectTransform>().anchoredPosition;
    }

    private void Update()
    {
        if(this.GetComponent<RectTransform>().anchoredPosition.y < storySizeZ)
        {
            this.GetComponent<RectTransform>().anchoredPosition += Vector2.up * Time.deltaTime * storySpeed;
        }
        else
        {
            this.GetComponent<RectTransform>().anchoredPosition = startPos;
        }
    }
}
