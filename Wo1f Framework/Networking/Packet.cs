using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Text.RegularExpressions;

using System.Security.Cryptography;
//Written entirely by Wo1f
namespace Wo1f_Framework.Networking
{
    public class Packet
    {
        private static Encoding encoder = Encoding.GetEncoding(1252);

        public string Command
        {
            get
            {
                return encoder.GetString(Bytes[0]);
            }
            set
            {
                if (Bytes.Count > 0)
                    Bytes[0] = encoder.GetBytes(value);
                else
                    Bytes.Add(encoder.GetBytes(value));
            }
        }
        public List<byte[]> ArgumentsB
        {
            get
            {
                List<byte[]> Ret = new List<byte[]>();
                Ret.AddRange(Bytes.GetRange(1, Bytes.Count));
                return Ret;
            }
        }
        //public List<string> Arguments;
        public bool ExtraPacket = false;
        public List<Packet> Extras = new List<Packet>();
        public bool Encrypted { get { return Command.StartsWith(Packet.BtoS(EPrefix)); } }

        byte[] ZeroReplacers = new byte[] { 255, 1, 13, 255 }; //allow to be changed in code
        List<byte[]> Bytes = new List<byte[]>(); //use this to store the information for later use
        //Bytes[0] = command, the rest are args

        //private bool _encrypted = false;
        private string EPrefixS = "=§";
        private byte[] EPrefix = StoB("=§");
        private string EndS = "§E§";
        private byte[] End = StoB("§E§");
        private string SplitterS = "§S§";
        private byte[] Splitter = StoB("§S§");
        private string PrefixS = "§±";
        private byte[] Prefix = StoB("§±");

        private static byte[] StoB(string S)
        {
            return encoder.GetBytes(S);
        }
        private static string BtoS(byte[] B)
        {
            return encoder.GetString(B);
        }
        public Packet(string Cmd, string[] Args)
        {
            Bytes.Clear();
            Bytes.Add(Packet.StoB(Cmd));
            foreach (string S in Args)
                Bytes.Add(Packet.StoB(S));
        }
        public Packet(string Cmd, string Arg)
        {
            Bytes.Clear();
            Bytes.Add(Packet.StoB(Cmd));
            Bytes.Add(Packet.StoB(Arg));
        }
        public Packet(string Cmd)
        {
            Command = Cmd;
        }

        public Packet(byte[] Vals)
        {
            try
            {
                List<byte> Vals1 = new List<byte>();
                Vals1.AddRange(Vals);
                //int index = Vals1.IndexOf(0);
                /*for (int x = Vals1.Count-1; x > 0; x--)
                {
                    if (Vals1[x] == 0)
                        Vals1.RemoveAt(x);
                    else
                        break;
                }*/
                //if (index != -1)
                //    Vals1.RemoveRange(index, Vals1.Count - index);
                //byte[] all = this.ToBytes();
                string all = encoder.GetString(Vals1.ToArray());

                if (!all.StartsWith(PrefixS) || !all.EndsWith(EndS)) //invalid packet
                {


                }

                string[] Dirty = Regex.Split(all, EndS);
                for (int x = 0; x < Dirty.Length; x++)
                {
                    string Line = Dirty[x];
                    if (Line == "")
                        continue;
                    if (x == 0)
                    {
                        string[] args = Regex.Split(Line, SplitterS);


                        Bytes.Clear();

                        foreach (string Arg in args)
                        {
                            //if (Arg == "")
                            //    continue;
                            Bytes.Add(encoder.GetBytes(Arg.Replace(PrefixS, "")));
                        }
                        if (Encrypted)
                        {
                            Decrypt();
                        }
                        else
                        {
                            throw new Exception("Received a non-encrypted packet.");
                        }
                        if (Bytes[Bytes.Count - 1].Length == 0)
                            Bytes.RemoveAt(Bytes.Count - 1);
                    }
                    else
                    {
                        Packet Extra = new Packet(encoder.GetBytes(Line));

                        string[] args = Regex.Split(Line, SplitterS);

                        //Bytes.Clear();
                        foreach (string Arg in args)
                        {
                            if (Arg == "")
                                continue;
                            Bytes.Add(encoder.GetBytes(Arg.Replace(PrefixS, "")));
                        }
                    }
                }
            }
            catch(Exception E)
            {
                
            }

        }
        public List<string> Arguments
        {
            get
            {
                List<string> Ret = new List<string>();
                byte[][] a = Bytes.ToArray();
                for (int x = 1; x < a.Length; x++) //ToArray so it won't change in runtime
                    if (a[x] != null)
                        Ret.Add(encoder.GetString(a[x]));
                    else
                        Ret.Add("");
                return Ret;
            }

        }

