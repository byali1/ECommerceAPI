﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ECommerceAPI.Application.Abstractions.Storage.Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ECommerceAPI.Infrastructure.Services.Storage.Azure
{
    public class AzureStorage : BaseStorage, IAzureStorage
    {
        private readonly BlobServiceClient _blobServiceClient; //AzureStorageAccount bağlantısını sağlar.
        private BlobContainerClient _blobContainerClient; //Hedef accountdaki container üzerinde işlem yapmamızı sağlar.

        public AzureStorage(IConfiguration configuration)
        {
            _blobServiceClient = new BlobServiceClient(configuration["Storage:Azure"]);
        }
        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string containerName, IFormFileCollection files)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await _blobContainerClient.CreateIfNotExistsAsync();
            await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

            List<(string fileName, string pathOrContainerName)> entities = new();

            foreach (var file in files)
            {
                string newFileName = await RenameFileAsync(file.Name); 

                var blobClient = _blobContainerClient.GetBlobClient(newFileName);

                await blobClient.UploadAsync(file.OpenReadStream());
                entities.Add((newFileName, $"{containerName}/{newFileName}"));
            }
            return entities;

        }

        public async Task DeleteAsync(string containerName, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = _blobContainerClient.GetBlobClient(fileName);
            await blobClient.DeleteAsync();
        }

        public List<string> GetFiles(string containerName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            return _blobContainerClient.GetBlobs().Select(b => b.Name).ToList();
        }

        public bool HasFile(string containerName, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            return _blobContainerClient.GetBlobs().Any(b => b.Name == fileName);
        }
    }
}
