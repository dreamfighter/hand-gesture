using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Motion_Detection_v2
{
    public class Setting
    {
        public int[] filterVal = new int[4];
        public String svmParam = "";
        public String hmmParam = "";
        public String cameraParam = "";
        public int interval = 30;

        public void saveSetting(int hmin, int hmax, int smin, int smax, String svm, String hmm, String cam,int val)
        {
            StreamWriter sw = new StreamWriter("setting/setting.stt");
            sw.WriteLine(hmin + "," + hmax + "," + smin + "," + smax);
            sw.WriteLine(svm);
            sw.WriteLine(hmm);
            sw.WriteLine(cam);
            sw.WriteLine(val);
            sw.Close();
        }

        public void saveSetting(String filename,int hmin, int hmax, int smin, int smax, String svm, String hmm, String cam, int val)
        {
            StreamWriter sw = new StreamWriter(filename);
            sw.WriteLine(hmin + "," + hmax + "," + smin + "," + smax);
            sw.WriteLine(svm);
            sw.WriteLine(hmm);
            sw.WriteLine(cam);
            sw.WriteLine(val);
            sw.Close();
        }

        public void load()
        {
            StreamReader sr = new StreamReader("setting/new_setting.stt");
            String input = sr.ReadLine();
            String[] result = input.Split(',');
            String ycrcb_input = sr.ReadLine();

            try { svmParam = sr.ReadLine(); }
            catch (Exception ex) { Console.WriteLine(ex.Message); svmParam = ""; }

            try { hmmParam = sr.ReadLine(); }
            catch (Exception ex) { Console.WriteLine(ex.Message); hmmParam = ""; }

            try { cameraParam = sr.ReadLine(); }
            catch (Exception ex) { Console.WriteLine(ex.Message); cameraParam = ""; }

            try { interval = int.Parse(sr.ReadLine()); }
            catch (Exception ex) { Console.WriteLine(ex.Message); interval = 30; }

            try
            {
                filterVal[0] = int.Parse(result[0]);
                filterVal[1] = int.Parse(result[1]);
                filterVal[2] = int.Parse(result[2]);
                filterVal[3] = int.Parse(result[3]);
            }
            catch (Exception ex)
            {
                filterVal[0] = 29;
                filterVal[1] = 90;
                filterVal[2] = 59;
                filterVal[3] = 255;
                Console.WriteLine(ex.Message);
            }
            sr.Close();
        }

        public void loadSetting()
        {
            StreamReader sr = new StreamReader("setting/setting.stt");
            String input = sr.ReadLine();
            String[] result = input.Split(',');

            try { svmParam = sr.ReadLine(); }
            catch (Exception ex) { Console.WriteLine(ex.Message); svmParam = ""; }

            try { hmmParam = sr.ReadLine(); }
            catch (Exception ex) { Console.WriteLine(ex.Message); hmmParam = ""; }

            try { cameraParam= sr.ReadLine(); }
            catch (Exception ex) { Console.WriteLine(ex.Message); cameraParam = ""; }

            try { interval = int.Parse(sr.ReadLine()); }
            catch (Exception ex) { Console.WriteLine(ex.Message); interval = 30; }

            try
            {
                filterVal[0] = int.Parse(result[0]);
                filterVal[1] = int.Parse(result[1]);
                filterVal[2] = int.Parse(result[2]);
                filterVal[3] = int.Parse(result[3]);
            }
            catch (Exception ex)
            {
                filterVal[0] = 29;
                filterVal[1] = 90;
                filterVal[2] = 59;
                filterVal[3] = 255;
                Console.WriteLine(ex.Message);
            }
            sr.Close();
        }

        public void loadSetting(String filename)
        {
            StreamReader sr = new StreamReader(filename);
            String input = sr.ReadLine();
            String[] result = input.Split(',');

            try { svmParam = sr.ReadLine(); }
            catch (Exception ex) { Console.WriteLine(ex.Message); svmParam = ""; }

            try { hmmParam = sr.ReadLine(); }
            catch (Exception ex) { Console.WriteLine(ex.Message); hmmParam = ""; }

            try { cameraParam = sr.ReadLine(); }
            catch (Exception ex) { Console.WriteLine(ex.Message); cameraParam = ""; }

            try { interval = int.Parse(sr.ReadLine()); }
            catch (Exception ex) { Console.WriteLine(ex.Message); interval = 30; }

            try
            {
                filterVal[0] = int.Parse(result[0]);
                filterVal[1] = int.Parse(result[1]);
                filterVal[2] = int.Parse(result[2]);
                filterVal[3] = int.Parse(result[3]);
            }
            catch (Exception ex)
            {
                filterVal[0] = 29;
                filterVal[1] = 90;
                filterVal[2] = 59;
                filterVal[3] = 255;
                Console.WriteLine(ex.Message);
            }
            sr.Close();
        }

        public int[] getFilterSetting()
        {
            return filterVal;
        }

        public String getSvmParam()
        {
            return svmParam;
        }

        public String getHmmParam()
        {
            return hmmParam;
        }

        public String getCamParam()
        {
            return cameraParam;
        }

        public int getInterval()
        {
            return interval;
        }
    }
}
