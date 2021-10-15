using hiq_coding_test.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace hiq_coding_test.Services
{
    public class FileUploadService : IFileUploadService
    {

        public FileModel ProccessFile(IFormFile file)
        {
                var contentFromFile = GetFileContent(file);
                var wordsFromContent = ExtractWordsFromFileContent(contentFromFile);
                var mostUsedWord = GetMostUsedWords(wordsFromContent);
                var transformedText = TransformTextFromFile(contentFromFile, mostUsedWord);

                var fileModel = new FileModel();
                fileModel.count = mostUsedWord[0].Value;
            fileModel.text = transformedText;
                return fileModel;
        }

        public bool CheckIfAcceptedFileType(IFormFile file)
        {
            if (!string.IsNullOrEmpty(file.FileName))
            {
                var allowedFileExtensions = new string[] { ".txt" };
                var fileExtension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                Console.WriteLine(fileExtension);

                return allowedFileExtensions.Contains(fileExtension);
            }
            throw new ArgumentException("The file extension is not accepted");
        }

        public string GetFileContent(IFormFile file)
        {
            // Extracting content from file
            string contentFromFile = null;
            using (var stream = file.OpenReadStream())
            using (var reader = new StreamReader(stream))
            {
                contentFromFile = reader.ReadToEnd();
            }
            return contentFromFile;
        }

        public string[] ExtractWordsFromFileContent(string contentFromFile)
        {
            
            //Splitting string into words array. 
            string regexPattern = @"\b[\p{L}]+\b";
            var wordsFromContent = Regex.Matches(contentFromFile, regexPattern)
                .Cast<Match>()
                .Select(match => match.Value.ToLower()).ToArray();

            return wordsFromContent;
        }

        public List<KeyValuePair<string, int>> GetMostUsedWords(string[] wordsFromContent)
        {
            // Create a dictionary and loop through string array. Adding words into dictionary and number of usages.
            Dictionary<string, int> repeatedWordCount = new Dictionary<string, int>();
            for (int i = 0; i < wordsFromContent.Length; i++)
            {
                if (repeatedWordCount.ContainsKey(wordsFromContent[i])) 
                {
                    int value = repeatedWordCount[wordsFromContent[i]];
                    repeatedWordCount[wordsFromContent[i]] = value + 1;
                }
                else
                {
                    repeatedWordCount.Add(wordsFromContent[i], 1); 
                }
            }

            //sorting Dictionary by value, by how many time same word is used and create to a list.
            var sortedRepeatedWordCount = repeatedWordCount.OrderByDescending(w => w.Value).ToList();
            // filtering out most used word or words 
            var mostUsedWords = sortedRepeatedWordCount.Where(x => x.Value == sortedRepeatedWordCount[0].Value).ToList();

            return mostUsedWords;
        }

        public string TransformTextFromFile(string textFromFile, List<KeyValuePair<string, int>> mostUsedWords)
        {   
            // loops most used words and replaces words from file with foo and bar
            mostUsedWords.ForEach(x => textFromFile = Regex.Replace(textFromFile, $"\\b{x.Key}\\b", "foo" + x.Key + "bar"));
            return textFromFile;
        }

    }
}
