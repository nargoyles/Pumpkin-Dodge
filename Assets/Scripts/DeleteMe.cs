using UnityEngine;
using System.Collections;

public class DeleteMe : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag=="pumpkin"){
			Destroy (col.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag=="apple"){
			Destroy (col.gameObject);
		}
	}
}
