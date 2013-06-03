using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wo1f_Framework
{
    public class Vars
    {
        static Dictionary<string, object> CVars = new Dictionary<string, object>();
        static bool First = true;
        static Vars()
        {

        }

        public static void Init()
        {

        }

        //Checks if the Key is already added and adds it if it is not there.  If it is there then the key's value is set to avoid issues with re-init

        public static bool Add(string Key, object Value)
        {
            if (!CVars.ContainsKey(Key))
            {
                CVars.Add(Key, Value);
                return true;
            }

            CVars[Key] = Value;
            return false;
        }

        public static string[] HelpLookup(string find, int max)
        {
            string[] Ret = new string[0];

            return Ret;
        }

        public static string[] FindVars(string find, int max)
        {
            List<string> Ret = new List<string>();
            string[] Keys = (string[])CVars.Keys.ToArray();
            for (int x = 0; x < Keys.Length; x++)
            {
                if (Keys.Contains(find))
                {
                    foreach (string S in Keys)
                    {
                        if (S.Contains(find))
                        {
                            if (Ret.Count == max)
                                return Ret.ToArray();
                            Ret.Add(S);
                        }

                    }
                }
                else
                {
                    return new string[0];
                }
            }

            return Ret.ToArray();
        }

        static void Check()
        {
            if (First)
            {
                Init();
                First = false;
            }
        }

        static public object Get(string VarName)
        {
            Check();
            if (CVars.ContainsKey(VarName))
                return CVars[VarName];
            return null;
        }

        static public void Set(string VarName, object Value)
        {
            Check();
            if (CVars.ContainsKey(VarName))
                CVars[VarName] = Value;
            else
                CVars.Add(VarName, Value);
        }
            
        static public string GetS(string VarName)
        {
            return (string)Get(VarName);
        }

        public static int GetI(string VarName)
        {
            return (int)Get(VarName);
        }
        internal static bool GetB(string VarName)
        {
            return (bool)Get(VarName);
        }

        internal static bool Toggle(string VarName)
        {
            bool V = GetB(VarName);
            Set(VarName, !V);
            return !V;
        }
    }
}
