using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   public Transform startMarker;
public Transform endMarker;

// Movement speed in units/sec.
public float speed = 1.0F;

// Time when the movement started.
private float startTime;

// Total distance between the markers.
private float journeyLength;

//Flip
 private SpriteRenderer _renderer;
 Animator anim;

void Start()
     {
     // Keep a note of the time the movement started.
          startTime = Time.time;

     // Calculate the journey length.
          journeyLength = Vector2.Distance(startMarker.position, endMarker.position);

           _renderer = GetComponent<SpriteRenderer>();
        if (_renderer == null)
        {
           Debug.LogError("Player Sprite is missing a renderer");
        }
     }

// Follows the target position like with a spring
void Update()
     {
          float distCovered = (Time.time - startTime) * speed;

          float fracJourney = distCovered / journeyLength;

          transform.position = Vector2.Lerp(startMarker.position, endMarker.position, Mathf.PingPong (fracJourney, 1));

          //Flip
          if (Input.GetAxisRaw("Horizontal") > 0)
          {
            _renderer.flipX = false;
          }
          else if (Input.GetAxisRaw("Horizontal") < 0)
          {
            _renderer.flipX = true;
          }   

     }
}
