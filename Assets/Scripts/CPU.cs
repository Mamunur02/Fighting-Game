using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CPU : MonoBehaviour
{
    private float forwardSpeed = 0.5f;
    private float backSpeed = 0.3f;

    private float timeBetweenAttack;
    private float startTimeBetweenAttack = 1.0f;

    private Animator anim;
    public Animator playerAnim;

    public Transform punchPoint;
    public LayerMask enemyLayers;

    private int lightPunchDamage = 10;
    private float lightPunchRange = 0.2f;
    private int heavyPunchDamage = 20;
    private float heavyPunchRange = 0.1f;

    public HealthBarTwo healthBarTwo;

    private int maxHealth = 100;
    private int currentHealth;

    private int playerOneRounds;

    public Timer timer;
    private float timeLeft;

    private int playerTwoRounds = 0;

    public Round round;
    public RoundTwo roundTwo;
    public RoundThree roundThree;
    public RoundFour roundFour;
    public RoundFive roundFive;
    public RoundSix roundSix;

    public PlayerOne playerOne;

    private int roundsToWin;

    private bool defend = false;
    ///A boolean value to decide if the CPU is defending
    private int opponent;
    ///Stores the integer value that lets us know if the player is playing against another player or CPU
    private int decision;
    ///An integer value that will be used to decide the action of the CPU
    private float detectionRange = 0.3f;
    ///A detection range that will be used to decide what method to use

    private void Start()
    {
        opponent = PlayerPrefs.GetInt("Opponent");
        ///Gets the opponent value
        if (opponent == 1)
        ///This means the user chose to play against the player
        {
            Destroy(this);
            ///This script is not needed
        }
        else
        {
            timeLeft = PlayerPrefs.GetFloat("Time");
            roundsToWin = PlayerPrefs.GetInt("Rounds");
            anim = GetComponent<Animator>();
            currentHealth = maxHealth;
            healthBarTwo.SetMaxHealth(maxHealth);
            ///Initialisation like the PlayerOne and PlayerTwo class
        }
    }

    private void Update()
    {
        defend = false;
        ///The CPU is never in a defending position unless moving to the right
        MoveLeft();
        ///The CPU will constantly move left/forwards
        Detector();
        ///Will always detect if there are any enemies in range
    }

    private void MoveRight()
    {
        transform.Translate(Vector3.right * backSpeed * Time.deltaTime);
        defend = true;
        ///If moving back then defending
        Debug.Log("Defending");
    }

    private void MoveLeft()
    {
        transform.Translate(Vector3.left * forwardSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        if (timeBetweenAttack <= 0)
        {
            if (decision == 2)
            ///Uses the value of decision to determine the punch
            {
                LightPunch();
            }
            else if (decision == 3)
            {
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
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<PlayerOne>().TakeDamage(lightPunchDamage);
            Debug.Log("We hit " + enemy.name);
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
        }
    }

    private void Detector()
    {
        Collider2D[] detectEnemy = Physics2D.OverlapCircleAll(punchPoint.position, detectionRange, enemyLayers);
        ///Checks if there are enemies in the detectionRange
        foreach (Collider2D enemy in detectEnemy)
        {
            Decision();
            ///If there are then it uses the Decision method to choose what to do
        }
    }

    private void Decision()
    {
        decision = Random.Range(1, 4);
        ///Generates a random value from 1 inclusive to 4 exclusive
        if (decision == 1)
        ///If the decision is 1 then it will do the MoveRight method
        {
            MoveRight();
        }
        else
        ///Else it will do the attack method
        {
            Attack();
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
    {
        if (defend == true)
        ///If defending then you don't take damage
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
        playerOneRounds += 1;
        playerOne.SetPlayerOneRounds(playerOneRounds);
        UpdateRoundBars();
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
        SetCurrentHealth(maxHealth);
        healthBarTwo.SetHealth(maxHealth);
        if (timeLeft == 0)
        {
            return;
        }
        else
        {
            timer.SetTimeLeft(timeLeft);
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
        GameRestart();
    }

    private void GameRestart()
    {
        if (playerOneRounds == roundsToWin && playerTwoRounds == roundsToWin)
        {
            SceneManager.LoadScene("FinalScene");
        }
        else if (playerOneRounds == roundsToWin)
        {
            SceneManager.LoadScene("FinalScene");
        }
        else if (playerTwoRounds == roundsToWin)
        {
            SceneManager.LoadScene("FinalScene");
        }
    }
}
