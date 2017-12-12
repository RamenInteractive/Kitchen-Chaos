using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticket : Item {
    public GameObject FireParticleSystem;
    public GameObject FireSmokeParticleSystem;
    public GameObject LightSmokeParticleSystem;
    public GameObject paper;

    private TicketVisualState curState;

    public enum TicketVisualState {
        Normal,
        Smoking,
        Flaming
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void updateTicketState(float time) {
        TicketVisualState newState = TicketVisualState.Normal;
        if (time > 0.75f) {
            // If 75% of the time has passed, the ticket should be ON FIRE
            newState = TicketVisualState.Flaming;
        }
        else if (time > 0.5f) {
            // If half the time has passed, the ticket should be smoking
            newState = TicketVisualState.Smoking;
        }
        if (curState != newState) {
            // Clear out existing particle systems
            removeParticleEffects();
            Renderer r = paper.GetComponent<Renderer>();
            switch (newState) {
                case TicketVisualState.Smoking:
                    Instantiate(LightSmokeParticleSystem, transform);
                    r.material.color = new Color(0.93f, 0.82f, 0.63f);
                    break;
                case TicketVisualState.Flaming:
                    Instantiate(FireSmokeParticleSystem, transform);
                    Instantiate(FireParticleSystem, transform);
                    r.material.color = new Color(0.85f, 0.3f, 0.25f);
                    break;
            }
        }
        curState = newState;
    }

    public void removeParticleEffects() {
        ParticleSystem[] systems = GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < systems.Length; i++) {
            ParticleEffect eff = systems[i].GetComponent<ParticleEffect>();
            if (eff != null) {
                eff.Detach();
            }
            else {
                Destroy(systems[i].gameObject);
            }
        }
    }
}
