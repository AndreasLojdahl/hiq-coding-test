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
        public bool CheckIfAcceptedFileType(IFormFile file)
        {
            if (!string.IsNullOrEmpty(file.FileName))
            {
                var allowedFileExtensions = new string[] { ".txt" };
                var fileExtension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                Console.WriteLine(fileExtension);

                return allowedFileExtensions.Contains(fileExtension);
            }
            throw new ArgumentException("No filename");
        }

        public string GetFileContent(IFormFile file)
        {
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
         
            string regexPattern = @"\b[\p{L}]+\b";

            var wordsFromContent = Regex.Matches(contentFromFile, regexPattern)
                .Cast<Match>()
                .Select(match => match.Value.ToLower()).ToArray();

            return wordsFromContent;
        }

        public bool ProccessFile(IFormFile file)
        {
            if (CheckIfAcceptedFileType(file))
            {
                var contentFromFile = GetFileContent(file);
                var wordsFromContent = ExtractWordsFromFileContent(contentFromFile);
            }

            return true;
        }









        //var wordsFromContent = Regex.Split(contentFromFile, "[.\\s\\n,]+");

        //var punctuation = wordsFromContent.Where(Char.IsPunctuation).Distinct().ToArray();
        //var words = wordsFromContent.Split().Select(x => x.Trim(punctuation));

        //foreach (string word in wordsFromContent)
        //{

        //Console.WriteLine(word);
        //}


        ////var sorted = wordsFromContent.Where(x => IsWordWithLetters(x));
        //Console.WriteLine("-------------------------------------------------------------");
        //     foreach (string word in sorted)
        //{

        //    Console.WriteLine(word);
        //}

        //Console.WriteLine(wordsFromContent.Length);
        //Console.WriteLine(sorted[0]);
        //return text;


        //var wordWithNonLetters = Regex.Replace(word, "[^a-zA-Z0-9% ._]", string.Empty);
        //return Regex.IsMatch(word, @"^[a-zA-Z]+$");

        //string pattern = @"\b[\p{L}]+\b";
        //Regex.Matches(text, pattern)
        //    .Cast<Match>()                          // Extract matches
        //    .Select(match => match.Value.ToLower()) // Change to same case
        //    .Distinct();



    }
}
