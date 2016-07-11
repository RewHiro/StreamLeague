
//using UnityEngine;
//using UnityEditor;
//using System.Collections;

////http://unitygeek.hatenablog.com/entry/2012/08/11/164525

///// <summary>
///// 軸のタイプ
///// </summary>
//public enum AxisType
//{
//    KeyOrMouseButton = 0,
//    MouseMovement = 1,
//    JoystickAxis = 2
//};

///// <summary>
///// 入力の情報
///// </summary>
//public class InputAxis
//{
//    public string name = "";
//    public string descriptiveName = "";
//    public string descriptiveNegativeName = "";
//    public string negativeButton = "";
//    public string positiveButton = "";
//    public string altNegativeButton = "";
//    public string altPositiveButton = "";

//    public float gravity = 0;
//    public float dead = 0;
//    public float sensitivity = 0;

//    public bool snap = false;
//    public bool invert = false;

//    public AxisType type = AxisType.KeyOrMouseButton;

//    // 1から始まる。
//    public int axis = 1;
//    // 0なら全てのゲームパッドから取得される。1以降なら対応したゲームパッド。
//    public int joyNum = 0;

//    /// <summary>
//    /// 押すと1になるキーの設定データを作成する
//    /// </summary>
//    /// <returns>The button.</returns>
//    /// <param name="name">Name.</param>
//    /// <param name="positiveButton">Positive button.</param>
//    /// <param name="altPositiveButton">Alternate positive button.</param>
//    public static InputAxis CreateButton(string name, string positiveButton, string altPositiveButton)
//    {
//        var axis = new InputAxis();
//        axis.name = name;
//        axis.positiveButton = positiveButton;
//        axis.altPositiveButton = altPositiveButton;
//        axis.gravity = 1000;
//        axis.dead = 0.001f;
//        axis.sensitivity = 1000;
//        axis.type = AxisType.KeyOrMouseButton;

//        return axis;
//    }

//    /// <summary>
//    /// ゲームパッド用の軸の設定データを作成する
//    /// </summary>
//    /// <returns>The joy axis.</returns>
//    /// <param name="name">Name.</param>
//    /// <param name="joystickNum">Joystick number.</param>
//    /// <param name="axisNum">Axis number.</param>
//    public static InputAxis CreatePadAxis(string name, int joystickNum, int axisNum)
//    {
//        var axis = new InputAxis();
//        axis.name = name;
//        axis.dead = 0.2f;
//        axis.sensitivity = 1;
//        axis.type = AxisType.JoystickAxis;
//        axis.axis = axisNum;
//        axis.joyNum = joystickNum;

//        return axis;
//    }

//    /// <summary>
//    /// キーボード用の軸の設定データを作成する
//    /// </summary>
//    /// <returns>The key axis.</returns>
//    /// <param name="name">Name.</param>
//    /// <param name="negativeButton">Negative button.</param>
//    /// <param name="positiveButton">Positive button.</param>
//    /// <param name="axisNum">Axis number.</param>
//    public static InputAxis CreateKeyAxis(string name, string negativeButton, string positiveButton, string altNegativeButton, string altPositiveButton)
//    {
//        var axis = new InputAxis();
//        axis.name = name;
//        axis.negativeButton = negativeButton;
//        axis.positiveButton = positiveButton;
//        axis.altNegativeButton = altNegativeButton;
//        axis.altPositiveButton = altPositiveButton;
//        axis.gravity = 3;
//        axis.sensitivity = 3;
//        axis.dead = 0.001f;
//        axis.type = AxisType.KeyOrMouseButton;

//        return axis;
//    }
//}

///// <summary>
///// InputManagerを設定するためのクラス
///// </summary>
//public class InputManagerGenerator
//{

//    //http://whaison.jugem.jp/?eid=761
//    SerializedObject serializedObject;
//    //http://docs.unity3d.com/ja/current/ScriptReference/SerializedProperty.html
//    SerializedProperty axesProperty;

