/// <author>
/// 新井大一
/// </author>


using UnityEngine;
using UnityEditor;

/// <summary>
/// InputManagerで設定するデータ群
/// </summary>
public class InputAxis
{
    // 軸のタイプ
    public enum AxisType
    {
        //　キーかマウス
        KeyOrMouseButton = 0,
        //　マウスの移動差分
        MouseMovement = 1,
        //　ゲームパッド
        JoystickAxis = 2
    };

    public InputAxis()
    {
        name = "";
        descriptiveName = "";
        descriptiveNegativeName = "";
        negativeButton = "";
        positiveButton = "";
        altNegativeButton = "";
        altPositiveButton = "";

        gravity = 0.0f;
        dead = 0.0f;
        sensitivity = 0.0f;

        snap = false;
        invert = false;

        type = AxisType.KeyOrMouseButton;
    }


    // InputManagerのデータ
    public string name { get; private set; }
    public string descriptiveName { get; private set; }
    public string descriptiveNegativeName { get; private set; }
    public string negativeButton { get; private set; }
    public string positiveButton { get; private set; }
    public string altNegativeButton { get; private set; }
    public string altPositiveButton { get; private set; }

    public float gravity { get; private set; }
    public float dead { get; private set; }
    public float sensitivity { get; private set; }

    public bool snap { get; private set; }
    public bool invert { get; private set; }

    public AxisType type { get; private set; }

    // 1から始まる。
    public int _axis = 1;
    // 0なら全てのゲームパッドから取得される。1以降なら対応したゲームパッド。
    public int _joyNum = 0;

    /// <summary>
    /// 押すと1になるキーの設定データを作成する
    /// </summary>
    /// <returns>The button.</returns>
    /// <param name="name">Name.</param>
    /// <param name="positive_button">Positive button.</param>
    /// <param name="alt_positive_button">Alternate positive button.</param>
    public static InputAxis CreateButton(ref string name, ref string positive_button, string alt_positive_button)
    {
        var axis = new InputAxis();
        axis.name = name;
        axis.positiveButton = positive_button;
        axis.altPositiveButton = alt_positive_button;
        axis.gravity = 1000;
        axis.dead = 0.001f;
        axis.sensitivity = 1000;
        axis.type = AxisType.KeyOrMouseButton;

        return axis;
    }

    public static InputAxis CreatePadButton(ref string name, ref string positive_button, int joystick_num, string alt_positive_button)
    {
        var axis = new InputAxis();
        axis.name = name;
        axis.positiveButton = positive_button;
        axis.altPositiveButton = alt_positive_button;
        axis.dead = 0.2f;
        axis.sensitivity = 1;
        axis.type = AxisType.KeyOrMouseButton;

        return axis;
    }

    /// <summary>
    /// ゲームパッド用の軸の設定データを作成する
    /// </summary>
    /// <returns>The joy axis.</returns>
    /// <param name="name">Name.</param>
    /// <param name="joystick_num">Joystick number.</param>
    /// <param name="axis_num">Axis number.</param>
    public static InputAxis CreatePadAxis(ref string name, int joystick_num, int axis_num)
    {
        var axis = new InputAxis();
        axis.name = name;
        axis.dead = 0.2f;
        axis.sensitivity = 1;
        axis.type = AxisType.JoystickAxis;
        axis._axis = axis_num;
        axis._joyNum = joystick_num;

        return axis;
    }

    /// <summary>
    /// キーボード用の軸の設定データを作成する
    /// </summary>
    /// <returns>The key axis.</returns>
    /// <param name="name">Name.</param>
    /// <param name="negative_button">Negative button.</param>
    /// <param name="positive_button">Positive button.</param>
    /// <param name="axisNum">Axis number.</param>
    public static InputAxis CreateKeyAxis(ref string name, ref string negative_button, ref string positive_button, string alt_negative_button, string alt_positive_button)
    {
        var axis = new InputAxis();
        axis.name = name;
        axis.negativeButton = negative_button;
        axis.positiveButton = positive_button;
        axis.altNegativeButton = alt_negative_button;
        axis.altPositiveButton = alt_positive_button;
        axis.gravity = 3;
        axis.sensitivity = 3;
        axis.dead = 0.001f;
        axis.type = AxisType.KeyOrMouseButton;

        return axis;
    }
}

