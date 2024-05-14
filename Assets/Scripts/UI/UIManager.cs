using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{   
    public PlayerStatebar playerStatebar;
    public CharactorEventSO healthChangeEvent;

    private void OnEnable() {
        healthChangeEvent.OnCharactorEventRaised += OnHealthChangeEvent;
    }

    private void OnDisable() {
        healthChangeEvent.OnCharactorEventRaised -= OnHealthChangeEvent;
    }


    public void OnHealthChangeEvent(Character character){
        playerStatebar.ChangeHealth(character.currentHealth/character.maxHealth);
    }
}
