using System.Collections;
using System.Collections.Generic;
using d4160.GameFoundation;
using d4160.GameFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDCanvas : CanvasBase, IMultipleStatUpgradeable
{
    [Header("SCORE")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private string _scoreFormat = "Score: {0}";

    [Header("LIVES")]
    [SerializeField] private Image _livesImage;
    [SerializeField] private Sprite[] _livesSprites;
    [SerializeField] private GameObject _gameOverGO;
    [SerializeField] private GameObject _restartGO;

    protected override void Start()
    {
        base.Start();

        _gameOverGO?.SetActive(false);
        _restartGO?.SetActive(false);

        UpdateScore(0);
        UpdateLives(3);
    }

    public void UpdateStat(int index, float value)
    {
        if (index == 1) return;

        switch (index)
        {
            case 0:
                UpdateLives((int)value);
                break;

            case 2:
                UpdateScore((int)value);
                break;
        }
    }

    private void UpdateScore(int value)
    {
        if (_scoreText)
        {
            _scoreText.text = string.Format(_scoreFormat, value);
        }
    }

    private void UpdateLives(int value)
    {
        if (_livesImage)
        {
            _livesImage.sprite = _livesSprites[value];

            _gameOverGO?.SetActive(value == 0);
            _restartGO?.SetActive(value == 0);

            if (value == 0)
            {
                var man = SingleplayerModeManager.Instance.As<SingleplayerModeManager>();
                man?.InputActions.SetInputActive(true, 1);
                man?.InputActions.SetInputActive(false, 0);
            }
        }
    }
}
