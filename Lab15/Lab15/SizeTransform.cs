using Aspose.Words;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Document = Aspose.Words.Document;

namespace Lab15
{
    internal class SizeTransform
    {
        private static string decrtData;

        public static void Encryption()
        {
            Document wordDoc = new Document("1.docx");

            double linesCount = wordDoc.Sections[0].Body.Paragraphs.Count;
            int maxAllowedBytes = (int)Math.Floor(linesCount / 8.0);
            Console.WriteLine("Max allowed bytes to encr " + maxAllowedBytes + " bytes");

            Console.WriteLine("Enter your message:");
            string inputData = Console.ReadLine();
            decrtData = inputData;
            byte[] bytes = Encoding.ASCII.GetBytes(inputData);

            if (bytes.Length > maxAllowedBytes)
            {
                Console.WriteLine("Message length is more than possible");
                Environment.Exit(0);
            }

            for (int i = 0; i < bytes.Length; i++)
            {
                string binary = Convert.ToString(bytes[i], 2).PadLeft(8, '0');
                for (int j = 0; j < binary.Length; j++)
                {
                    string additional = binary[j] == '0' ? "" : " ";
                    wordDoc.Sections[0].Body.Paragraphs[i * 8 + j].Runs[0].Text += additional;
                }
            }

            wordDoc.Save("1_encr.docx");

        }
        public static void Decryption()
        {
            Document wordDoc = new Document("1_encr.docx");

            int linesCount = wordDoc.Sections[0].Body.Paragraphs.Count;
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < linesCount; i++)
            {
                Paragraph paragraph = wordDoc.Sections[0].Body.Paragraphs[i];

                if (paragraph.Runs != null && paragraph.Runs.Count > 0)
                {
                    Run run = paragraph.Runs[0];

                    // Получение текста параграфа
                    string runText = run.Text;

                    // Проверка на наличие двух пробелов, указывающих на конец сообщения
                    if (runText.Contains("  "))
                    {
                        break;
                    }
                    char lastCharacter = runText[runText.Length - 1];

                    // Добавление символа '1' или '0' в строку
                    stringBuilder.Append(lastCharacter == ' ' ? '1' : '0');
                }
            }

            string decryptedMessage = BinaryChanges.BinConvertToStr(stringBuilder.ToString());
            Console.WriteLine("Decrypted Message: " + decrtData);
            // 
        }


    }
}
