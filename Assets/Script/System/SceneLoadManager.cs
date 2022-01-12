using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadManager : LongSing<SceneLoadManager>
{
        [SerializeField] private float EnterTime = 3f;
        [SerializeField] private float ExitTime = 2f;
        [SerializeField] private Image backimage;
        [SerializeField] private GameObject StartVFX;
        private Canvas thisCanvas;
        private Animation anim;
        private const string GamePlay = "GamePlay";
        private const string MainMenu = "MainMenu";
        private AsyncOperation loader;
        private WaitUntil loadUntil;
        private Color backimageColor;
        protected override void Awake()
        {
            anim = StartVFX.GetComponent<Animation>();
            thisCanvas = this.GetComponent<Canvas>();
            base.Awake();
        }

        private void OnEnable()
        {
            thisCanvas.worldCamera = Camera.main;
        }

        public void StartGame1()
        {
            StopAllCoroutines();
            StartCoroutine(SceneLoadCoroutine1(GamePlay));
        }
        public void StartGame2()
        {
            StopAllCoroutines();
            StartCoroutine(SceneLoadCoroutine2(GamePlay));
        }
        public void MainMenuBack()
        {
            StopAllCoroutines();
            StartCoroutine(SceneLoadCoroutine1(MainMenu));
        }
        /// <summary>
        /// 淡入淡出效果
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        private IEnumerator SceneLoadCoroutine1(string sceneName)
        {
            //SceneManager.LoadScene(Gameplay)
            loader = SceneManager.LoadSceneAsync(sceneName);
            loader.allowSceneActivation = false;
            backimage.gameObject.SetActive(true);
            while (backimage.color.a < 1f)
            {
                backimageColor.a = Mathf.Clamp01(backimageColor.a + Time.unscaledDeltaTime / EnterTime);
                backimage.color = backimageColor;
                yield return null;
            }
            yield return new WaitUntil(() => loader.progress >= 0.9f);
            loader.allowSceneActivation = true;
            while (backimageColor.a > 0f)
            {
                backimageColor.a = Mathf.Clamp01(backimageColor.a - Time.unscaledDeltaTime / ExitTime);
                backimage.color = backimageColor;
                yield return null;
            }
            backimage.gameObject.SetActive(false);
        }

        private IEnumerator SceneLoadCoroutine2(string sceneName)
        {
            anim.gameObject.SetActive(true);
            loader = SceneManager.LoadSceneAsync(sceneName);
            loader.allowSceneActivation = false;
            anim.Play("start");
            while (anim.IsPlaying("start"))
            {
                yield return null;
            }
            yield return new WaitUntil(() => loader.progress >= 0.9f);
            loader.allowSceneActivation = true;
            anim.Play("startEnd");
            while (anim.IsPlaying("startEnd"))
            {
                yield return null;
            }
            anim.gameObject.SetActive(false);
        }
}
