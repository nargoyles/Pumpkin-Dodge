using UnityEngine;
using System.Collections;

public class Falling : MonoBehaviour
{
	public Transform pumpkin;
	public AudioClip[] audioclips;
	private AudioSource count;
	public Transform [] apple;
	private float spawnWait;
	private float maxTime = 200f;
	private float random;
	private float randomApple;
	private float maxHealth = 100f;
	private float curHealth;
	public GameObject healthBar;
	private bool gameRunning = true;
	private GameObject[] apples;
	private GameObject[] pumpkins;
	
	
	void Start()
	{
		count = GetComponent<AudioSource>();
		StartCoroutine (spawn());
		StartCoroutine (spawnApple ());
		curHealth = maxHealth;
		count = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter2D(Collider2D pump){
		if(pump.gameObject.tag=="pumpkin")
		{
			float bad = pump.transform.localScale.x * 10;
			curHealth -= bad * 5;
			Destroy (pump.gameObject);
			count.clip = audioclips[6];
			if(!count.isPlaying){
				count.Play();
			}
			if (curHealth <= 0 && Application.loadedLevel == 1) {
				Dead ();
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{

		if (col.gameObject.tag == "apple") {
			float good = col.transform.localScale.x * 20;
			Score.score += (int)good;
			curHealth += col.transform.localScale.x * 10; 
			Destroy(col.gameObject);
			count.clip = audioclips[Random.Range (0,6)];
			if(!count.isPlaying){
				count.Play();
			}
		}
	}
	
	void Update(){



		if (gameRunning) {
			healthBar.transform.localScale = new Vector3 (Mathf.Clamp (curHealth / maxHealth, 0f, 1f), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
			spawnWait = (maxTime - Time.timeSinceLevelLoad) * .01f;
			curHealth -= .07f;
			if (curHealth <= 0 && Application.loadedLevel == 1) {
				Dead ();
			}
		}

	}

	IEnumerator spawn()  {
		while (true) {

			for (int i = 0; i < 1; i++) {

				random = Random.Range (.3f, .8f);;
				Instantiate (pumpkin, new Vector3 (Random.Range (-9, 10), 6.5f), Quaternion.identity);
				pumpkin.transform.localScale = new Vector2 (random, random);
				yield return new WaitForSeconds (spawnWait-Random.Range(.001f, 2f));
			}
		}
	}

	IEnumerator spawnApple()  {
		while (true) {
			for (int i = 0; i < 3; i++) {
				int whichApple = Random.Range(0,3);
				randomApple = Random.Range (.4f, 1.5f);
				Instantiate (apple[whichApple], new Vector3 (Random.Range (-9, 10), 6.5f), Quaternion.identity);
				apple[whichApple].transform.localScale = new Vector2 (randomApple, randomApple);
				yield return new WaitForSeconds (spawnWait);
			}
		}
	}

	void Dead(){
		StopAllCoroutines ();
		apples = GameObject.FindGameObjectsWithTag("apple");
		pumpkins = GameObject.FindGameObjectsWithTag("pumpkin");
		foreach (GameObject a in apples){
			Destroy(a);
		}
		foreach (GameObject p in pumpkins){
			Destroy(p);
		}
		healthBar.transform.localScale = new Vector3 (0, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
		count.clip = audioclips [7];
		float wait = count.clip.length;
		count.Play();
		Invoke("EndScreen", wait);
		gameRunning = false;
	}

	void EndScreen(){
		Application.LoadLevel ("Dead");
	}
}
