using System;
using RPG.Saving;
using UnityEngine;

namespace RPG.Stats {

  public class Experience : MonoBehaviour, ISaveable {
    [SerializeField] float experiencePoints = 0f;

    public float ExperiencePoints => experiencePoints;
    public event Action onExperienceGained;

    public void GainExperience(float xp) {
      experiencePoints += xp;
      if (onExperienceGained != null) {
        onExperienceGained();
      }
    }
    public object CaptureState() {
      return experiencePoints;
    }

    public void RestoreState(object state) {
      experiencePoints = (float) state;
    }
  }
}