/// <summary>
/// InputManagerを作成する機能
/// </summary>
public class InputManagerGenerater
{
    public InputManagerGenerater()
    {
        Set();
    }

    /// <summary>
    /// InputManagerに設定する
    /// </summary>
    [MenuItem("Utility/Add InputManager")]
    public static void Set()
    {
        // InputManager.assetをシリアライズされたオブジェクトとして読み込む
        // 参考URL：http://docs.unity3d.com/ja/current/ScriptReference/AssetDatabase.html
        _serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);

        for (int i = 0; i < 5; i++)
        {
            AddPlayerInputSettings(i);
        }
    }

    /// <summary>
    /// 軸を追加します。
    /// </summary>
    /// <param name="serializedObject">Serialized object.</param>
    /// <param name="axis">Axis.</param>
    public static void AddAxis(InputAxis axis)
    {
        if (axis._axis < 1) Debug.LogError("Axisは1以上に設定してください。");
        SerializedProperty axesProperty = _serializedObject.FindProperty("m_Axes");

        axesProperty.arraySize++;
        _serializedObject.ApplyModifiedProperties();

        SerializedProperty axisProperty = axesProperty.GetArrayElementAtIndex(axesProperty.arraySize - 1);

        GetChildProperty(axisProperty, "m_Name").stringValue = axis.name;
        GetChildProperty(axisProperty, "descriptiveName").stringValue = axis.descriptiveName;
        GetChildProperty(axisProperty, "descriptiveNegativeName").stringValue = axis.descriptiveNegativeName;
        GetChildProperty(axisProperty, "negativeButton").stringValue = axis.negativeButton;
        GetChildProperty(axisProperty, "positiveButton").stringValue = axis.positiveButton;
        GetChildProperty(axisProperty, "altNegativeButton").stringValue = axis.altNegativeButton;
        GetChildProperty(axisProperty, "altPositiveButton").stringValue = axis.altPositiveButton;
        GetChildProperty(axisProperty, "gravity").floatValue = axis.gravity;
        GetChildProperty(axisProperty, "dead").floatValue = axis.dead;
        GetChildProperty(axisProperty, "sensitivity").floatValue = axis.sensitivity;
        GetChildProperty(axisProperty, "snap").boolValue = axis.snap;
        GetChildProperty(axisProperty, "invert").boolValue = axis.invert;
        GetChildProperty(axisProperty, "type").intValue = (int)axis.type;
        GetChildProperty(axisProperty, "axis").intValue = axis._axis - 1;
        GetChildProperty(axisProperty, "joyNum").intValue = axis._joyNum;

        _serializedObject.ApplyModifiedProperties();

    }

    //---------------------------------------------------------------------------------------------------------------------

    //参考URL：http://whaison.jugem.jp/?eid=761
    static SerializedObject _serializedObject;

    /// <summary>
    /// 子要素のプロパティを取得します。
    /// </summary>
    /// <returns>The child property.</returns>
    /// <param name="parent">Parent.</param>
    /// <param name="name">Name.</param>
    static SerializedProperty GetChildProperty(SerializedProperty parent, string name)
    {
        SerializedProperty child = parent.Copy();
        child.Next(true);

        do
        {
            if (child.name == name) return child;
        }
        while (child.Next(false));
        return null;
    }

    /// <summary>
    /// プレイヤーごとの入力設定を追加する
    /// </summary>
    /// <param name="inputManagerGenerator">Input manager generator.</param>
    /// <param name="player_index">Player index.</param>
    static void AddPlayerInputSettings(int player_index)
    {
        //if (player_index < 0 || player_index > 3) Debug.LogError("プレイヤーインデックスの値が不正です。");
        string upKey = "", downKey = "", leftKey = "", rightKey = "";
        GetAxisKey(out upKey, out downKey, out leftKey, out rightKey, player_index);

        int joystickNum = player_index;

        // 左スティック横方向
        {
            var name = string.Format("LeftHorizontal{0}", player_index);
            AddAxis(InputAxis.CreatePadAxis(ref name, joystickNum, 1));
            AddAxis(InputAxis.CreateKeyAxis(ref name, ref leftKey, ref rightKey, "", ""));
        }

        // 左スティック縦方向
        {
            var name = string.Format("LeftVertical{0}", player_index);
            AddAxis(InputAxis.CreatePadAxis(ref name, joystickNum, 2));
            AddAxis(InputAxis.CreateKeyAxis(ref name, ref downKey, ref upKey, "", ""));
        }

        // 右スティック横方向
        {
            var name = string.Format("RightHorizontal{0}", player_index);
            var leftName = "f";
            var rightName = "h";
            AddAxis(InputAxis.CreatePadAxis(ref name, joystickNum, 4));
            AddAxis(InputAxis.CreateKeyAxis(ref name, ref leftName, ref rightName, "", ""));
        }

        // 右スティック縦方向
        {
            var name = string.Format("RightVertical{0}", player_index);
            var leftName = "g";
            var rightName = "t";
            AddAxis(InputAxis.CreatePadAxis(ref name, joystickNum, 5));
            AddAxis(InputAxis.CreateKeyAxis(ref name, ref leftName, ref rightName, "", ""));
        }

        //R2
        {
            var name = string.Format("R2{0}", player_index);
            AddAxis(InputAxis.CreatePadAxis(ref name, joystickNum, 3));
            var leftName = "q";
            var rightName = "e";
            AddAxis(InputAxis.CreateKeyAxis(ref name, ref leftName, ref rightName, "", ""));
        }

        ////R3
        //{
        //    var name = string.Format("R3{0}", player_index);
        //    var button = string.Format("joystick button 9", joystickNum);
        //    AddAxis(InputAxis.CreatePadButton(ref name, ref button, joystickNum, ""));
        //}

        ////A
        //{
        //    var name = string.Format("A{0}", player_index);
        //    var button = string.Format("joystick {0} button 0", joystickNum);
        //    AddAxis(InputAxis.CreatePadButton(ref name, ref button, joystickNum, ""));
        //}

        ////B
        //{
        //    var name = string.Format("B{0}", player_index);
        //    var button = string.Format("joystick button 1", joystickNum);
        //    AddAxis(InputAxis.CreatePadButton(ref name, ref button, joystickNum, ""));
        //}

        ////Start
        //{
        //    var name = string.Format("Start{0}", player_index);
        //    var button = string.Format("joystick button 7", joystickNum);
        //    AddAxis(InputAxis.CreatePadButton(ref name, ref button, joystickNum, ""));
        //}
    }

    /// <summary>
    /// キーボードでプレイした場合、割り当たっているキーを取得する
    /// </summary>
    /// <param name="up_key">Up key.</param>
    /// <param name="down_key">Down key.</param>
    /// <param name="left_key">Left key.</param>
    /// <param name="right_key">Right key.</param>
    /// <param name="attackKey">Attack key.</param>
    /// <param name="playerIndex">Player index.</param>
    static void GetAxisKey(out string up_key, out string down_key, out string left_key, out string right_key, int playerIndex)
    {
        up_key = "";
        down_key = "";
        left_key = "";
        right_key = "";

        switch (playerIndex)
        {
            case 1:
                up_key = "s";
                down_key = "w";
                left_key = "a";
                right_key = "d";
                break;
            case 2:
                up_key = "k";
                down_key = "i";
                left_key = "j";
                right_key = "l";
                break;
            case 3:
                up_key = "down";
                down_key = "up";
                left_key = "left";
                right_key = "right";
                break;
            case 4:
                up_key = "[5]";
                down_key = "[8]";
                left_key = "[4]";
                right_key = "[6]";
                break;
            default:
                Debug.LogError("プレイヤーインデックスの値が不正です。");
                up_key = "";
                down_key = "";
                left_key = "";
                right_key = "";
                break;
        }
    }
}