using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

//stuff that actually saves/loads files
using Microsoft.Xna.Framework.Storage;
using System.Xml.Serialization;
using System.IO;

namespace AdlezHolder
{
    public class SaveFile
    {
        public GameData Data
        {
            get { return myData; }
        }
        GameData myData;

        //has file property

        StorageDevice device;
        StorageContainer container;
        IAsyncResult result;

        string fileName;
        const string SAVE_LOCATION = "Saves";

        public SaveFile(int fileIndex)
        {
            fileName = "File" + fileIndex + ".hbs";
            initialize();            
        }

        public SaveFile(string fileName)
        {
            this.fileName = fileName;
            initialize();
        }

        private void initialize()
        {
            myData = new GameData();
            //if (hasFile())
            //    load();
        }

        public void save(GameData saveData)
        {
            result = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);

            device = StorageDevice.EndShowSelector(result);

            result = device.BeginOpenContainer(SAVE_LOCATION, null, null);

            result.AsyncWaitHandle.WaitOne();

            container = device.EndOpenContainer(result);

            result.AsyncWaitHandle.Close();
                        
            using (Stream stream = container.CreateFile(fileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GameData));
                serializer.Serialize(stream, saveData);
            }

            container.Dispose();
            myData = saveData.clone();
        }

        public GameData load()
        {
            result = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);

            device = StorageDevice.EndShowSelector(result);

            result = device.BeginOpenContainer(SAVE_LOCATION, null, null);

            result.AsyncWaitHandle.WaitOne();

            container = device.EndOpenContainer(result);

            result.AsyncWaitHandle.Close();

            if (!container.FileExists(fileName))
            {
                container.Dispose();
                throw new Exception("No File to load");
            }
            
            using (Stream stream = container.OpenFile(fileName, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GameData));
                myData = (GameData)serializer.Deserialize(stream);
            }
            container.Dispose();

            return myData;
        }

        public void delete()
        {
            result = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);

            device = StorageDevice.EndShowSelector(result);

            result = device.BeginOpenContainer(SAVE_LOCATION, null, null);

            result.AsyncWaitHandle.WaitOne();

            container = device.EndOpenContainer(result);

            result.AsyncWaitHandle.Close();

            if (container.FileExists(fileName))
            {
                container.DeleteFile(fileName);
            }
            container.Dispose();

            myData = new GameData();
        }

        public bool hasFile()
        {
            result = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);

            device = StorageDevice.EndShowSelector(result);

            result = device.BeginOpenContainer(SAVE_LOCATION, null, null);

            result.AsyncWaitHandle.WaitOne();

            container = device.EndOpenContainer(result);

            result.AsyncWaitHandle.Close();

            return container.FileExists(fileName);
        }

        private void createFile()
        {
            result = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);

            device = StorageDevice.EndShowSelector(result);

            result = device.BeginOpenContainer(SAVE_LOCATION, null, null);

            result.AsyncWaitHandle.WaitOne();

            container = device.EndOpenContainer(result);

            result.AsyncWaitHandle.Close();

            container.CreateFile(fileName);
            container.Dispose();
        }


    }
}
