using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFB_GazerDemo : MonoBehaviour {

	public Animator animator;

	public ParticleSystem[] Cast1Particles;
	public Light[] Cast1Lights;
	public ParticleSystem[] Cast2Particles;
	public Light[] Cast2Lights;
	public ParticleSystem[] Cast3Particles;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}

	public void Locomotion (float value) {
		animator.SetFloat ("Locomotion", value);
	}


    private void Update()
    {
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			animator.SetTrigger("Cast2");
            
        }
    }
    public void IdleType (float value) {
		animator.SetFloat ("IdleType", value);
	}

	public void Cast1Start(){
		for (int i = 0; i < Cast1Particles.Length; i++) {
			ParticleSystem.EmissionModule module = Cast1Particles [i].emission;
			module.enabled = true;
		}
		for (int i = 0; i < Cast1Lights.Length; i++) {
			Cast1Lights [i].enabled = true;
		}
	}

	public void Cast1End(){
		for (int i = 0; i < Cast1Particles.Length; i++) {
			ParticleSystem.EmissionModule module = Cast1Particles [i].emission;
			module.enabled = false;
		}
		for (int i = 0; i < Cast1Lights.Length; i++) {
			Cast1Lights [i].enabled = false;
		}
	}

	public void Cast2Start(){
		for (int i = 0; i < Cast2Particles.Length; i++) {
			ParticleSystem.EmissionModule module = Cast2Particles [i].emission;
			module.enabled = true;
		}
		for (int i = 0; i < Cast2Lights.Length; i++) {
			Cast2Lights [i].enabled = true;
		}
	}

	public void Cast2End(){
		for (int i = 0; i < Cast2Particles.Length; i++) {
			ParticleSystem.EmissionModule module = Cast2Particles [i].emission;
			module.enabled = false;
		}
		for (int i = 0; i < Cast2Lights.Length; i++) {
			Cast2Lights [i].enabled = false;
		}
	}

	public void Cast3Start(){
		for (int i = 0; i < Cast3Particles.Length; i++) {
			ParticleSystem.EmissionModule module = Cast3Particles [i].emission;
			module.enabled = true;
		}
	}

	public void Cast3End(){
		for (int i = 0; i < Cast3Particles.Length; i++) {
			ParticleSystem.EmissionModule module = Cast3Particles [i].emission;
			module.enabled = false;
		}
	}
}
