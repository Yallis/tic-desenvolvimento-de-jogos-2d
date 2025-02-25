using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {

    public string nextLevelName = "Level 2";
    public float speed = 3.5f;
    public float jumpForce = 5f;
    public GameObject shotPrefab;
    public float shotForce = 10f;
    public Image itemImage;

    bool canJump = true;
    bool canAttack = true;
    bool canWinLevel = false;

    PlayerInputs inputActions;
    SpriteRenderer sprite;
    Animator animator;
    Rigidbody2D rbPlayer;
    SFXController sfxController;
    SceneController sceneController;
    //PlayerAudioController audioController;

    private void Awake() {
        inputActions = new PlayerInputs();
    }

    private void Start() {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rbPlayer = GetComponent<Rigidbody2D>();
        sfxController = FindObjectOfType<SFXController>();
        sceneController = FindObjectOfType<SceneController>();
        //audioController = GetComponent<PlayerAudioController>();
    }

    void Update() {
        var moveInputs = inputActions.Player_Map.Movement.ReadValue<Vector2>();
        transform.position += new Vector3(moveInputs.x, 0, 0) * Time.deltaTime * speed;

        animator.SetBool("b_isWalking", moveInputs.x != 0);

        // Flipa o sprite na direção em que o player estiver olhando
        if(moveInputs.x != 0) {
            sprite.flipX = moveInputs.x < 0;
        }

        // Define se o play pode pular
        canJump = Mathf.Abs(rbPlayer.velocity.y) <= 0.001f;
        
        HandleJumpAction();
        HandleAttack();
    }

    private void OnEnable() {
        inputActions.Enable();
    }

    private void OnDisable() {
        inputActions.Disable();
    }

    // Pular
    void HandleJumpAction() {
        bool jumpPressed = inputActions.Player_Map.Jump.IsPressed();

        if (canJump && jumpPressed) {
            rbPlayer.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            sfxController.PlayJumpSound();
        }
    }

    // Roda a animação de disparo
    void HandleAttack() {
        bool attackPressed = inputActions.Player_Map.Attack.IsPressed();

        if (canAttack && attackPressed) {
            animator.SetTrigger("t_attack");
            canAttack = false;
        }
    }

    // Atira um ovo
    public void ShotNewEgg() {
        // Instancia um ovo
        GameObject newShot = GameObject.Instantiate(shotPrefab);
        newShot.transform.position = new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z);

        bool isLookingRight = !sprite.flipX;
        // Define a direção do tiro
        Vector2 shotDirection = new Vector2(isLookingRight ? -1 : 1, 0) * shotForce;
        // Adiciona impulso ao tiro
        newShot.GetComponent<Rigidbody2D>().AddForce(shotDirection, ForceMode2D.Impulse);
        newShot.GetComponent<SpriteRenderer>().flipY = !isLookingRight;
    }

    public void SetCanAttack() {
        canAttack = true;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // Checa se colidiu com o inimigo
        if (collision.gameObject.CompareTag("Enemy")) {
            sfxController.PlayPlayerDeath();
            Debug.Log("You Lose!");
            sceneController.LoadScene("GameOver");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // Checa se colidiu com o item
        if (collision.gameObject.CompareTag("Item")) {
            Destroy(collision.gameObject);
            itemImage.color = Color.white;
            sfxController.PlayGetItem();
            //audioController.PlayGetItem();
            canWinLevel = true;
        }

        // Checa se colidiu o ponto final da fase
        if (collision.gameObject.CompareTag("Finish")) {
            if (canWinLevel) {
                sfxController.PlayWin();
                Debug.Log("You Won!");
                sceneController.LoadScene(nextLevelName);
            }
        }
    }
}
