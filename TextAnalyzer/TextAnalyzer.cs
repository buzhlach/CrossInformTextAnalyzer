using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossInformTextAnalyzer
{
    /// <summary>
    /// Анализирует полученный текст.
    /// </summary>
    public class TextAnalyzer
    {
        /// <summary>
        /// Анализируемый текст.
        /// </summary>
        private string _text;

        /// <summary>
        /// Конструктор с параметром.
        /// </summary>
        /// <param name="text">Анализируемый текст.</param>
        /// <exception cref="ArgumentNullException">Анализируемый текст null.</exception>
        public TextAnalyzer(string text)
        {
            if (text is null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            _text = text;
        }

        public TextAnalyzer()
        {
            _text = "";
        }

        /// <summary>
        /// Записывает в поле анализируемого текста текст из файла.
        /// </summary>
        /// <param name="filePath">Путь к файлу.</param>
        /// <exception cref="ArgumentNullException">Путь к файлу null.</exception>
        /// <exception cref="ArgumentException">Путь к файлу указывает на несуществующий файл.</exception>
        public void SetTextFromFile(string filePath)
        {
            if (String.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new ArgumentException("Файл не существует", nameof(filePath));
            }
            _text =File.ReadAllText(filePath);
        }

        /// <summary>
        /// Возвращает substringsCount самых часто встречаемых подстрок слов с частотой их встречи.
        /// </summary>
        /// <param name="substringLength">Размер подстрок слов.</param>
        /// <param name="substringsCount">Количество возвращаемых строк.</param>
        /// <returns>Набор пар, в которых подстроки - ключи, а частота встречи - значения.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Параметры функции принимают значение меньше 1.</exception>
        public IEnumerable<KeyValuePair<string,int>> GetTheMostCommonWordsSubstrings(int substringLength, int substringsCount)
        {
            if (substringLength < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(substringLength),"Длина подстроки должна быть не менее 1.");
            }

            if (substringsCount < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(substringsCount), "Количество подстрок должно быть не менее 1.");
            }

            var substrings = new ConcurrentDictionary<string, int>();

            Parallel.For(0, _text.Length-substringLength+1,firstIndex =>
            {
                var lastIndex = firstIndex+substringLength-1;

                for(var i = firstIndex; i <= lastIndex; i++)
                {
                    if (!Char.IsLetter(_text[i]))
                        return;
                }

                var substring = _text.Substring(firstIndex, substringLength);

                substrings.AddOrUpdate(substring, 1, (_, metCount) => metCount + 1);
            });

            var result = substrings
                .OrderByDescending(substring => substring.Value)
                .Take(substringsCount);

            return result;
        }

        /// <summary>
        /// Возвращает substringsCount самых часто встречаемых подстрок слов с частотой их встречи на тексте из файла.
        /// </summary>
        /// <param name="filePath">Путь к файлу.</param>
        /// <param name="substringLength">Размер подстрок слов.</param>
        /// <param name="substringsCount">Количество возвращаемых строк.</param>
        /// <returns>Набор пар, в которых подстроки - ключи, а частота встречи - значения.</returns>
        public IEnumerable<KeyValuePair<string, int>> GetTheMostCommonWordsSubstringsFromFile(string filePath,int substringLength, int substringsCount)
        {
            SetTextFromFile(filePath);
            return GetTheMostCommonWordsSubstrings(substringLength, substringsCount);
        }
    }
}
