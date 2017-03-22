using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the sequencing of the boss's attacks, health, animations, etc.
/// </summary>
public class ChefBossController : MonoBehaviour {

    private Animator _animator;
    public Animator animator {
        get{ return _animator ?? (_animator = GetComponentInChildren<Animator> ()); }
    }

    private float maxHealth = 100;
    public float Health;

	// Use this for initialization
	void Start () {
        Health = maxHealth;
        StartCoroutine (AttackSequence());	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    #region health and damage

    public void TakeDamage( float damage ){
        
        //subtract damage from health
        Health = Mathf.Max (0, Health - damage);

        //check for death
        if (Health <= 0)
            Die ();
    }

    void Regenerate(){

    }

    public void Die(){

    }

    #endregion

    #region attacks

    /// <summary>
    /// Basic attack sequencing based on health of the boss
    /// </summary>
    /// <returns>The sequence.</returns>
    IEnumerator AttackSequence(){
        if (Health > maxHealth * 0.65f) {
            //65-100% health
            if (Random.value > 0.75f) { ChopGarlic(); } else { PepperCloud(); }

            yield return new WaitForSeconds(4.0f);
        } else if (Health > maxHealth * 0.45f) {
            //45-65% health
            if (Random.value > 0.5f) { FriedHeart(); } else { ChopGarlic(); }

            yield return new WaitForSeconds(4.0f);
        } else if (Health > maxHealth * 0.2f) {
            //1-20% health
            if (Random.value > 0.8f) { FriedHeart(); } else { PepperCloud(); }

            yield return new WaitForSeconds(4.0f);
        }
    }

    public void ChopGarlic(){

    }

    public void PepperCloud(){

    }

    public void FriedHeart(){

    }

    #endregion
}
