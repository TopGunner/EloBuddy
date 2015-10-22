using System;
using EloBuddy;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace UltimateZhonyas
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Wait till the loading screen has passed
            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            // Initialize the classes that we need
            Config.Initialize();
            SaveMePls.Initialize();

            // Listen to events we need
            
        }
    }
}
