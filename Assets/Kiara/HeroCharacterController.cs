using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCharacterController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] public float runSpeed = 8f;
    [SerializeField] public float jumpHeight = 2f;
    [SerializeField] public Transform[] groundChecks;


    private float gravity = -50f;
    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;
    public float horizontalInput;
    public Transform player;
    public bool Movement = true;

    private bool jumpPressed;
    private float jumpTimer;
    private float jumpGracePeriod = 0.2f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        horizontalInput = 1;

        //Olhar para frente (Ainda tá como cilindro então não muda nada)
        transform.forward = new Vector3(horizontalInput, 0, Mathf.Abs(horizontalInput) - 1);

        //Tá no chão
        isGrounded = false;

        foreach (var groundCheck in groundChecks)
        {
            if (Physics.CheckSphere(groundCheck.position, 0.1f, groundLayers, QueryTriggerInteraction.Ignore)) 
            {
                isGrounded = true;
                break;
            }
        }


        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }
        else
        {
            //Gravidade
            velocity.y += gravity * Time.deltaTime;
        }

        //Se move para a direita
        if (Movement == true)
        {
            characterController.Move(new Vector3(horizontalInput * runSpeed, 0, 0) * Time.deltaTime);
        }


        // Pulo
        jumpPressed = Input.GetButtonDown("Jump");
        
        // Polimento do pulo
        if (jumpPressed)
        {
            jumpTimer = Time.time;
        }

        if (isGrounded && (jumpPressed || (jumpTimer > 0 && Time.time < jumpTimer + jumpGracePeriod)))
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2 * gravity);
            jumpTimer = -1;
        }

        characterController.Move(velocity * Time.deltaTime);

        //Fim de Jogo
        if (player.position.y < -15)
        {
            FindObjectOfType<GameManager>().EndGame();
        }

    }


}
