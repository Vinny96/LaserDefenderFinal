using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatus : MonoBehaviour
{
    // configuration parameters




    // beta variables and configuration parameters
    [SerializeField] int pointsPerEnemy;
    [SerializeField] int score;
    [SerializeField] int currentScore;
    [SerializeField] TextMeshProUGUI scoreDisplay = null;
    // variables


    // methods

    private void Start()
    {
        resetScore();
        currentScore = score;
        scoreDisplay.text = score.ToString();
    }

    // beta methods
    
    private void displayScore()
    {
        scoreDisplay.text = currentScore.ToString();
    }

    private int resetScore()
    {
        score = 0;
        return score;
    }


    public int IncreaseAndGetScore()
    {
        score += pointsPerEnemy;
        currentScore = score;
        displayScore(); // testing. 
        return score;
    }
}





/*
 * this class will keep track of the players health and score
 * There are several things we did to keep track of the players health. We created this class which holds the TextMeshProUGUI element for the the scoreText. We also created a getter method in the player class
 * that will get the playersHealth. We then used this method within the displayHealth method to get the health from the player and display it in our playerHealthDisplay element. 
 * 
 * now what we want to do next is to update the score mechanism. 
 * 
 * 
 * Displaying the updated score after enemies have been destroyed is not working. 
 * 
 * 
 * Some things I discovered about gameStatus object. When creating the gameStatus object it is best if it is not prefabbed immediately. Attach the necessary gameObjects and then it is best to prefab it. If you prefab 
 * it and then attach the necessary gameObjects, you will encounter a bug in which the health is not appearing. 
 * 
 * next thing to work on is getting the score to update with each enemy gameObject that has been destroyed. 
 *  
 *  Something interesting is happening with the score mechanism. It is updating but the updated score is not displaying as each enemy is geting destroyed. It is updating when the game is over and it is not
 *  updating the score during the game. The same thing is now happening with the the health. It is not updating within the gameScreen. 
 *  
 *  
 *  I added debug.log(score) and what this proved was that the score variable itself is not updating. The score is not increasing and is constantly remaining at 10. What if we assign it some kinds of holder ?
 *  The above problem has been fixed. So all the enemy's are now triggers and the player laser is not a trigger. The incraese score method is also wrapped in an on triggerEnter2D method. I tried wrapping the 
 *  increaseScore method into a collisionEnter2D method and it worked just fine. However if we chose to go with the onTriggerEnter2D method, the enemy object stop firing for some reason. But when we go with a collisionEnter2D
 *  method, this is no longer the case. figure out why this is after game completion. 
 * 
 * Next thing we need to do is to reset the score to zero every time the game starts. Score is not resetting every time we start the game and it is holding the value from the previous session. now it is diferent but not working.
 * 
 * 
 * UPDATED NOTES JULY 27 2019
 * The score is not updating during the game. By update I mean updating the score display. The actual score variable that is on the gameStatus inspector is updating with each enemy destroyed
 * but the scoreDisplay is not updating the same time as the score variable on the gameStatus inspector. 
 * The health is having similar issues as within the inspector the player health is decreasing with ever enemy laser it has collided with but it is not updating on healthDisplay. 
 * Both may have the same issue ?? 
 * 
 * for now every other part of the game is going to get completed, those two issues will get resolved when everything else is completed. 
 * The plan that I have in mind is when each enemy object is destroyed is to play an audio effect and have a particle explosion effect. 
 * The particle explosion effect is working. The sound effects are also working as intended.
 * 
 * Now we have to fix the health and score issues. 
 * so the player health variable is decreasing when an enemy laser collides with the objet. But this is not reflecting in healthDisplay. 
 * The players health is still giivng issues as it will not display the updated health nor is it even displaying the health. What I am going to do instead is to implement the health displaying 
 * code in the players code section instead. Any variables associated with players health display will be moved to the player code. 
 * 
 * 
 * Updated notes August 2nd 2019 New Error
 * NullReferenceException: Object reference not set to an instance of an object
Enemy.OnCollisionEnter2D (UnityEngine.Collision2D collision) (at Assets/Scripts/Enemy.cs:58)The gamestatus object in the enemy code does not actually exist so we have to perform find object of type GameStatus then it should work. IT WORKS
EVERYTHING WORKS !!!!!!




}*/
