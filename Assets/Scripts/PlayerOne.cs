using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerOne : MonoBehaviour
///The class PlayerOne which inherits methods from Unity's MonoBehaviour class
{

    private float forwardSpeed = 0.5f;
    ///The player's forward speed
    private float backSpeed = 0.3f;
    ///The player's back speed

    private float timeBetweenAttack;
    ///The variable that will measure when a new attack can be activated
    private float startTimeBetweenAttack = 1.0f;
    ///The time needed to wait to do another attack

    private Animator anim;
    public Animator playerAnim;
    ///Creates a variable called playerAnim that will be the animator for the player

    public Transform punchPoint;
    public LayerMask enemyLayers;

    private int lightPunchDamage = 10;
    private float lightPunchRange = 0.2f;
    private int heavyPunchDamage = 20;
    private float heavyPunchRange = 0.1f;

    public HealthBar healthBar;

    private int maxHealth = 100;
    ///This is the maximum health of the object
    private int currentHealth;
    ///This is the current health of the object during the game

    private int playerTwoRounds;

    public Timer timer;
    private float timeLeft;

    private int playerOneRounds = 0;

    public Round round;
    public RoundTwo roundTwo;
    public RoundThree roundThree;
    public RoundFour roundFour;
    public RoundFive roundFive;
    public RoundSix roundSix;

    public PlayerTwo playerTwo;
    ///PlayerTwo class
    public CPU cpu;
    ///CPU class

    private int roundsToWin;
    ///The number of rounds to win

    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    ///A dictionary to store the key codes for the custom controls

    private int opponent;

    private void Start()
    ///This is called when initialising the object
    {
        keys.Add("PlayerOneLeft", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PlayerOneLeft", "A")));
        keys.Add("PlayerOneRight", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PlayerOneRight", "D")));
        keys.Add("PlayerOneLightPunch", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PlayerOneLightPunch", "O")));
        keys.Add("PlayerOneHeavyPunch", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PlayerOneHeavyPunch", "P")));
        ///Adds the key codes to the dic†ionary from PlayerPrefs
        opponent = PlayerPrefs.GetInt("Opponent");
        ///This is so the PlayerOne class knows what opponent its facing
        roundsToWin = PlayerPrefs.GetInt("Rounds");
        ///This is so the PlayerOne class knows how many rounds they need to win
        timeLeft = PlayerPrefs.GetFloat("Time");
        ///This is so the PlayerOne class also knows what option the user picked
        anim = GetComponent<Animator>();
        ///Gets the animator
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        ///Setting the health bar to max
    }

    private void Update()
    {
        MoveRight();
        MoveLeft();
        Attack();
        ///These functions happen during real time and update the sprite on the screen during the game
    }

    private void MoveRight()
    ///Function to move the character to the right
    {
        if (Input.GetKey(keys["PlayerOneRight"]))
        ///Right arrow key needed to move the character to the right
        {
            transform.Translate(Vector3.right * forwardSpeed * Time.deltaTime);
            Debug.Log("Moved right");
            ///This is to give a message to the console that the character moved right
        }
    }

    private void MoveLeft()
    {
        if (Input.GetKey(keys["PlayerOneLeft"]))
        {
            transform.Translate(Vector3.left * backSpeed * Time.deltaTime);
            Debug.Log("Moved left");
        }
    }

    private void Attack()
    {
        if (timeBetweenAttack <= 0)
        ///Checks if the user has waited one second
        {
            if (Input.GetKey(keys["PlayerOneLightPunch"]))
            ///Condition for light punch
            {
                Debug.Log("Light Punch");
                LightPunch();
            }
            else if (Input.GetKey(keys["PlayerOneHeavyPunch"]))
            ///Condition for heavy punch
            {
                Debug.Log("Heavy Punch");
                HeavyPunch();
            }
            timeBetweenAttack = startTimeBetweenAttack;
            ///Resets the timer
        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;
            ///Decreases the time remaining for the next attack in real time
        }
    }

    private void LightPunch()
    {
        playerAnim.SetTrigger("LightPunch");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(punchPoint.position, lightPunchRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (opponent == 1)
            ///If the opponent is 1 then we are playing against a player
            {
                ///Need to reference the PlayerTwo class
                enemy.GetComponent<PlayerTwo>().TakeDamage(lightPunchDamage);
                Debug.Log("We hit " + enemy.name);
            }
            else if (opponent == 2)
            {
                ///Need to reference the PlayerOne class
                enemy.GetComponent<CPU>().TakeDamage(lightPunchDamage);
                Debug.Log("We hit " + enemy.name);
            }
        }
    }

    private void HeavyPunch()
    {
        playerAnim.SetTrigger("HeavyPunch");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(punchPoint.position, heavyPunchRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (opponent == 1)
            {
                enemy.GetComponent<PlayerTwo>().TakeDamage(heavyPunchDamage);
                Debug.Log("We hit " + enemy.name);
            }
            else if (opponent == 2)
            {
                enemy.GetComponent<CPU>().TakeDamage(heavyPunchDamage);
                Debug.Log("We hit " + enemy.name);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (punchPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(punchPoint.position, lightPunchRange);
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetMaxHealth(int health)
    {
        maxHealth = health;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetCurrentHealth(int health)
    {
        currentHealth = health;
    }

    public void TakeDamage(int damage)
    ///The function that is called when dealing damage
    {
        if (Input.GetKey(keys["PlayerOneLeft"]))
        ///Checks if player one is moving backwards
        {
            Debug.Log("Defending");
            return;
        }
        else
        {
            Damaged(damage);
        }
        if (currentHealth <= 0)
        ///This means the round has finished
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy has died");
        if (opponent == 1)
        ///Checks which class to get and set the rounds from
        {
            playerTwoRounds = playerTwo.GetPlayerTwoRounds();
            playerTwoRounds += 1;
            playerTwo.SetPlayerTwoRounds(playerTwoRounds);
        }
        else
        {
            playerTwoRounds = cpu.GetPlayerTwoRounds();
            playerTwoRounds += 1;
            cpu.SetPlayerTwoRounds(playerTwoRounds);
        }
        UpdateRoundBars();
    }

    private void Damaged(int damage)
    {
        playerAnim.SetTrigger("Damaged");
        ///Being damaged animation
        currentHealth -= damage;
        ///The player's health is decreased by damage of the punch
        healthBar.SetHealth(currentHealth);
        ///Makes the health bar update
    }

    public void RoundReset()
    {
        transform.position = new Vector3(-1f, -0.5f, 0f);
        SetCurrentHealth(maxHealth);
        healthBar.SetHealth(maxHealth);
        if (timeLeft == 0)
        {
            return;
        }
        else
        {
            timer.SetTimeLeft(timeLeft);
        }
    }

    public int GetPlayerOneRounds()
    {
        return playerOneRounds;
    }

    public void SetPlayerOneRounds(int rounds)
    {
        playerOneRounds = rounds;
    }

    public void UpdateRoundBars()
    {
        if (opponent == 1)
        ///Uses the first two if statements to choose what class to get the rounds from
        {
            playerTwoRounds = playerTwo.GetPlayerTwoRounds();
        }
        else if (opponent == 2)
        {
            playerTwoRounds = cpu.GetPlayerTwoRounds();
        }
        if (playerOneRounds == 1)
        {
            round.RoundWin();
        }
        if (playerOneRounds == 2)
        {
            roundTwo.RoundWinTwo();
        }
        if (playerOneRounds == 3)
        {
            roundThree.RoundWinThree();
        }
        if (playerTwoRounds == 1)
        {
            roundFour.RoundWinFour();
        }
        if (playerTwoRounds == 2)
        {
            roundFive.RoundWinFive();
        }
        if (playerTwoRounds == 3)
        {
            roundSix.RoundWinSix();
        }
        GameRestart();
    }

    private void GameRestart()
    {
        if (playerOneRounds == roundsToWin && playerTwoRounds == roundsToWin)
        {
            SceneManager.LoadScene("FinalScene");
            Debug.Log("Game draw");
        }
        else if (playerOneRounds == roundsToWin)
        {
            SceneManager.LoadScene("FinalScene");
            Debug.Log("Player one wins");
        }
        else if (playerTwoRounds == roundsToWin)
        {
            SceneManager.LoadScene("FinalScene");
            Debug.Log("Player two wins");
        }
    }
}