//    /// <summary>
//    /// コンストラクタ
//    /// </summary>
//    public InputManagerGenerator()
//    {
//        // InputManager.assetをシリアライズされたオブジェクトとして読み込む
//        // http://docs.unity3d.com/ja/current/ScriptReference/AssetDatabase.html
//        serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
//        //http://git.burgzergarcade.net/Petey/Unity-5-Lighting-Example/blob/3292420153e3c510a69e3001b066116b3c069d32/Unity%205%20Lighting%20Example/ProjectSettings/InputManager.asset
//        axesProperty = serializedObject.FindProperty("m_Axes");
//    }

//    /// <summary>
//    /// 軸を追加します。
//    /// </summary>
//    /// <param name="serializedObject">Serialized object.</param>
//    /// <param name="axis">Axis.</param>
//    public void AddAxis(InputAxis axis)
//    {
//        if (axis.axis < 1) Debug.LogError("Axisは1以上に設定してください。");
//        SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");

//        axesProperty.arraySize++;
//        serializedObject.ApplyModifiedProperties();

//        SerializedProperty axisProperty = axesProperty.GetArrayElementAtIndex(axesProperty.arraySize - 1);

//        GetChildProperty(axisProperty, "m_Name").stringValue = axis.name;
//        GetChildProperty(axisProperty, "descriptiveName").stringValue = axis.descriptiveName;
//        GetChildProperty(axisProperty, "descriptiveNegativeName").stringValue = axis.descriptiveNegativeName;
//        GetChildProperty(axisProperty, "negativeButton").stringValue = axis.negativeButton;
//        GetChildProperty(axisProperty, "positiveButton").stringValue = axis.positiveButton;
//        GetChildProperty(axisProperty, "altNegativeButton").stringValue = axis.altNegativeButton;
//        GetChildProperty(axisProperty, "altPositiveButton").stringValue = axis.altPositiveButton;
//        GetChildProperty(axisProperty, "gravity").floatValue = axis.gravity;
//        GetChildProperty(axisProperty, "dead").floatValue = axis.dead;
//        GetChildProperty(axisProperty, "sensitivity").floatValue = axis.sensitivity;
//        GetChildProperty(axisProperty, "snap").boolValue = axis.snap;
//        GetChildProperty(axisProperty, "invert").boolValue = axis.invert;
//        GetChildProperty(axisProperty, "type").intValue = (int)axis.type;
//        GetChildProperty(axisProperty, "axis").intValue = axis.axis - 1;
//        GetChildProperty(axisProperty, "joyNum").intValue = axis.joyNum;

//        serializedObject.ApplyModifiedProperties();

//    }

//    /// <summary>
//    /// 子要素のプロパティを取得します。
//    /// </summary>
//    /// <returns>The child property.</returns>
//    /// <param name="parent">Parent.</param>
//    /// <param name="name">Name.</param>
//    private SerializedProperty GetChildProperty(SerializedProperty parent, string name)
//    {
//        SerializedProperty child = parent.Copy();
//        child.Next(true);
//        do
//        {
//            if (child.name == name) return child;
//        }
//        while (child.Next(false));
//        return null;
//    }

//    /// <summary>
//    /// 設定を全てクリアします。
//    /// </summary>
//    public void Clear()
//    {
//        axesProperty.ClearArray();
//        serializedObject.ApplyModifiedProperties();
//    }
//}

//public class InputManagerSetter
//{


//    /// <summary>
//    /// インプットマネージャーを再設定します。
//    /// </summary>
//    [MenuItem("Util/Reset InputManager")]
//    public static void ResetInputManager()
//    {
//        Debug.Log("インプットマネージャーの設定を開始します。");
//        InputManagerGenerator inputManagerGenerator = new InputManagerGenerator();

//        //Debug.Log("設定を全てクリアします。");
//        //inputManagerGenerator.Clear();

//        Debug.Log("プレイヤーごとの設定を追加します。");
//        for (int i = 0; i < 4; i++)
//        {
//            AddPlayerInputSettings(inputManagerGenerator, i);
//        }

//        Debug.Log("グローバル設定を追加します。");
//        AddGlobalInputSettings(inputManagerGenerator);

//        Debug.Log("インプットマネージャーの設定が完了しました。");
//    }

