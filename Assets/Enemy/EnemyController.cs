using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {


    public float speed = 2.0f;
    public Transform[] waypoints;

    int currentIndex = 0;

    Animator animator;
    SpriteRenderer sprite;
    
    void Start() {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    
    void Update() {

        if(waypoints.Length == 0) {
            animator.SetBool("b_isWalking", false);
            return;
        }

        animator.SetBool("b_isWalking", true);

        Transform currentWaypoint = waypoints[currentIndex];

        sprite.flipX = transform.position.x > currentWaypoint.position.x;

        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentWaypoint.position) <= 0.1f ){
            currentIndex = (currentIndex + 1) % waypoints.Length;
        }
    }
}
