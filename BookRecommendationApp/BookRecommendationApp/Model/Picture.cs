using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRecommendationApp.Model
{
    // WARNING: DO NOT SERIALIZE & PUSH TO DATABASE
    public class Picture : IDisposable
    {
        private Image image = null;

        public Picture(string filepath) { FilePath = filepath; }

        public string FilePath { get; set; }
        public string Content { get; set; }
        
        public void SetNewName()
        {
            if (FilePath == null)
                return;

            string newName = Guid.NewGuid().ToString();
            string type = FilePath.Split('.').Last();
            FilePath = newName + '.' + type;
        }

        public Image GetImage()
        {
            if (image != null)
                return image;

            // Invalid Picture object
            if (FilePath == null)
                return null;

            // Make picture from hash
            if (Content != null)
            {
                using (FileStream cout = File.OpenWrite(FilePath))
                {
                    byte[] data = Decrypt(Content);
                    cout.Write(data, 0, data.Length);
                }
                return image = Image.FromFile(FilePath);
            }
            else // Get picture from file
            {
                try
                {
                    return image = Image.FromFile(FilePath);
                }
                catch (Exception e) {
                    if (e is OutOfMemoryException)
                        throw e;
                    Database.PostError(e);
                    // Outside, try to get hash from database
                    return null;
                }
            }
        }

        // Save to Computer
        public void SaveImage()
        {
            // if Content (provided outside) is not available
            if (Content == null)
                return;

            // Invalid Picture object
            if (FilePath == null)
                return;
            
            using (FileStream cout = File.OpenWrite(FilePath))
            {
                byte[] data = Decrypt(Content);
                cout.Write(data, 0, data.Length);
            }
        }

        // Load from Computer
        public void LoadContent()
        {
            // Invalid Picture object
            if (FilePath == null)
                return;

            using (FileStream cin = File.OpenRead(FilePath))
            {
                byte[] data = new byte[cin.Length];
                cin.Read(data, 0, data.Length);

                Content = Encrypt(data);
            }
        }

        #region Encrypt & Decrypt
        private const int offset = 0x40;
        private const int shift = 4;
        private static Encoding encoding = Encoding.UTF8;

        public string Encrypt(byte[] data)
        {
            int len = data.Length;
            ushort[] data2 = new ushort[len];

            for (int i = 0; i < len; i++)
                data2[i] = (ushort)(data[i] << shift);

            byte[] data3 = new byte[len * 2];
            Buffer.BlockCopy(data2, 0, data3, 0, len * 2);

            for (int i = 0; i < len; i++)
            {
                data3[i * 2 + 1] = (byte)(data3[i * 2 + 1] + offset);
                data3[i * 2] = (byte)((data3[i * 2] >> shift) + offset);
            }

            string test = encoding.GetString(data3, 0, len * 2);
            return test;
        }

        public byte[] Decrypt(string str)
        {
            byte[] data = encoding.GetBytes(str);
            int len = data.Length / 2;
            byte[] data2 = new byte[len];

            if (len * 2 != data.Length)
                throw new NotImplementedException();

            for (int i = 0; i < len; i++)
                data2[i] = (byte)(((data[2 * i + 1] - offset) << (8 - shift)) + (data[i * 2] - offset));

            return data2;
        }
        #endregion

        public void Dispose()
        {
            image?.Dispose();
        }
    }
}
