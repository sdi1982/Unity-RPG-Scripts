using System;
using RPG.Saving;
using UnityEngine;

namespace RPG.Stats {

  public class Experience : MonoBehaviour, ISaveable {
    [SerializeField] float experiencePoints = 0f;

    public event Action onExperienceGained;

    public void GainExperience(float xp) {
      experiencePoints += xp;
      onExperienceGained();
    }
    public object CaptureState() {
      return experiencePoints;
    }

    public void RestoreState(object state) {
      experiencePoints = (float) state;
    }

    public float ExperiencePoints {
      get {
        return experiencePoints;
      }
    }
  }
}