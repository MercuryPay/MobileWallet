using System.Runtime.InteropServices;

namespace MpsWidgetHostingControl
{
    [ComVisible(false)]
    public delegate void DataReadyEventHandler(string data);

    [Guid("AA0BD0C7-F453-40D1-8CAE-FEEB0B0BF63D")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IMpsWidgetControlEvents
    {
        [DispId(1)]
        void DataReady(string data);

    }

    [Guid("C2E8BBCA-5ECC-489A-9766-4A65D85609AE")]
    public interface IMpsWidgetControl
    {
        /// <summary>
        /// /////////////////
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        [DispId(1)]
        void AddAttribute(string key, string value);

        [DispId(2)]
        void RemoveAttribute(string key);

        [DispId(3)]
        string GetAttribute(string key);

        [DispId(4)]
        string Navigate();
    }
}
