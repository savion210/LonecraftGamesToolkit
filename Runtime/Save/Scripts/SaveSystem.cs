using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace LonecraftGames.Toolkit.Save
{
    /// <summary>
    /// The SaveSystem class provides methods for saving and loading data to and from files.
    /// It supports both the persistent data path (typically a directory unique to the user's device where data can be saved between game sessions)
    /// and the StreamingAssets folder (a read-only folder that can be used to store assets that are loaded by the game at runtime).
    /// The data can be optionally encrypted before saving and decrypted when loading using AES encryption.
    /// </summary>
    public class SaveSystem
    {
        /// <summary>
        /// The path to the persistent data directory.
        /// This is typically a directory that is unique to the user's device where data can be saved between game sessions.
        /// </summary>
        private string _persistentDataPath;

        /// <summary>
        /// The password used for encrypting and decrypting data.
        /// This should be kept secret and not exposed to the end user.
        /// </summary>
        private const string _encryptionKey = "8)*LrH2ljQT2%EPq";

        /// <summary>
        /// Dictionary to cache loaded resources
        /// </summary>
        private readonly Dictionary<string, UnityEngine.Object> _resourceCache =
            new Dictionary<string, UnityEngine.Object>();

        #region Save

        /// <summary>
        /// Saves data to a file in the persistent data path.
        /// </summary>
        /// <param name="data">The data to be saved.</param>
        /// <param name="folderName">The name of the folder inside the persistent data path.</param>
        /// <param name="fileName">The name of the file to which the data will be saved.</param>
        /// <param name="isEncryption">A boolean indicating whether the data should be encrypted before saving.</param>
        /// <typeparam name="T">The type of the data to be saved.</typeparam>
        public void Save<T>(T data, string folderName, string fileName, bool isEncryption)
        {
            // Set the persistent data path
            _persistentDataPath = Application.persistentDataPath;

            // Get the full path to the folder
            string path = GetPath(_persistentDataPath, folderName);
            // Get the full path to the file
            string filePath = GetFilePath(_persistentDataPath, folderName, fileName);
            Debug.Log($"Save path: {filePath}");

            if (isEncryption && string.IsNullOrEmpty(_encryptionKey))
            {
                Debug.LogError("[SaveSystem] Save failed! Encryption is enabled but no password was set. Call SetPassword() first, (e.g., in your GameManager's Awake()).");
                return;
            }

            try
            {
                // Create the directory if it doesn't exist
                CreateFolderDirectory(path);

                // Serialize the data to JSON
                string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);

                if (isEncryption)
                {
                    // If encryption is enabled, encrypt the data before saving
                    EncryptData(filePath, _encryptionKey, jsonData);
                }
                else
                {
                    // If encryption is not enabled, write the JSON data directly to the file
                    File.WriteAllText(filePath, jsonData);
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during saving
                Debug.LogError($"Error while saving file {fileName}: {ex.Message}");
            }
        }

        #endregion

        #region Load

        /// <summary>
        /// Loads data from a file in the persistent data path.
        /// </summary>
        /// <param name="folderName">The name of the folder inside the persistent data path.</param>
        /// <param name="fileName">The name of the file from which to load the data.</param>
        /// <param name="isEncrypted">A boolean indicating whether the data in the file is encrypted.</param>
        /// <returns>
        /// The data loaded from the file, deserialized into the specified type.
        /// If the file does not exist or an error occurs during loading, the default value for the type is returned.
        /// </returns>
        /// <typeparam name="T">The type into which to deserialize the loaded data.</typeparam>
        public T Load<T>(string folderName, string fileName, bool isEncrypted)
        {
            try
            {
                // Set the persistent data path
                _persistentDataPath = Application.persistentDataPath;
                // Combine the persistent data path, folder name, and file name to get the full file path
                string filePath = Path.Combine(GetFilePath(_persistentDataPath, folderName, fileName));

                if (isEncrypted && string.IsNullOrEmpty(_encryptionKey))
                {
                    Debug.LogError("[SaveSystem] Load failed! Encryption is enabled but no password was set. Call SetPassword() first, (e.g., in your GameManager's Awake()).");
                    return default(T);
                }

                if (isEncrypted)
                {
                    // If the data is encrypted, decrypt it before deserializing
                    string encryptedData = DecryptData(filePath, isEncrypted);
                    return JsonConvert.DeserializeObject<T>(encryptedData);
                }
                else
                {
                    // If the data is not encrypted, read it directly from the file and deserialize
                    string jsonData = File.ReadAllText(filePath);
                    return JsonConvert.DeserializeObject<T>(jsonData);
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during loading and return the default value for the type
                Debug.LogError($"Error while loading file {fileName}: {ex.Message}");
                return default(T);
            }
        }

        #endregion

        #region Save Streaming Assets

        /// <summary>
        /// Saves data to a file in the StreamingAssets folder.
        /// </summary>
        /// <param name="data">The data to be saved.</param>
        /// <param name="folderName">The name of the folder inside the StreamingAssets folder.</param>
        /// <param name="fileName">The name of the file to which the data will be saved.</param>
        /// <param name="isEncryption">A boolean indicating whether the data should be encrypted before saving.</param>
        /// <typeparam name="T">The type of the data to be saved.</typeparam>
        public void SaveToStreamingAssets<T>(T data, string folderName, string fileName, bool isEncryption)
        {
            // Save to streaming assets folder
            string path = GetPath(Application.streamingAssetsPath, folderName);
            string filePath = GetFilePath(Application.streamingAssetsPath, folderName, fileName);

            if (isEncryption && string.IsNullOrEmpty(_encryptionKey))
            {
                Debug.LogError("[SaveSystem] Save failed! Encryption is enabled but no password was set. Call SetPassword() first, (e.g., in your GameManager's Awake()).");
                return;
            }

            try
            {
                // Create the directory if it doesn't exist
                CreateFolderDirectory(path);

                // Serialize the data to JSON
                string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);

                if (isEncryption)
                {
                    // If encryption is enabled, encrypt the data before saving
                    EncryptData(filePath, _encryptionKey, jsonData);
                }
                else
                {
                    // If encryption is not enabled, write the JSON data directly to the file
                    File.WriteAllText(filePath, jsonData);
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during saving
                Debug.LogError($"Error while saving file {fileName}: {ex.Message}");
            }
        }

        #endregion

        #region Load Streaming Assets

        /// <summary>
        /// Loads data from a file in the StreamingAssets folder.
        /// </summary>
        /// <param name="folderName">The name of the folder inside the StreamingAssets folder.</param>
        /// <param name="fileName">The name of the file from which to load the data.</param>
        /// <param name="isEncrypted">A boolean indicating whether the data in the file is encrypted.</param>
        /// <returns>
        /// The data loaded from the file, deserialized into the specified type.
        /// If the file does not exist or an error occurs during loading, the default value for the type is returned.
        /// </returns>
        /// <typeparam name="T">The type into which to deserialize the loaded data.</typeparam>
        public T LoadFromStreamingAssets<T>(string folderName, string fileName, bool isEncrypted)
        {
            try
            {
                // Combine the StreamingAssets path, folder name, and file name to get the full file path
                string filePath = Path.Combine(GetFilePath(Application.streamingAssetsPath, folderName, fileName));

                // The new failsafe check
                if (isEncrypted && string.IsNullOrEmpty(_encryptionKey))
                {
                    Debug.LogError("[SaveSystem] Load failed! Encryption is enabled but no password was set. Call SetPassword() first, (e.g., in your GameManager's Awake()).");
                    return default(T);
                }


                if (isEncrypted)
                {
                    // If the data is encrypted, decrypt it before deserializing
                    string encryptedData = DecryptData(filePath, _encryptionKey);
                    return JsonConvert.DeserializeObject<T>(encryptedData);
                }
                else
                {
                    // If the data is not encrypted, read it directly from the file and deserialize
                    string jsonData = File.ReadAllText(filePath);
                    return JsonConvert.DeserializeObject<T>(jsonData);
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during loading and return the default value for the type
                Debug.LogError($"Error while loading file {fileName}: {ex.Message}");

                return default(T);
            }
        }

        #endregion

        #region Load Resources

        /// <summary>
        /// Loads a resource from the Resources folder, using a generic type parameter.
        /// If the resource has already been loaded, it is retrieved from the cache.
        /// </summary>
        /// <typeparam name="T">The type of the resource to load.</typeparam>
        /// <param name="path">The path to the resource within the Resources folder.</param>
        /// <returns>The loaded resource, or null if the resource could not be found.</returns>
        public T LoadResource<T>(string path) where T : UnityEngine.Object
        {
            if (_resourceCache.TryGetValue(path, out var cachedResource))
            {
                return cachedResource as T;
            }

            T resource = Resources.Load<T>(path);
            if (resource != null)
            {
                _resourceCache[path] = resource;
            }
            else
            {
                Debug.LogWarning($"Resource at {path} not found");
            }

            return resource;
        }

        /// <summary>
        /// Clears the resource cache, unloading unused assets to free up memory.
        /// </summary>
        public void ClearResourceCache()
        {
            _resourceCache.Clear();
            Resources.UnloadUnusedAssets();
        }

        /// <summary>
        /// Clears a specific resource from the cache, unloading unused assets to free up memory.
        /// </summary>
        /// <param name="path">The path to the resource within the Resources folder.</param>
        public void ClearSpecificResource(string path)
        {
            if (_resourceCache.ContainsKey(path))
            {
                _resourceCache.Remove(path);
                Resources.UnloadUnusedAssets();
            }
        }

        public Dictionary<string, UnityEngine.Object> GetResourceCache()
        {
            return _resourceCache;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Constructs the path to a folder inside the persistent data path.
        /// </summary>
        /// <param name="persistentDataPath">The path to the persistent data directory.</param>
        /// <param name="folderName">The name of the folder inside the persistent data path.</param>
        /// <returns>
        /// The full path to the folder inside the persistent data path.
        /// </returns>
        private string GetPath(string persistentDataPath, string folderName)
        {
            return persistentDataPath + "/" + folderName;
        }

        /// <summary>
        /// Constructs the full path to a file inside a folder in the persistent data path.
        /// </summary>
        /// <param name="persistentDataPath">The path to the persistent data directory.</param>
        /// <param name="folderName">The name of the folder inside the persistent data path.</param>
        /// <param name="fileName">The name of the file inside the folder.</param>
        /// <returns>
        /// The full path to the file inside the folder in the persistent data path.
        /// </returns>
        private string GetFilePath(string persistentDataPath, string folderName, string fileName)
        {
            return Path.Combine(persistentDataPath, folderName, fileName);
        }

        /// <summary>
        /// Creates a new directory at the specified path if it does not already exist.
        /// </summary>
        /// <param name="path">The path where the directory should be created.</param>
        private void CreateFolderDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        #endregion

        #region Encryption

        /// <summary>
        /// Encrypts the provided data using AES encryption and writes the encrypted data to a file.
        /// </summary>
        /// <param name="filePath">The path to the file where the encrypted data will be written.</param>
        /// <param name="password">The password used to derive the encryption key.</param>
        /// <param name="xmlData">The data to be encrypted and written to the file.</param>
        private void EncryptData(string filePath, string password, string xmlData)
        {
            byte[] xmlDataBytes = Encoding.UTF8.GetBytes(xmlData);

            using (Aes aesAlg = Aes.Create())
            {
                // 1. Generate a new random salt.
                byte[] salt = GenerateRandomSalt();

                // 2. Use the password and the salt to derive an encryption key.
                using (var pdb = new Rfc2898DeriveBytes(password, salt))
                {
                    aesAlg.Key = pdb.GetBytes(32); // 256 bits key
                    aesAlg.GenerateIV(); // Generating a random IV
                }

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(xmlDataBytes, 0, xmlDataBytes.Length);
                        csEncrypt.FlushFinalBlock();

                        byte[] encryptedDataBytes = msEncrypt.ToArray();

                        // 4. Store the salt + IV + encrypted data to file
                        File.WriteAllBytes(filePath, salt.Concat(aesAlg.IV).Concat(encryptedDataBytes).ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// Decrypts data from a file using AES decryption.
        /// </summary>
        /// <param name="filePath">The path to the file containing the encrypted data.</param>
        /// <param name="password">The password used to derive the decryption key.</param>
        /// <returns>
        /// The decrypted data as a string.
        /// </returns>
        private string DecryptData(string filePath, string password)
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);

            // 1. Retrieve the salt and IV from the stored data
            byte[] salt = fileBytes.Take(16).ToArray();
            byte[] iv = fileBytes.Skip(16).Take(16).ToArray();
            byte[] encryptedDataBytes = fileBytes.Skip(32).ToArray();

            using (Aes aesAlg = Aes.Create())
            {
                // 2. Use the password and the retrieved salt to derive the decryption key.
                using (var pdb = new Rfc2898DeriveBytes(password, salt))
                {
                    aesAlg.Key = pdb.GetBytes(32); // 256 bits key
                    aesAlg.IV = iv;
                }

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(encryptedDataBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            string decryptedData = srDecrypt.ReadToEnd();
                            return decryptedData;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Generates a random salt of the specified length.
        /// </summary>
        /// <param name="length">The length of the salt to generate. Defaults to 16.</param>
        /// <returns>
        /// A byte array containing the generated salt.
        /// </returns>
        private static byte[] GenerateRandomSalt(int length = 16)
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] randomNumber = new byte[length];
                rng.GetBytes(randomNumber);
                return randomNumber;
            }
        }


        /// <summary>
        /// Sets the encryption key to be used for all save and load operations.
        /// This MUST be called before any encryption/decryption is attempted.
        /// </summary>
        /// <param name="password">The password/key to use.</param>
        public void SetPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                Debug.LogWarning("[SaveSystem] A null or empty password was set.");
            }
            _encryptionKey = password;
        }

        #endregion
    }
}