using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageAnim : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    Image image;

    public void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        for(int i = 0; i < sprites.Length; i++)
        {
            image.sprite = sprites[i];
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(Animation());
    }
}
