using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GamemangerMessages
{
    EnemyGetTheBall,
    FriendGetTheBall,
    NeutralGetTheBall,
    TeacherSpotTheStudents
}

public class GameManager  
{

    private Character[] _arrayCharacters;
    
    public GameManager()
    {
        _arrayCharacters = new Character[5];
    }
    
    //Load Level
    
    //GetWalls
    
    //SetCharacters
    
    //SetTeacher
    
    //SetNodes
    
    //Manages Messages from characters
    private void ReceiveMessage(GamemangerMessages message)
    {
        switch(message)
        {
            case GamemangerMessages.EnemyGetTheBall:
            case GamemangerMessages.TeacherSpotTheStudents:
                Debug.Log("GameOver");
                break;
            case GamemangerMessages.NeutralGetTheBall:
                Debug.Log("NeutraGetTheBall");
                break;
            case GamemangerMessages.FriendGetTheBall:
                Debug.Log("FriendGetTheBall");
                break;
            
        }
    }
    
}
