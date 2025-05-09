using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D corpo;
    public float velocidade;
    public SpriteRenderer sprite;
    public float jumpForce;
    public GameObject audioMoeda;
    public int moedas;
    void Start()
    {
        // comando para pegar o Rigidbody do player
        corpo = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {   
        float horizontal = Input.GetAxis("Horizontal");
        Move(horizontal);
        Flip(horizontal);
        Jump();
    }
    void Move(float horizontal) {
        Vector3 movimento = new Vector3(horizontal, 0f, 0f);
        transform.position += movimento * Time.deltaTime * velocidade;
    } 
    void Flip(float horizontal) {
        if (horizontal < 0) {
            sprite.flipX = true;
        } else if (horizontal > 0) {
            sprite.flipX = false;
        }
    }
    void Jump() {
        if (Input.GetButtonDown("Jump")) {
            corpo.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Moeda")) {
            GameObject prefeb = Instantiate(audioMoeda, new Vector3(this.gameObject.transform.position.x,
                                                                    this.gameObject.transform.position.y,
                                                                    this.gameObject.transform.position.z), Quaternion.identity);
            Destroy(prefeb.gameObject, 1.5f);
            Destroy(collision.gameObject);
            moedas++;
        }
    }
}
