using hiq_coding_test.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hiq_coding_test.Services
{
    public interface IFileUploadService
    {
        bool CheckIfAcceptedFileType(IFormFile file);
        FileModel ProccessFile(IFormFile file);
        string[] ExtractWordsFromFileContent(string contentFromFile);
        string GetFileContent(IFormFile file);
        List<KeyValuePair<string, int>> GetMostUsedWords(string[] wordsFromContent);

        string TransformTextFromFile(string textFromFile, List<KeyValuePair<string, int>> mostUsedWords);
    }
}
