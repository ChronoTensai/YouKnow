using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teacher : Enemy {

	public Node StartNode;
    public Node[] FollowNode;
    private Node ToFollow;
    private Node LastNodeVisited;

    private bool desicion = false;
    public float speed = 20f;
    private float closeEnough = 0.2f;


    void Start () {
        if (StartNode)
        {
            //No se como hacer que mire en el sentido que se va a dirigir
            //como que no? si lo tenias  bien en move xD
            this.transform.SetPositionAndRotation(StartNode.transform.position, Quaternion.LookRotation(StartNode.transform.position - this.transform.position, transform.TransformDirection(Vector3.forward)));
        }
        else
        {
            this.transform.SetPositionAndRotation(FollowNode[0].transform.position, Quaternion.LookRotation(FollowNode[0].transform.position - this.transform.position, transform.TransformDirection(Vector3.forward)));
        }
        ToFollow = FollowNode[0];

    }
	
	
	
	void Update()
	{
	    if(MODEL.GAME_STATE == GameStates.Playing)
	    {
	        if(SearchForPaper())
	        {
                Destroy(GameObject.FindGameObjectWithTag("Message"));
                MODEL.SOUND_MANAGER.PlayAudio(this.CatchAudio);
	            MODEL.GAME_MANAGER.YouLose();
	        }
	        else
	        {
	            Move();
	            LookAtNode(ToFollow);
	        }
	        
	    }
    }
	
	bool SearchForPaper()
	{
	   bool findMessage = false;

       //Nos fijamos si el mensaje esta en la escena
	   GameObject go = GameObject.FindGameObjectWithTag("Message");
       if(go != null)
       {
            //Nos fijamos si el papel esta en frende de la profesora
            Vector2 teacherFront = transform.TransformDirection(Vector2.up);
            Vector2 direction = go.transform.position - transform.position;
            findMessage = Vector2.Dot(teacherFront, direction) > 0;
            
       }
	   
	   return findMessage;
	}

   


    void Move()
    {
        if (ToFollow)
        {
            //Movemos el codigo que estaba aca a una funcion
            //Sacamos esta cuenta no hace falta porque la profesora ya esta mirando asi que le decimos que avance para adelante nomas
            //Bueno adelante arriba para este caso es arriba
            this.transform.Translate(Vector3.up * Time.deltaTime * speed);
            
            //Estamos trabajando en 2d no necesitamos el calculo de la tercera dimension incluso si los nodos estan mal puestos puede pasar que nunca llegue a acercarse lo suficiente
            if (Vector2.Distance(this.transform.position, ToFollow.transform.position) <= closeEnough)
            {
               
                // Ojo con esto el random puede dar cualquier cosa y trabar el juego por un rato largo (me paso :P)
                // agregamos unos check safe 

                //Acostumbrate a usar base 0
                //Ponemos 4 ya que hay pocas probabilidaes que toque el numero 3 si hacemos un random de float hasta 3.
                int nodeIdSelected = Mathf.FloorToInt(Random.Range(0, 4));
                //Nos aseguramos de que no sea 4 solo por las dudas :P
                nodeIdSelected = nodeIdSelected == 4 ? 3 : nodeIdSelected;
                SetNewNode(nodeIdSelected);
            }
        }
    }

    private void SetNewNode(int nodeId)
    {
        Node newNode = null;

        switch (nodeId)
        {
            case 0:
                if (ToFollow.Top != null && !ToFollow.Top.LastVisited)
                {
                    newNode = ToFollow.Top;
                }                
                break;
            case 1:
                if (ToFollow.Right && !ToFollow.Right.LastVisited)
                {
                    newNode = ToFollow.Right;
                }
                break;
            case 2:
                if (ToFollow.Bot && !ToFollow.Bot.LastVisited)
                {
                      newNode = ToFollow.Bot;
                }
                break;
            case 3:
                if (ToFollow.Left && !ToFollow.Left.LastVisited)
                {
                   newNode = ToFollow.Left;              
                }
                break;
        }


        if (newNode == null)
        {
            //Si tenemos mala suerte con el random que vaya al siguiente
            //Si nos pasamos de 3 vamos al primero
            if (nodeId != 3)
                SetNewNode(nodeId + 1);
            else
                SetNewNode(0);
        }
        else
        {
            //Seteamos el nuevo nodo y al last visited viejo le decimos que ya no lo es
            if (LastNodeVisited != null)
            {
                LastNodeVisited.LastVisited = false;
            }

            //Ahora el nodo actual es el viejo
            LastNodeVisited = ToFollow;
            LastNodeVisited.LastVisited = true;

            //Seteamos el nuevo nodo
            ToFollow = newNode;
        }
    }
    
    private void LookAtNode(Node node)
    {
        //Asi funciona bien :)
        Quaternion rotation = Quaternion.LookRotation(node.transform.position - this.transform.position, transform.TransformDirection(Vector3.back));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
    }
}
