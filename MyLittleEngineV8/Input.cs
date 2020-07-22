using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace MyLittleEngineV8
{   
    abstract public class Input
    {
        internal static bool Focus = true;
        
        private static List<Tuple<string, bool, bool>> keys= new List<Tuple<string, bool, bool>>();

        internal static void PrepareKeys()
        {
            keys.Add(new Tuple<string, bool, bool>("W", false, false));
            keys.Add(new Tuple<string, bool, bool>("A", false, false));
            keys.Add(new Tuple<string, bool, bool>("S", false, false));
            keys.Add(new Tuple<string, bool, bool>("D", false, false));
            keys.Add(new Tuple<string, bool, bool>("w", false, false));
            keys.Add(new Tuple<string, bool, bool>("a", false, false));
            keys.Add(new Tuple<string, bool, bool>("s", false, false));
            keys.Add(new Tuple<string, bool, bool>("d", false, false));
            keys.Add(new Tuple<string, bool, bool>("L", false, false));
            keys.Add(new Tuple<string, bool, bool>("l", false, false));
            keys.Add(new Tuple<string, bool, bool>("O", false, false));
            keys.Add(new Tuple<string, bool, bool>("o", false, false));
        }
        internal static void SetKey(KeyEventArgs e, bool isClicked)
        {
            for (int i = 0; i < keys.Count; i++)
            {
                if (keys[i].Item1 == e.KeyCode.ToString())
                {
                    string name = keys[i].Item1;
                    bool wasClicked = keys[i].Item2;

                    keys[i] = new Tuple<string, bool, bool>(name, isClicked, wasClicked);
                    break;
                }
            }
        }

        public static bool GetKey(string keyName)
        {
            if (!Focus)
                return false;
            else
            {
                foreach (Tuple<string, bool, bool> key in keys)
                {
                    if (key.Item1 == keyName)
                    {
                        return key.Item2;
                    }
                }
            }
            return false;
        }
        public static bool GetKeyUp(string keyName)
        {
            for (int i = 0; i < keys.Count; i++)
            {
                if (keys[i].Item1 == keyName)
                {
                    if (keys[i].Item2 == false && keys[i].Item3 == true)
                    {
                        keys[i] = new Tuple<string, bool, bool>(keyName, false, false);
                        return true;
                    }
                        
                    else
                        return false;
                }
            }
               
            return false;
        }
        public static bool GetKeyDown(string keyName)
        {
            for (int i = 0; i < keys.Count; i++)
            {
                if (keys[i].Item1 == keyName)
                {
                    if (keys[i].Item2 == true && keys[i].Item3 == false)
                    {
                        keys[i] = new Tuple<string, bool, bool>(keyName, true, true);
                        return true;
                    }

                    else
                        return false;
                }
            }

            return false;
        }
    }
}
