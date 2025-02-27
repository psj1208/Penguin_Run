using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FadeHelper
{
    /// <summary>
    /// CanvasGroup 컴포넌트를 이용한 Fade In/Out 메서드
    /// </summary>
    /// <param name="fader">CanvasGroup 컴포넌트</param>
    /// <param name="startAlpha">시작 알파값</param>
    /// <param name="endAlpha">최종 알파값</param>
    /// <param name="fadeTime">연출 지속 시간</param>
    /// <param name="onComplete">연출 효과 종료 시 호출할 메서드</param>
    public static IEnumerator Fade(CanvasGroup fader, float startAlpha, float endAlpha, float fadeTime, System.Action onComplete = null)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            fader.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeTime);
            yield return null;
        }
        fader.alpha = endAlpha;
        onComplete?.Invoke();
    }

    /// <summary>
    /// CanvasGroup 컴포넌트를 이용한 Fade In/Out 메서드
    /// </summary>
    /// <param name="fader">CanvasGroup 컴포넌트</param>
    /// <param name="fadeTime">Fade In-Out 연출 지속 시간</param>
    public static IEnumerator FadeSequence(CanvasGroup fader, float fadeTime)
    {
        yield return Fade(fader, 0f, 1f, fadeTime * 0.5f);
        yield return Fade(fader, 1f, 0f, fadeTime * 0.5f);
    }
}
