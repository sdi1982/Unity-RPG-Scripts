﻿using System.Collections;
using RPG.Attributes;
using RPG.Control;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat {
    public class PickupBase : MonoBehaviour, IRaycastable {

        public CursorType Cursor =>
            CursorType.Pickup;

        private Collider objectCollider;

        private void Awake() {
            objectCollider = GetComponent<Collider>();
        }

        private IEnumerator HideForSeconds(float time) {
            ShowPickup(false);
            yield return new WaitForSeconds(time);
            ShowPickup(true);
        }

        private void ShowPickup(bool shouldShow) {
            objectCollider.enabled = shouldShow;
            for (int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).gameObject.SetActive(shouldShow);
            }
        }

        public void Pickup(Health health, GameObject healFX, float healthPercentToRestore, bool respawnable, float respawnTime) {
            Instantiate(healFX, health.transform);
            health.Heal(healthPercentToRestore);
            if (respawnable) {
                StartCoroutine(HideForSeconds(respawnTime));
            } else {
                Destroy(gameObject);
            }
        }

        public void Pickup(Fighter fighter, WeaponConfig weapon, bool respawnable, float respawnTime) {
            fighter.EquipWeapon(weapon);
            if (respawnable) {
                StartCoroutine(HideForSeconds(respawnTime));
            } else {
                Destroy(gameObject);
            }
        }

        public bool HandleRaycast(PlayerController callingController) {
            if (Input.GetMouseButtonDown(0)) {
                callingController.GetComponent<Mover>().StartMovement(transform.position, 1f);
            }
            return true;
        }
    }

}