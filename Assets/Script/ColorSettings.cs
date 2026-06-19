using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// カラー選択をenumで管理
public enum CarColor
{
    White,
    Cyan,
    Blue,
    Magenta,
    Red,
    Green,
    Yellow,
    Black
}

public class ColorSettings : MonoBehaviour
{
    // レンダラーの参照
    [SerializeField] private Renderer carRenderer;

    // 選択中のカラー選択
    [SerializeField] private static CarColor currentColor;

    // カラー選択リスト
    private List<Color> colors;

    // シーンチェック
    private bool titleCheck;

    private void Start()
    {
        // カラーリングの初期設定
        colors = new List<Color>
        {
            Color.white,
            Color.cyan,
            Color.blue,
            Color.magenta,
            Color.red,
            Color.green,
            Color.yellow,
            Color.black
        };
        ColorSelection();
    }

    private void Update()
    {
        // タイトル画面以外ではカラー選択ができないように制限
        if (SceneManager.GetActiveScene().name != "TitleScene")
        {
            return;
        }

        // 右で昇順 (→ D)    左で降順 (← A)
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame || Keyboard.current.dKey.wasPressedThisFrame)
        {
            NextSelection();
        }
        else if (Keyboard.current.leftArrowKey.wasPressedThisFrame || Keyboard.current.aKey.wasPressedThisFrame)
        {
            // 同時に動かないようにelse if
            PreviousSelection();
        }
    }

    // カラー選択設定
    private void ColorSelection()
    {
        // 数字で切り替えられるようにintに変換する (メソッド内のみ可能)
        int index = (int)currentColor;

        // マテリアルに反映してカラーリング変更
        carRenderer.material.color = colors[index];
    }

    // 次の選択
    private void NextSelection()
    {
        // 数字で切り替えられるようにintに変換する (メソッド内のみ可能)
        int index = (int)currentColor;

        // 昇順で上がっていく
        index++;

        // 最大値を越えたなら0から
        if (index >= colors.Count)
        {
            index = 0;
        }

        // 今度は数字をカラーに変換する (メソッド内のみ可能)
        currentColor = (CarColor)index;

        // 反映して更新させる
        ColorSelection();
    }

    // 前の選択
    private void PreviousSelection()
    {
        // 数字で切り替えられるようにintに変換する (メソッド内のみ可能)
        int index = (int)currentColor;

        // 降順で下がっていく
        index--;

        // 0を越えたなら最大値から
        if (index < 0)
        {
            // インデックスは0から数えるから全体からマイナス 1すれば同じになる
            index = colors.Count - 1;
        }

        // 今度は数字をカラーに変換する (メソッド内のみ可能)
        currentColor = (CarColor)index;

        // 反映して更新させる
        ColorSelection();
    }
}