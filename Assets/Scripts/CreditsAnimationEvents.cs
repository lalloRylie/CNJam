﻿using UnityEngine;
using System.Collections;
using System;

public class CreditsAnimationEvents : MonoBehaviour
{

    public void CloseCredits()
    {
        //GetComponent<Animator>().SetTime(0.0);
        gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Play(this Animation animation, string clipName, bool useTimeScale, Action onComplete)
    {
        Debug.Log("Overwritten Play animation, useTimeScale? " + useTimeScale);
        //We Don't want to use timeScale, so we have to animate by frame..
        if (!useTimeScale)
        {
            Debug.Log("Started this animation! ( " + clipName + " ) ");
            AnimationState _currState = animation[clipName];
            
            bool isPlaying = true;
            float _startTime = 0F;
            float _progressTime = 0F;
            float _timeAtLastFrame = 0F;
            float _timeAtCurrentFrame = 0F;
            float deltaTime = 0F;


            animation.Play(clipName);

            _timeAtLastFrame = Time.realtimeSinceStartup;
            while (isPlaying)
            {
                _timeAtCurrentFrame = Time.realtimeSinceStartup;
                deltaTime = _timeAtCurrentFrame - _timeAtLastFrame;
                _timeAtLastFrame = _timeAtCurrentFrame;

                _progressTime += deltaTime;
                _currState.normalizedTime = _progressTime / _currState.length;
                animation.Sample();

                //Debug.Log(_progressTime);

                if (_progressTime >= _currState.length)
                {
                    //Debug.Log("Bam! Done animating");
                    if (_currState.wrapMode != WrapMode.Loop)
                    {
                        //Debug.Log("Animation is not a loop anim, kill it.");
                        //_currState.enabled = false;
                        isPlaying = false;
                    }
                    else
                    {
                        //Debug.Log("Loop anim, continue.");
                        _progressTime = 0.0f;
                    }
                }

                yield return new WaitForEndOfFrame();
            }
            yield return null;
            if (onComplete != null)
            {
                Debug.Log("Start onComplete");
                onComplete();
                CloseCredits();
            }
        }
        else
        {
            animation.Play(clipName);
        }
    }
}
