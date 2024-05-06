using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotesFx : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float disappearanceSpeed;
    [SerializeField] private float colorDisappearanceSpeed;

    [SerializeField] private float lifeTime;

    private float emoteTimer;
    private SpriteRenderer emote;

    private void Start()
    {
        emote = GetComponent<SpriteRenderer>();
        emoteTimer = lifeTime;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + 1), speed * Time.deltaTime);
        emoteTimer -= Time.deltaTime;
        if (emoteTimer < 0)
        {
            float alpha = emote.color.a - colorDisappearanceSpeed * Time.deltaTime;
            emote.color = new Color(emote.color.r, emote.color.g, emote.color.b, alpha);

            if (emote.color.a < 50)
                speed = disappearanceSpeed;

            if (emote.color.a <= 0)
                Destroy(gameObject);
        }

    }
}