//    /// <summary>
//    /// グローバルな入力設定を追加する（OK、キャンセルなど）
//    /// </summary>
//    /// <param name="inputManagerGenerator">Input manager generator.</param>
//    private static void AddGlobalInputSettings(InputManagerGenerator inputManagerGenerator)
//    {

//        // 横方向
//        {
//            var name = "Horizontal";
//            inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, 0, 1));
//            inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, "a", "d", "left", "right"));
//        }

//        // 縦方向
//        {
//            var name = "Vertical";
//            inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, 0, 2));
//            inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, "s", "w", "down", "up"));
//        }

//        // 決定
//        {
//            var name = "OK";
//            inputManagerGenerator.AddAxis(InputAxis.CreateButton(name, "z", "joystick button 0"));
//        }

//        // キャンセル
//        {
//            var name = "Cancel";
//            inputManagerGenerator.AddAxis(InputAxis.CreateButton(name, "x", "joystick button 1"));
//        }

//        // ポーズ
//        {
//            var name = "Pause";
//            inputManagerGenerator.AddAxis(InputAxis.CreateButton(name, "escape", "joystick button 7"));
//        }
//    }

//    /// <summary>
//    /// プレイヤーごとの入力設定を追加する
//    /// </summary>
//    /// <param name="inputManagerGenerator">Input manager generator.</param>
//    /// <param name="playerIndex">Player index.</param>
//    private static void AddPlayerInputSettings(InputManagerGenerator inputManagerGenerator, int playerIndex)
//    {
//        if (playerIndex < 0 || playerIndex > 3) Debug.LogError("プレイヤーインデックスの値が不正です。");
//        string upKey = "", downKey = "", leftKey = "", rightKey = "", attackKey = "";
//        GetAxisKey(out upKey, out downKey, out leftKey, out rightKey, out attackKey, playerIndex);

//        int joystickNum = playerIndex + 1;

//        // 横方向
//        {
//            var name = string.Format("Player{0} Horizontal", playerIndex);
//            inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, joystickNum, 1));
//            inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, leftKey, rightKey, "", ""));
//        }

//        // 縦方向
//        {
//            var name = string.Format("Player{0} Vertical", playerIndex);
//            inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, joystickNum, 2));
//            inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, downKey, upKey, "", ""));
//        }


//        // 攻撃
//        {
//            var axis = new InputAxis();
//            var name = string.Format("Player{0} Attack", playerIndex);
//            var button = string.Format("joystick {0} button 0", joystickNum);
//            inputManagerGenerator.AddAxis(InputAxis.CreateButton(name, button, attackKey));
//        }
//    }

//    /// <summary>
//    /// キーボードでプレイした場合、割り当たっているキーを取得する
//    /// </summary>
//    /// <param name="upKey">Up key.</param>
//    /// <param name="downKey">Down key.</param>
//    /// <param name="leftKey">Left key.</param>
//    /// <param name="rightKey">Right key.</param>
//    /// <param name="attackKey">Attack key.</param>
//    /// <param name="playerIndex">Player index.</param>
//    private static void GetAxisKey(out string upKey, out string downKey, out string leftKey, out string rightKey, out string attackKey, int playerIndex)
//    {
//        upKey = "";
//        downKey = "";
//        leftKey = "";
//        rightKey = "";
//        attackKey = "";

//        switch (playerIndex)
//        {
//            case 0:
//                upKey = "w";
//                downKey = "s";
//                leftKey = "a";
//                rightKey = "d";
//                attackKey = "e";
//                break;
//            case 1:
//                upKey = "i";
//                downKey = "k";
//                leftKey = "j";
//                rightKey = "l";
//                attackKey = "o";
//                break;
//            case 2:
//                upKey = "up";
//                downKey = "down";
//                leftKey = "left";
//                rightKey = "right";
//                attackKey = "[0]";
//                break;
//            case 3:
//                upKey = "[8]";
//                downKey = "[5]";
//                leftKey = "[4]";
//                rightKey = "[6]";
//                attackKey = "[9]";
//                break;
//            default:
//                Debug.LogError("プレイヤーインデックスの値が不正です。");
//                upKey = "";
//                downKey = "";
//                leftKey = "";
//                rightKey = "";
//                attackKey = "";
//                break;
//        }
//    }
//}