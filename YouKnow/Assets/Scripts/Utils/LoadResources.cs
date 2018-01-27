using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadResources  {

    private static Transform canvas;
    private static Transform game;
    
    public static void SetParents()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
        game = GameObject.FindGameObjectWithTag("Game").transform;        
    }
        
    public static void LoadUIResource(string name)
    {
        string path = "UI/"+ UnityEngine.SceneManagement.SceneManager.GetActiveScene().name +"/"+ name;
        GameObject go = (GameObject) Resources.Load(path);
        go = (GameObject) GameObject.Instantiate(go, go.transform.position,Quaternion.identity);
        go.transform.SetParent(canvas, false);
	}
    
    public static GameObject LoadGameResource(string name, int position = 0)
    {
        string path = "Game/"+ UnityEngine.SceneManagement.SceneManager.GetActiveScene().name +"/"+ name;
        GameObject ob = (GameObject) GameObject.Instantiate(Resources.Load(path));
        ob.transform.SetParent(game);
        
        if(position != 0)
        {
            ob.GetComponent<SpriteRenderer>().sortingOrder = position;
        }
        
        return ob;
    }
    
    
    public static T LoadGameResourceAndGetComponent<T>(string name)
    {
        string path = "Game/"+ UnityEngine.SceneManagement.SceneManager.GetActiveScene().name +"/"+ name;
        GameObject ob = (GameObject) GameObject.Instantiate(Resources.Load(path));
        ob.transform.SetParent(game);        
        return ob.GetComponent<T>();
    }
}
