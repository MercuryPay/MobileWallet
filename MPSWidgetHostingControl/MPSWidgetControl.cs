using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Reflection;
using System.Web.Script.Serialization;

namespace  MpsWidgetHostingControl
{
    [ProgId("MpsWidgetControl.MpsWidgetControl")]
    [ClassInterface(ClassInterfaceType.None), ComSourceInterfaces(typeof(IMpsWidgetControlEvents))]
    [ComVisible(true)]
    [Guid("CBC190DB-E536-496D-9D30-D17A92782126")]
    public partial class MpsWidgetControl : UserControl, IMpsWidgetControl
    {
        // Ref: http://msdn.microsoft.com/en-us/library/ms678497.aspx
        const int OleMiscRecomposeOnResize = 1;
        const int OleMiscCantLinkInside = 16;
        const int OleMiscInsideOut = 128;
        const int OleMiscActivateWhenVisible = 256;
        const int OleMiscSetClientSiteFirst = 131072;

        private delegate void BeforeNavigate2(object pDisp, ref dynamic url, ref dynamic flags, ref dynamic targetFrameName, ref dynamic postData, ref dynamic headers, ref bool cancel);


        public MpsWidgetControl()
        {
            InitializeComponent();
            dynamic d = wbControl.ActiveXInstance;

            d.BeforeNavigate2 += new BeforeNavigate2(HandleBeforeNavigate2);
        }

        private readonly Dictionary<string, string> _attributeCollection = new Dictionary<string, string>();

        public event DataReadyEventHandler DataReady;

        [ComVisible(true)]
        public void AddAttribute(string key, string value)
        { 
            if (_attributeCollection.ContainsKey(key))
            {
                _attributeCollection[key] = value;
            }
            else
            {
                _attributeCollection.Add(key, value);
            }
        }

        [ComVisible(true)]
        public void RemoveAttribute(string key)
        { 
            if (_attributeCollection.ContainsKey(key))
            {
                _attributeCollection.Remove(key);
            }
        }

        [ComVisible(true)]
        public string GetAttribute(string key)
        {
            string value = null;

            _attributeCollection.TryGetValue(key, out value);

            return value;
        }


        [ComVisible(true)]
        public string Navigate()
        {
            try
            {
                if ((!_attributeCollection.ContainsKey("MERCHANTID")) || (!_attributeCollection.ContainsKey("PASSWORD")) ||
                    (!_attributeCollection.ContainsKey("COMPLETEURL")) || (!_attributeCollection.ContainsKey("AMOUNT")))
                {
                    return "Validation error missing element";
                }

                string postData = String.Format("MERCHANTID={0}&PASSWORD={1}&COMPLETEURL={2}&AMOUNT={3}", _attributeCollection["MERCHANTID"],
                                                _attributeCollection["PASSWORD"], _attributeCollection["COMPLETEURL"], _attributeCollection["AMOUNT"]);
                var byteDataToPost = Encoding.UTF8.GetBytes(postData);
                var additionalHeaders = "Content-Type: application/x-www-form-urlencoded";

                wbControl.Navigate(_attributeCollection["WHURL"], "", byteDataToPost, additionalHeaders);

            }
            catch
            {
                return "exception occured process request.";
            }

            return string.Empty;
        }

        private void HandleBeforeNavigate2(object pDisp, ref dynamic url, ref dynamic flags, ref dynamic targetFrameName,
            ref dynamic postData, ref dynamic headers, ref bool cancel)
        {
            if (url == _attributeCollection["COMPLETEURL"])
            {
                if (postData != null)
                {
                    string data = System.Text.ASCIIEncoding.ASCII.GetString(postData);
                    var arr = data.Split(new char[] {'&'});
                    var dict = new Dictionary<string, string>();
                    foreach (var s in arr)
                    {
                        var arr2 = s.Split(new char[] {'='});
                        dict.Add(arr2[0], arr2[1].Replace("\u0000", ""));
                    }
                    var json = new JavaScriptSerializer().Serialize(dict);
                    OnDataReady(json);
                    cancel = true;
                }
            }
        }

        private void OnDataReady(string data)
        {
            if (DataReady != null)
            {
                DataReady(data);
            }
        }

        [ComRegisterFunction()]
        public static void RegisterClass(string key)
        {
            StringBuilder sb = new StringBuilder(key);
            sb.Replace(@"HKEY_CLASSES_ROOT\", "");
            RegistryKey k = Registry.ClassesRoot.OpenSubKey(sb.ToString(), true);
            RegistryKey ctrl = k.CreateSubKey("Control");
            ctrl.Close();
            RegistryKey inprocServer32 = k.OpenSubKey("InprocServer32", true);
            inprocServer32.SetValue("CodeBase", Assembly.GetExecutingAssembly().CodeBase);
            inprocServer32.SetValue(null, Environment.SystemDirectory + @"\mscoree.dll");
            inprocServer32.Close();

            RegistryKey miscStatus = k.CreateSubKey("MiscStatus");
            int nMiscStatus = OleMiscRecomposeOnResize +
                OleMiscCantLinkInside + OleMiscInsideOut +
                OleMiscActivateWhenVisible + OleMiscSetClientSiteFirst;
            miscStatus.SetValue("", nMiscStatus.ToString(), RegistryValueKind.String);
            miscStatus.Close();

            RegistryKey typeLib = k.CreateSubKey("TypeLib");
            Guid libId = Marshal.GetTypeLibGuidForAssembly(Assembly.GetExecutingAssembly());
            typeLib.SetValue("", libId.ToString("B"), RegistryValueKind.String);
            typeLib.Close();

            RegistryKey version = k.CreateSubKey("Version");
            int nMajor, nMinor;
            Marshal.GetTypeLibVersionForAssembly(Assembly.GetExecutingAssembly(), out nMajor, out nMinor);
            version.SetValue("", String.Format("{0}.{1}", nMajor, nMinor));
            version.Close();
        }

        [ComUnregisterFunction()]
        public static void UnregisterClass(string key)
        {
            StringBuilder sb = new StringBuilder(key);
            sb.Replace(@"HKEY_CLASSES_ROOT\", "");
            RegistryKey k = Registry.ClassesRoot.OpenSubKey(sb.ToString(), true);
            k.DeleteSubKey("Control", false);
            k.DeleteSubKey("MiscStatus", false);
            k.DeleteSubKey("TypeLib", false);
            k.DeleteSubKey("Version", false);
            k.Close();            
        }

    }
}
