﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LAYER 
{
	public const float FRIEND = 8;
	public const float NEUTRAL = 9;
	public const float ENEMY = 10;
	public const float WALL = 11;		
	public const float MESSAGE = 12;
	public const float BLACKBOARD_WALL = 13;
	
}

public static class SCENE 
{
	public const string MAIN_MENU = "MainMenu";
	public const string LEVEL_ONE = "Level_1";
    public const string LEVEL_TWO = "Level_2";
    public const string LEVEL_THREE = "Level_3";
    public const string LEVEL_FOUR = "Level_4";
}

public static class MODEL
{
    public static GameManager GAME_MANAGER;
    public static GameStates GAME_STATE;
    public static SoundManager SOUND_MANAGER;  
}
