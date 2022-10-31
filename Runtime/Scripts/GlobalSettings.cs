namespace InventoryPackage
{
    public static class GlobalSettings
    {
        public static string LibraryKeyword {get => GetLibraryKeyWord();}
        public static string GetLibraryKeyWord()
        {
            return"ItemLibrary";
        }
    }
}