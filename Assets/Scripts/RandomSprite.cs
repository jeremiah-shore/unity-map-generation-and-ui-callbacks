using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour {

    [SerializeField] private Sprite[] sprites;
    [SerializeField] private bool randomizeHorzDirection;

    private SpriteRenderer sprRen;

	void Start () {
        sprRen = gameObject.GetComponent<SpriteRenderer>();
        sprRen.sprite = pickRandomSprite();
        if(randomizeHorzDirection) randomizeDirection();
	}

    private Sprite pickRandomSprite() {
        int i = Random.Range(0, sprites.Length);
        return sprites[i];
    }

    private void randomizeDirection() {
        bool flip = Random.value > 0.5f ? true : false;
        sprRen.flipX = flip;
    }
	
}
