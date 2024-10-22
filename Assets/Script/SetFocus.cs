﻿using UnityEngine;
using System.Collections;
/*AR摄像机自动对焦*/
namespace Vuforia {
  public class SetFocus : MonoBehaviour {

    void Start() {
      VuforiaBehaviour.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
      VuforiaBehaviour.Instance.RegisterOnPauseCallback(OnPaused);
    }

    private void OnVuforiaStarted() {
      CameraDevice.Instance.SetFocusMode(
          CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    }

    private void OnPaused(bool paused) {
      if (!paused) // resumed
    {
        // Set again autofocus mode when app is resumed
        CameraDevice.Instance.SetFocusMode(
            CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
      }
    }
  }
}

