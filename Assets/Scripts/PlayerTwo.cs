using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTwo : MonoBehaviour
{
    private float forwardSpeed = 0.5f;
    private float backSpeed = 0.3f;

    private float timeBetweenAttack;
    private float startTimeBetweenAttack = 1.0f;

    private Animator anim;
    public Animator playerAnim;

    public Transform punchPoint;
    ///Coordinates of where the punch occurs
    public LayerMask enemyLayers;
    ///The layer defines what is an enemy
    private float lightPunchRange = 0.2f;
    ///Range of the punch
    private float heavyPunchRange = 0.1f;

    private int lightPunchDamage = 10;
    ///How much damage these different punches will deal
    private int heavyPunchDamage = 20;

    public HealthBarTwo healthBarTwo;

    private int maxHealth = 100;
    private int currentHealth;

    private int playerTwoRounds = 0;
    ///Number of rounds won that the game starts off with
    private int playerOneRounds;
    ///To keep track of the number of rounds that player one has in the PlayerTwo class

    public Round round;
    public RoundTwo roundTwo;
    public RoundThree roundThree;
    public RoundFour roundFour;
    public RoundFive roundFive;
    public RoundSix roundSix;
    ///The different round bars that will need to be updated within this class

    public Timer timer;
    ///To reference the timer

    private float timeLeft;

    public PlayerOne playerOne;
    ///A variable called playerOne that will use methods from the PlayerOne class

    private int roundsToWin;

    private int opponent;

    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    private void Start()
    {
        opponent = PlayerPrefs.GetInt("Opponent");
        if (opponent == 2)
        ///If the opponent is the CPU then the PlayerTwo class is not needed
        {
            Destroy(this);
        }
        else
        {
            keys.Add("PlayerTwoLeft", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PlayerTwoLeft", "LeftArrow")));
            keys.Add("PlayerTwoRight", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PlayerTwoRight", "RightArrow")));
            keys.Add("PlayerTwoLightPunch", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PlayerTwoLightPunch", "N")));
            keys.Add("PlayerTwoHeavyPunch", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PlayerTwoHeavyPunch", "M")));
            timeLeft = PlayerPrefs.GetFloat("Time");
            roundsToWin = PlayerPrefs.GetInt("Rounds");
            anim = GetComponent<Animator>();
            currentHealth = maxHealth;
            healthBarTwo.SetMaxHealth(maxHealth);
        }
    }

    private void Update()
    {
        MoveRight();
        MoveLeft();
        Attack();
    }

    private void MoveRight()
    {
        if (Input.GetKey(keys["PlayerTwoRight"]))
        {
            transform.Translate(Vector3.right * backSpeed * Time.deltaTime);
        }
    }

    private void MoveLeft()
    {
        if (Input.GetKey(keys["PlayerTwoLeft"]))
        {
            transform.Translate(Vector3.left * forwardSpeed * Time.deltaTime);
        }
    }

    private void Attack()
    {
        if (timeBetweenAttack <= 0)
        {
            if (Input.GetKey(keys["PlayerTwoLightPunch"]))
            {
                Debug.Log("Light Punch");
                LightPunch();
            }
            else if (Input.GetKey(keys["PlayerTwoHeavyPunch"]))
            {
                Debug.Log("Heavy Punch");
                HeavyPunch();
            }
            timeBetweenAttack = startTimeBetweenAttack;
        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;
        }
    }

    private void LightPunch()
    {
        playerAnim.SetTrigger("LightPunch");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(punchPoint.position, lightPunchRange, enemyLayers);
        ///Creates a circle hitbox at our punchPoint's coordinates with the lightPunchRange
        ///The enemyLayers that were in this hitbox will be stored in a list called hitEnemies
        foreach (Collider2D enemy in hitEnemies)
        ///A for loop checking each game object that we stored in our list
        {
            enemy.GetComponent<PlayerOne>().TakeDamage(lightPunchDamage);
            ///The enemy that was in the list would have its TakeDamage function called
            ///The lightPunchDamage would be the argument
            Debug.Log("We hit " + enemy.name);
            ///If we hit an enemy it will give a message to the console of the enemy that we hit
            Debug.Log(enemy.name + " has " + enemy.GetComponent<PlayerOne>().GetCurrentHealth() + " health");
            ///This is to check that we have dealt damage
        }
    }

    private void HeavyPunch()
    {
        playerAnim.SetTrigger("HeavyPunch");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(punchPoint.position, heavyPunchRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<PlayerOne>().TakeDamage(heavyPunchDamage);
            Debug.Log("We hit " + enemy.name);
            Debug.Log(enemy.name + " has " + enemy.GetComponent<PlayerOne>().GetCurrentHealth() + " health");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (punchPoint == null)
        {
            return;
            ///To prevent crashes
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
    {
        if (Input.GetKey(keys["PlayerTwoRight"]))
        {
            return;
        }
        else
        {
            Damaged(damage);
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy has died");
        playerOneRounds = playerOne.GetPlayerOneRounds();
        ///Gets player one's rounds from the PlayerOne class
        playerOneRounds += 1;
        ///Increments it by 1
        playerOne.SetPlayerOneRounds(playerOneRounds);
        ///Sets player one's rounds to the new amount
        UpdateRoundBars();
        ///The round bars need to be updated
    }

    private void Damaged(int damage)
    {
        playerAnim.SetTrigger("Damaged");
        currentHealth -= damage;
        healthBarTwo.SetHealth(currentHealth);
    }

    public void RoundReset()
    {
        transform.position = new Vector3(1f, -0.5f, 0f);
        ///Puts the player to the original position
        SetCurrentHealth(maxHealth);
        ///Puts health back to max
        healthBarTwo.SetHealth(maxHealth);
        ///Puts health bar to max
        if (timeLeft == 0)
        ///This means the user has chosen the infinite option
        {
            return;
            ///Doesn't need to reset timer
        }
        else
        {
            timer.SetTimeLeft(timeLeft);
            ///Resets the timer
        }
    }

    public int GetPlayerTwoRounds()
    {
        return playerTwoRounds;
    }

    public void SetPlayerTwoRounds(int rounds)
    {
        playerTwoRounds = rounds;
    }

    public void UpdateRoundBars()
    {
        playerOneRounds = playerOne.GetPlayerOneRounds();
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
        ///A lot of if statements to determine which round bar to update
        GameRestart();
        ///This will make sure to check if a player has won after a round
    }

    private void GameRestart()
    {
        if (playerOneRounds == roundsToWin && playerTwoRounds == roundsToWin)
        ///Checking if both players satisfy the criteria
        ///Must mean draw
        {
            Debug.Log("Game draw");
            SceneManager.LoadScene("FinalScene");
            ///Makes the player enter the new scene
        }
        else if (playerOneRounds == roundsToWin)
        ///If only one person satisfies that means single win
        {
            Debug.Log("Player one wins");
            SceneManager.LoadScene("FinalScene");
        }
        else if (playerTwoRounds == roundsToWin)
        {
            Debug.Log("Player two wins");
            SceneManager.LoadScene("FinalScene");
        }
    }
}