        public override string ToString()
        {
            string val = Packet.BtoS(Prefix);
            val += Command;

            string[] Ss = Arguments.ToArray();
            for (int x = 0; x < Ss.Length; x++)
            {
                val += SplitterS + Ss[x];
            }

            return val;
        }

        public byte[] ToBytes()
        {
            List<byte> Ret = new List<byte>();
            //Prefix,cmd,separator,data

            if (!Encrypted)
                Ret.AddRange(Prefix);

            foreach (byte[] Vals in Bytes.ToArray())
            {
                Ret.AddRange(Vals);
                Ret.AddRange(Splitter);
            }
            if (!Encrypted)
                Ret.AddRange(End);
            string test = encoder.GetString(Ret.ToArray());
            return Ret.ToArray();
        }

        internal void Encrypt()
        {
            if (Encrypted)
                throw new Exception("Packet already encrypted");

            string p = EPrefixS;
            byte[] cmd = merge(EPrefix, testE(Bytes[0]));
            Bytes[0] = cmd;
            for (int x = 1; x < this.Bytes.Count; x++)
            {
                //this.Arguments[x] = testE(this.Arguments[x]);
                this.Bytes[x] = testE(this.Bytes[x]);
            }
        }

        private byte[] merge(byte[] EPrefix, byte[] p)
        {
            List<byte> Ret = new List<byte>();
            Ret.AddRange(EPrefix);
            Ret.AddRange(p);
            return Ret.ToArray();
        }

        internal void Decrypt()
        {
            try
            {
                if (!Encrypted)
                    throw new Exception("Packet already decrypted");
                byte[] cmd = new byte[Bytes[0].Length - EPrefix.Length];
                Array.Copy(Bytes[0], EPrefix.Length, cmd, 0, cmd.Length);
                byte[] d = testD(cmd);
                if (d == null)
                { }
                Bytes[0] = d;

                for (int x = 1; x < Arguments.Count; x++)
                    if (Bytes[x] != null)
                    {
                        Bytes[x] = testD(Bytes[x]);
                    }
                    else
                    {

                    }

            }
            catch(Exception E)
            {
                
            }
        }

        private bool IsEncrypted(byte[] vals)
        {
            try
            {
                testD(vals);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private string testE(string vals)
        {
            return Convert.ToBase64String(testE(UTF8Encoding.UTF8.GetBytes(vals)));
            //return UTF8Encoding.UTF8.GetString(testE(UTF8Encoding.UTF8.GetBytes(vals)));
        }
        private byte[] testE(byte[] vals)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
            string key = "beached whales go rawr!!";
            string iv = "asdfasdf"; //TODO Increase security

            int S = DES.BlockSize / 8;
            KeySizes[] KS = DES.LegalKeySizes;
            int min = KS[0].MinSize / 8;
            int max = KS[0].MaxSize / 8;
            if (key.Length > max)
                key = key.Remove(max);



            byte[] K = UTF8Encoding.UTF8.GetBytes(key);
            byte[] I = UTF8Encoding.UTF8.GetBytes(iv);
            DES.Key = K;
            DES.IV = I;

            DES.Mode = CipherMode.ECB;
            DES.Padding = PaddingMode.PKCS7;

            byte[] ret = null;
            try
            {
                ICryptoTransform cTransform = DES.CreateEncryptor();
                ret = cTransform.TransformFinalBlock(vals, 0, vals.Length);
                DES.Clear();
            }
            catch (Exception E)
            {

            }
            return ret;
        }
        private string testD(string vals)
        {
            return Convert.ToBase64String(testD(Convert.FromBase64String(vals)));
            //return UTF8Encoding.UTF8.GetString(testD(UTF8Encoding.UTF8.GetBytes(vals)));
        }
        private byte[] testD(byte[] vals)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
            string key = "beached whales go rawr!!";
            string iv = "asdfasdf"; //TODO Increase security

            int S = DES.BlockSize / 8;
            KeySizes[] KS = DES.LegalKeySizes;
            int min = KS[0].MinSize / 8;
            int max = KS[0].MaxSize / 8;
            if (key.Length > max)
                key = key.Remove(max);



            byte[] K = UTF8Encoding.UTF8.GetBytes(key);
            byte[] I = UTF8Encoding.UTF8.GetBytes(iv);
            DES.Key = K;
            DES.IV = I;

            DES.Mode = CipherMode.ECB;
            DES.Padding = PaddingMode.PKCS7;

            byte[] ret = null;
            try
            {
                ICryptoTransform cTransform = DES.CreateDecryptor();
                ret = cTransform.TransformFinalBlock(vals, 0, vals.Length);
                DES.Clear();
            }
            catch (Exception E)
            {

            }
            return ret;
        }
    }
}
