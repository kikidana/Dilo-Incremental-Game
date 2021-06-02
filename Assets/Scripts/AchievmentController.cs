using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievmentController : MonoBehaviour
{
    // instance ini mirip seperti pada GameManager, fungsinya adalah membuat sistem singelton
    // untuk memudahkan pemanggilan script yang bersifat manager dari scirpt lain

    private static AchievmentController _instance = null;
    public static AchievmentController Instance
    {
        get
        {
            {
                if(_instance == null)
                {
                    _instance = FindObjectOfType<AchievmentController>();
                }

                return _instance;
            }
        }
    }

    [SerializeField] private Transform _popUpTransform;
    [SerializeField] private Text _popUpText;
    [SerializeField] private float _popUpShowDuration = 3f;
    [SerializeField] private List<AchievementData> _achievmentList;

    private float _popUpShowDurationCounter;

    // Update is called once per frame
    void Update()
    {
        if (_popUpShowDurationCounter > 0)
        {
            // Kurangi durasi ketika pop up durasi lebih dari 0
            _popUpShowDurationCounter -= Time.unscaledDeltaTime;

            // Lerp adalah fungsi liner interpolation, digunakan untuk mengubah value secara perlahan
            _popUpTransform.localScale = Vector3.LerpUnclamped(_popUpTransform.localScale, Vector3.one, 0.5f);
        }
        else
        {
            _popUpTransform.localScale = Vector2.LerpUnclamped(_popUpTransform.localScale, Vector3.right, 0.5f);
        }
    }

    public void UnlockAchievment(AchievementType type, string value)
    {
        // mencari data achievment 
        AchievementData achievment = _achievmentList.Find(a => a.Type == type && a.Value == value);

        if(achievment != null && !achievment.IsUnlocked)
        {
            achievment.IsUnlocked = true;
            ShowAchievmentPopUp(achievment);
        }
    }

    private void ShowAchievmentPopUp(AchievementData achievment)
    {
        _popUpText.text = achievment.Title;
        _popUpShowDurationCounter = _popUpShowDuration;
        _popUpTransform.localScale = Vector2.right;
    }
}

// System.Serialize digunakan agar object dari script bisa di-serialize
// dan bisa di-inputkan dari inspector, jika tidak terdapat ini, maka variable tidak akan muncul di inspector

[System.Serializable]

public class AchievementData
{
    public string Title;
    public AchievementType Type;
    public string Value;
    public bool IsUnlocked;
}

public enum AchievementType
{
    UnlockResource
}