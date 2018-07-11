using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "InputLayout", menuName = "Input", order = 1)]
    public class InputLayout : ScriptableObject
    {
        private static int JOYSTICK_OFFSET = 20;

        public enum ActionType
        {
            ATTACK,
            SPECIAL,
            JUMP,

            SHOULDER_LEFT,
            SHOULDER_RIGHT,

            START,

            MOVE_FORWARD,
            MOVE_RIGHT
        }

        [Serializable]
        public class InputMap
        {
            public ActionType Action;

            public KeyCode Code;
            public KeyCode Code2;

            public string Axis;
            public float AxisScale = 1;

            public override string ToString()
            {
                return Action.ToString();
            }
        }

        public string TryParseName;

        public InputMap[] Map;

        private int JoyStickIndex;
        private Dictionary<ActionType, InputMap> Inputs;

        public void Init(int index)
        {
            JoyStickIndex = index;
            this.Inputs = Map.ToDictionary(x => x.Action);
        }

        public bool IsButtonDown(ActionType type)
        {
            InputMap map = Inputs[type];
            return Input.GetKeyDown(Remap(map.Code)) || Input.GetKeyDown(Remap(map.Code2));
        }

        public bool IsButton(ActionType type)
        {
            InputMap map = Inputs[type];
            return Input.GetKey(Remap(map.Code)) || Input.GetKey(Remap(map.Code2));
        }

        public float GetAxis(ActionType type)
        {
            InputMap map = Inputs[type];
            return Input.GetAxis(string.Format("J{0}_A{1}", JoyStickIndex, map.Axis)) * map.AxisScale;  //Input.GetKey(Remap(map.Code)) || Input.GetKeyDown(Remap(map.Code2));
        }


        private KeyCode Remap(KeyCode code)
        {
            return code +((JOYSTICK_OFFSET) * JoyStickIndex);
        }

    }
}
