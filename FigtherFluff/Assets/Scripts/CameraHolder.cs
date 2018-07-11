using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
  public class CameraHolder : MonoBehaviour
    {
        public delegate void PreCullEvent(Transform camera);
        public static PreCullEvent onPreCull;

        void OnPreCull()
        {
            if (onPreCull != null)
            {
                onPreCull(transform);
            }
        }

        internal IEnumerator ShaketCoroutine(float duration, float magnitude)
        {
            Vector3 originalPos = transform.localPosition;

            float elapsed = 0;

            while (elapsed < duration)
            {
                float x = UnityEngine.Random.Range(-1, 1) * magnitude;
                float y = UnityEngine.Random.Range(-1, 1) * magnitude;

                transform.localPosition = new Vector3(x, y, originalPos.z);

                elapsed += Time.unscaledDeltaTime;
                yield return null;
            }

            transform.localPosition = originalPos;
        }
    }
}
