using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;

public class ProgressBar : MonoBehaviour
{
    public SpriteRenderer progress;
    public TMP_Text text;

    public float progressValue = 0f;
    public float animationSpeed = 1f;

    public int displayMin = 0;
    public int displayMax = 100;

    private float width;

    public Func<int, int, string> displayProgressString;
    
    private void Start()
    {
        width = progress.size.x;
        displayProgressString = defaultDisplayProgressString;
    }

    private void Update()
    {
        progress.size = new Vector2(progressValue * width, progress.size.y);
    }

    public async UniTask setDisplayProgress(int displayProgress)
    {
        if (text != null)
        {
            text.text = displayProgressString(displayProgress, displayMax);    
        }
        await setProgress((float)(displayProgress - displayMin) / (displayMax - displayMin));
    }
    
    private async UniTask setProgress(float finalProgressValue)
    {
        finalProgressValue = Mathf.Clamp(finalProgressValue, 0f, 1f);
        var startProgress = progressValue;
        var t = 0f;
        while (!Mathf.Approximately(progressValue, finalProgressValue))
        {
            t += animationSpeed * Time.fixedDeltaTime;
            progressValue = Mathf.Lerp(startProgress, finalProgressValue, t);
            await UniTask.WaitForFixedUpdate();
        }
    }

    private string defaultDisplayProgressString(int currentProgress, int maxProgress)
    {
        return currentProgress + "/" + maxProgress;
    }
}
