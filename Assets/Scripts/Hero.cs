using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine;

public enum States// ������ 
{
    idle,
    run,
    jump,
}

public class Hero : MonoBehaviour
{

    [SerializeField] private float speed = 3f;// �������� ��������
    [SerializeField] private int lives = 3;// ���������� ������
    [SerializeField] private float jumpForse = 8f;// ���� ������
    //
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private Transform grondCheck;
    [SerializeField] public float groundRadius = 0.2f;
    public LayerMask WhatIsGround;
    //
    private Rigidbody2D rb;
    private Animator anim; // ���� ���� ��������
    private SpriteRenderer sprite;

    private Bullet bullet;
    //
    private bool hasEntered;

    //
    public static Hero Instance { get; set; }
    private States State
    {
        get { return (States)anim.GetInteger("state"); } // �������� �������� ����� �� ���������
        set { anim.SetInteger("state", (int)value); } // ������ ��� ��������
    }

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // �������� ������ �� ��������� ����
        sprite = GetComponentInChildren<SpriteRenderer>();

        bullet = Resources.Load<Bullet>("Bullet");
    }
    private void FixedUpdate()
    {
        CheckGround();
    }
    private void Update()
    {
        if (isGrounded) State = States.idle; // ����� �� �� �� ����� ( ���� ����� �� �������� idle)

        if (Input.GetButtonDown("Fire1")) Shoot(); // ���� ������ ������ ��� �� �������� ����� shoot (��������)

        if (Input.GetButton("Horizontal"))
            Run();
        if (isGrounded && Input.GetButtonDown("Jump"))
            Jump();
    }
    private void Run()
    {
        if (isGrounded) State = States.run; // �������� ����� ��

        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, (speed * Time.deltaTime) * 1.5f);
        sprite.flipX = dir.x < 0.0f;
    }
    private void Jump()
    {
        rb.AddForce(transform.up * jumpForse, ForceMode2D.Impulse);
    }

    private void Shoot()
    {
        Vector3 position = transform.position;
        position.y +=3f;


        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;

        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0F : 1.0F);
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(grondCheck.position, groundRadius, WhatIsGround);

        if (!isGrounded) State = States.jump; // ���� �� �������� �����, �� �������� ������
    }
    public void GetDamage(int damage)
    {
        lives -= damage;
        Debug.Log("� ����� "+lives);
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
