using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class UIKeyFrame : MonoBehaviour
{
    [SerializeField] private Sprite newSprite;
    [SerializeField] private float enterDelay = 1f;
    [SerializeField] private float backDelay = 2f;
    private Sprite standardSprite;
    private SpriteRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        standardSprite = renderer.sprite;
    }
    public void Work()
    {
        StartCoroutine(ChangeSprite());
    }
    public IEnumerator ChangeSprite()
    {
        yield return new WaitForSeconds(enterDelay);
        renderer.sprite = newSprite;
        yield return new WaitForSeconds(backDelay);
        renderer.sprite = standardSprite;
    }
}
