using UnityEngine;

public class FootstepSound : MySound
{
    public AudioClip[] footstepSounds_grass_left;
    public AudioClip[] footstepSounds_grass_right;
    public FootstepSound()
    {
        // Initialize the footstep sounds if needed
        footstepSounds_grass_left = Resources.LoadAll<AudioClip>("Audio/Footsteps/Grass/Left");
        footstepSounds_grass_right = Resources.LoadAll<AudioClip>("Audio/Footsteps/Grass/Right");
        if (footstepSounds_grass_left.Length == 0 || footstepSounds_grass_right.Length == 0)
        {
            Debug.LogWarning("Footstep sounds not found. Please check the Resources folder.");
        }
    }
    public override void PlayerLeftSound(Transform transform)
    {
        if (footstepSounds_grass_left.Length > 0)
        {
            int randomIndex = Random.Range(0, footstepSounds_grass_left.Length);
            AudioSource.PlayClipAtPoint(footstepSounds_grass_left[randomIndex], transform.position);
        }
    }
    public override void PlayerRightSound(Transform transform)
    {
        if (footstepSounds_grass_right.Length > 0)
        {
            int randomIndex = Random.Range(0, footstepSounds_grass_right.Length);
            AudioSource.PlayClipAtPoint(footstepSounds_grass_right[randomIndex], transform.position);
        }
    }
}
