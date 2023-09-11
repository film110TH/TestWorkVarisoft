using System.Threading.Tasks;
using UnityEngine;

public class Tool
{
    #region Singleton
    private static Tool _instance = null;
    private static readonly object _padlock = new object();

    public static Tool Instance
    {
        get
        {
            lock (_padlock)
            {
                if (_instance == null)
                {
                    _instance = new Tool();
                }
                return _instance;
            }
        }
    }
    #endregion

    public static async void SetActive(GameObject obj, bool state, float delay)
    {
        await TaskSetActive(obj, state, delay);
    }
    private static async Task TaskSetActive(GameObject obj, bool state, float delay)
    {
        await Task.Delay((int)(delay * 1000f));
        try
        {
            obj.SetActive(state);
        }
        catch { }
    }
    public static async Task<bool> Delaybool(bool state, float delay )
    {
        await Task.Delay((int)(delay * 1000f));
        return state = !state;
    }

    public static async void Delayfuntion(System.Action action, float delay)
    {
        await TaskDelayfuntion(action, delay);
    }

    static async Task TaskDelayfuntion(System.Action action,float delay)
    {
        await Task.Delay((int)(delay * 1000f));
        action();
    }
}