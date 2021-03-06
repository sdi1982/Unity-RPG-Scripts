﻿using System.Collections;
using RPG.Core;
using RPG.Movement;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Combat
{
    public class PickupBase : MonoBehaviour, IRaycastable
    {
        [SerializeField] private OnPickupEvent onPickUp;

        [System.Serializable]
        public class OnPickupEvent : UnityEvent<string> { }

        public CursorType Cursor => CursorType.Pickup;

        private Collider m_ObjectCollider;

        private void Awake()
        {
            m_ObjectCollider = GetComponent<Collider>();
        }

        private IEnumerator HideForSeconds(float time)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(time);
            ShowPickup(true);
        }

        private void ShowPickup(bool shouldShow)
        {
            m_ObjectCollider.enabled = shouldShow;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(shouldShow);
            }
        }

        protected void Pickup(bool respawnable, float respawnTime)
        {
            if (respawnable)
            {
                StartCoroutine(HideForSeconds(respawnTime));
            }
            else
            {
                Destroy(gameObject);
            }

            onPickUp?.Invoke(gameObject.name);
        }

        public bool HandleRaycast(GameObject callingObject)
        {
            if (Input.GetMouseButtonDown(0))
            {
                callingObject.GetComponent<Mover>().StartMovement(transform.position, 1f);
            }

            return true;
        }
    }
}