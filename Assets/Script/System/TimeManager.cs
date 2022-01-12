using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
        [SerializeField] private float bulletTime = 0.1f;
        private float timeBeforePause;
    
        public void BulletTime()
        {
            Time.timeScale = bulletTime;
        }
        public void Pause()
        {
            timeBeforePause = Time.timeScale;
            Time.timeScale = 0f;
        }
        public void UnPause()
        {
            Time.timeScale = timeBeforePause;
        }
}
