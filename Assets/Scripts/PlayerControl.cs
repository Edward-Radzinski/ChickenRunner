using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    private Animator anim;
    private CharacterController controller; //с помощью этого можно управлять персонажем без использования физики юньки
    public Text score;
    public Text currentCoins;

    private Vector3 direction;
    private Vector3 targetPosition;

    public float speed = 10;
    public float jumpForce = 5;
    private float gravity = -20f;
    public float gravityPower = 20f;

    private int moveLine = 1;
    public float distanceLine = 2.5f;
    private float time;
    public int coins = 0;

    //baffs
    public bool magnetBaff = false;
    public int magnetTime = 5;
    public bool timerBaff = false;
    public int timerTime = 5;
    public bool jumpBaff = false;
    public int jumpTime = 5;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MagnetBaff"))
        {
            magnetTime = PlayerPrefs.GetInt("MagnetBaff");
        }
        if (PlayerPrefs.HasKey("JumpBaff"))
        {
            jumpTime = PlayerPrefs.GetInt("MagnetBaff");
        }
        if (PlayerPrefs.HasKey("TimerBaff"))
        {
            timerTime = PlayerPrefs.GetInt("TimerBaff");
        }
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        score.text = "0";
        currentCoins.text = "0";
    }
    private void Update()
    {
        ScoreAndSpeed();
        Movement();
        Baffs();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle") //если это препятсвие то памерець
        {
            PauseMenu.died = true;
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "Coin") //если монетка то удалить ее
        {
            coins++;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Magnet")
        {
            magnetBaff = true;
            StopCoroutine(MBaff());
            Destroy(other.gameObject);
            StartCoroutine(MBaff());
        }
        else if (other.gameObject.tag == "Timer")
        {
            timerBaff = true;
            StopCoroutine(TBaff());
            Destroy(other.gameObject);
            StartCoroutine(TBaff());
        }
        else if (other.gameObject.tag == "Jump")
        {
            jumpBaff = true;
            StopCoroutine(JBaff());
            Destroy(other.gameObject);
            StartCoroutine(JBaff());
        }
        else if (other.gameObject.tag == "BigCoin")
        {
            coins += 15;
            Destroy(other.gameObject);
        }
    }
    private void ScoreAndSpeed()
    {
        time += Time.deltaTime;
        if ((int)transform.position.z / 2 > 0) //рекорд равен пройденному расстоянию игрока деленному на 2
        {
            score.text = ((int)transform.position.z / 2).ToString();
            currentCoins.text = coins.ToString();
        }
        if (time > 5 && speed < 400) //каждый 5 сек увеличивается скорость 
        {
            time = 0;
            speed += 0.5f;
        }
    }
    private void Baffs()
    {
        if (magnetBaff)
            MagneteBaff();
        if (timerBaff && !PauseMenu.pause)
            TimerBaff();
        if (jumpBaff)
            JumpBaff();
    }
    private void Move() //перемещение осуществляется засчет контроллера и тупо перемещения игрока по оси Z
    {
        direction.z = speed;
        direction.y += gravity * Time.fixedDeltaTime;//grafity
        controller.Move(direction * Time.fixedDeltaTime);
    }
    private void Movement() //тут обработка свайпов по экранчику
    {
        if (SwipeManager.swipeRight)
        {
            if (moveLine < 2) //если это не крайняя правая полоса то осуществить перемещение вправо
            {
                moveLine++;
            }
        }
        else if (SwipeManager.swipeLeft) //если это не крайняя левая полоса то осуществить перемещение влево
        {
            if (moveLine > 0)
            {
                moveLine--;
            }
        }
        else if (SwipeManager.swipeUp)
        {
            if (controller.isGrounded) //если челик на земле то прыгнуть (изменение оси Y)
            {
                direction.y = jumpForce;
            }
        }
        else if (SwipeManager.swipeDown) //пригнуться
        {
            gravity = -1000;
            controller.height = 0.8f;
            anim.SetBool("isJump", false); //отключение анимки прыжка
            anim.SetBool("isRoll", true); //включение анимки пригибания
            StartCoroutine(Enum()); // через некоторое время выключить анимку (описано ниже)
        }
        if (controller.isGrounded) //тут проверка на то что не выполняется ничего для воспроизведения анимки бега
        {
            gravity = -gravityPower;
            anim.SetBool("isJump", false);
        }
        else if (!controller.isGrounded && direction.y > 0.5f) //воспроизведение анимки прыжка
        {
            anim.SetBool("isJump", true);
        }

        targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (moveLine == 0)                                  //перемещение
            targetPosition += Vector3.left * distanceLine;  //и это
        else if (moveLine == 2)                             //и это
            targetPosition += Vector3.right * distanceLine; //и это

        if (transform.position == targetPosition) return;
        else //тут происходит правильная реализация позиции коллайдера (без этого игрок бы передвигался а коллайдер бы оставался на месте)
        {
            Vector3 difference = targetPosition - transform.position;
            Vector3 moveDirection = difference.normalized * 25 * Time.deltaTime;
            if (moveDirection.sqrMagnitude < difference.sqrMagnitude)
                controller.Move(moveDirection);
            else
                controller.Move(difference);
        }
    }
    private void MagneteBaff()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject coin in coins)
        {
            if (coin.transform.position.z - transform.position.z < 10)
                coin.transform.position = Vector3.MoveTowards(coin.transform.position, transform.position, 15 * Time.deltaTime);
        }
    }
    private void TimerBaff()
    {
        Time.timeScale = 0.5f;
    }
    private void JumpBaff()
    {
        jumpForce = 15;
    }

    //IEnumerator нужен для того чтобы выполнить какое-либо событие спустя некоторое время, по коду там все понятно
    IEnumerator Enum()
    {
        yield return new WaitForSeconds(0.5f);
        controller.height = 1.5f;
        anim.SetBool("isRoll", false);
    }
    IEnumerator MBaff()
    {
        yield return new WaitForSeconds(magnetTime);
        magnetBaff = false;
    }
    IEnumerator TBaff()
    {
        yield return new WaitForSeconds(timerTime);
        Time.timeScale = 1;
        timerBaff = false;
    }
    IEnumerator JBaff()
    {
        yield return new WaitForSeconds(jumpTime);
        jumpForce = 10;
        jumpBaff = false;
    }
}
