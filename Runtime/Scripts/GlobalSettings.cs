using System;

namespace InventoryPackage
{
    public static class GlobalSettings
    {
        public static string LibraryKeyword { get => GlobalSettings.GetLibraryKeyWord(); }
        private static string GetLibraryKeyWord()
        {
            return "ItemLibrary";
        }

        public static string DefaultDescription { get => GetDefaultDescription(); }
        private static string GetDefaultDescription()
        {
            return "No description written.";
        }

        public static int DefaultStackSize { get => GetDefaultStackSize(); }

        private static int GetDefaultStackSize()
        {
            return 100;
        }
    }
}