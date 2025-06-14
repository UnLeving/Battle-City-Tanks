using UnityEngine;

namespace Player
{
    public class PlayerLivesModel : MonoBehaviour
    {
        public int CurrentLives { get; private set; }

        public PlayerLivesModel(int initialLives)
        {
            CurrentLives = initialLives;
        }

        public void LoseLife()
        {
            if (CurrentLives > 0)
            {
                CurrentLives--;
                
                //Debug.Log($"Lives left: {CurrentLives}");
            }
        }

        public void GainLife()
        {
            CurrentLives++;
            
            //Debug.Log($"Lives gained. Total: {CurrentLives}");
        }
    }
}