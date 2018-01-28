using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	public Node Top;
	public Node Left;
	public Node Right;
	public Node Bot;
	
	public bool LastVisited;
	public bool IsBlackBoard;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.25f);
    }
}
