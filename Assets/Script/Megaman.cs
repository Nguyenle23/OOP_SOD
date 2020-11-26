using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Megaman : MonoBehaviour
{
    public float speed = 50f, maxspeed = 3, jumpPow = 220f, maxjump = 4;
    public bool Ground = true, faceright = true, doublejump = false;
    private bool isShooting;

    [SerializeField]
    private float shootDelay = .1f;

    [SerializeField]
    GameObject Bullet;

    [SerializeField]
    Transform BulletSpawnPos;

    public int ourHealth;
    public int maxhealth = 10;

    public Rigidbody2D r2;
    public Animator anim;

    public object position { get; internal set; }

    // Start is called before the first frame update
    void Start()
    {
        r2 = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        ourHealth = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Ground", Ground);
        anim.SetFloat("Speed", Mathf.Abs(r2.velocity.x));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Ground)
            {
                Ground = false;
                doublejump = true;
                r2.AddForce(Vector2.up * jumpPow * 10);
            }
            else if (doublejump)
            {
                doublejump = false;
                r2.velocity = new Vector2(r2.velocity.x, 0);
                r2.AddForce(Vector2.up * jumpPow * 5f);
            }
        }

        if (Input.GetKey(KeyCode.LeftControl) /*|| Input.GetKey(KeyCode.Mouse0)*/)
        {
            if (isShooting) return;

            //shoot
            anim.Play("WalkShoot");
            isShooting = true;

            //instantiate and shoot bullet
            GameObject bullet; 
            bullet = Instantiate(Bullet);
            bullet.GetComponent<Bullet>().StartShoot(faceright);
            bullet.transform.position = BulletSpawnPos.transform.position;

            if (faceright == true)
            {
                bullet.transform.position = BulletSpawnPos.transform.position;
            }
            else
            {
                bullet.transform.position = BulletSpawnPos.transform.position;
                bullet.transform.Rotate(1f, 0f, 180);
            }


            Invoke("ResetShoot", shootDelay);
        }
    }
    public void ResetShoot()
    {
        isShooting = false;
        anim.Play("Idle");
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        r2.AddForce((Vector2.right)* speed * h);

        if (r2.velocity.x > maxspeed)
            r2.velocity = new Vector2(maxspeed, r2.velocity.y);
        if (r2.velocity.x < -maxspeed)
            r2.velocity = new Vector2(-maxspeed, r2.velocity.y);

        if (r2.velocity.y > maxjump)
            r2.velocity = new Vector2(r2.velocity.x, maxjump);
        if (r2.velocity.y < -maxjump)
            r2.velocity = new Vector2(r2.velocity.x, -maxjump);

        if (h > 0 && !faceright)
        {
            Flip();
        }
        if (h < 0 && faceright)
        {
            Flip();
        }
        if (Ground)
        {
            r2.velocity = new Vector2(r2.velocity.x * 0.7f, r2.velocity.y);
        }

        if (ourHealth <= 0)
        {
            Death();
        }

    }
   private void Flip()
    {
        faceright = !faceright;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Damage(int damage)
    {
        ourHealth -= damage;
        gameObject.GetComponent<Animation>().Play("RedFlash");
    }
    
    public void Knockback(float Knockpow, Vector2 Knockdir)
    {
        r2.velocity = new Vector2(0, 0);
        r2.AddForce(new Vector2(Knockdir.x * -100, Knockdir.y * Knockpow));
    }
}

