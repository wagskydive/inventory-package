using System.IO;
using UnityEngine;

namespace InventoryPackage
{
    public static class TextureSaver
    {
        public static void SaveTextureToFile(Texture2D texture, string filePath)
        {

            var bytes = texture.EncodeToPNG();
            var file = File.Open(filePath, FileMode.Create);
            var binary = new BinaryWriter(file);
            binary.Write(bytes);
            file.Close();
        }
    }
}
