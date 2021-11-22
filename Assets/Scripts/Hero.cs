using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine;

public enum States// список 
{
    idle,
    run,
    jump,
}

public class Hero : MonoBehaviour
{

    [SerializeField] private float speed = 3f;// скорость движени€
    [SerializeField] private int lives = 3;// количество жизней
    [SerializeField] private float jumpForse = 8f;// сила прыжка
    //
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private Transform grondCheck;
    [SerializeField] public float groundRadius = 0.2f;
    public LayerMask WhatIsGround;
    //
    private Rigidbody2D rb;
    private Animator anim; // поле типа аниматор
    private SpriteRenderer sprite;

    
    //
    private bool hasEntered;

    //
    public static Hero Instance { get; set; }
    private States State
    {
        get { return (States)anim.GetInteger("state"); } // получаем значение стате из аниматора
        set { anim.SetInteger("state", (int)value); } // мен€ем это значение
    }

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // получаем ссылку на компонент пол€
        sprite = GetComponentInChildren<SpriteRenderer>();

    }
    private void FixedUpdate()
    {
        CheckGround();
    }
    private void Update()
    {
        if (isGrounded) State = States.idle; // стоим ли мы на земле ( если стоим то анимаци€ idle)

        if (Input.GetButton("Horizontal"))
            Run();
        if (isGrounded && Input.GetButtonDown("Jump"))
            Jump();
    }
    private void Run()
    {
        if (isGrounded) State = States.run; // проверка така€ же

        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, (speed * Time.deltaTime) * 1.5f);
        sprite.flipX = dir.x < 0.0f;
    }
    private void Jump()
    {
        rb.AddForce(transform.up * jumpForse, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(grondCheck.position, groundRadius, WhatIsGround);

        if (!isGrounded) State = States.jump; // если не касаемс€ земли, то анимаци€ прыжка
    }
    public void GetDamage(int damage)
    {
        lives -= damage;
        Debug.Log("” геро€ "+lives);
        if (lives < 1)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("SampleScene");
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("platform"))
            this.transform.parent = collision.transform;
        if (!collision.gameObject.tag.Equals("platform"))
            this.transform.parent = null;
    }
    
}
