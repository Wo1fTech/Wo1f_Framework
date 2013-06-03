using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Wo1f_Framework.Windows
{
    public class Wo1fWindows
    {
        public static ImageList GetIconList()
        {
            ImageList ImgList = new ImageList();

            System.Resources.ResourceManager resources = Properties.Resources.ResourceManager;

            ImgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImgList.ImageStream")));
            ImgList.TransparentColor = System.Drawing.Color.Transparent;
            ImgList.Images.Add(Properties.Resources.folder_icon_512x512);
            ImgList.Images.Add(Properties.Resources.exe);
            ImgList.Images.Add(Properties.Resources.text);
            ImgList.Images.Add(Properties.Resources.img);
            ImgList.Images.Add(Properties.Resources.WinRAR_icon);
            ImgList.Images.Add(Properties.Resources.zip);
            ImgList.Images.Add(Properties.Resources.sound);
            ImgList.Images.Add(Properties.Resources.test);
            ImgList.Images.Add(Properties.Resources.Filetype_BAT_icon);            
            ImgList.Images.Add(Properties.Resources.apps_7_zip_88651);



            return ImgList;
        }
        public static int GetIconKey(string extension)
        {
            //extensions to add
            //cs/c/h/csproj/sln/suo
            //res/resx/
            //








            extension = extension.Replace(".", "");
            extension = extension.Replace(";1", "");
            switch (extension.ToLower()) //add more extensions!
            {
                case "bin":
                case "cmd":
                case "exe":
                    return 1;
                case "bat":
                    return 8;

                case "dll":
                    return 15;

                case "cfg":
                case "config":
                case "inf":
                case "ini":
                    return 7;

                case "log":
                case "txt":
                    return 2;

                case "dds":
                case "img":
                case "tga":
                case "png":
                case "jpg":
                case "bmp":
                    return 3;

                case "zip":
                    return 5;
                case "rar":
                    return 4;

                case "7zip":
                case "7z":
                    return 9;

                case "mp3":
                case "wav":
                    return 6;

                case "avi":
                case "mp4":
                case "wmv":
                    return 16;
            }
            return 16;
        
        }
    }
}
