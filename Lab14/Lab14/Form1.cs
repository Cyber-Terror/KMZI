using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using static StegProject.ByteBitTransfer;

namespace StegProject
{

    public partial class Lab15 : Form
    {

        public int dlina = 1;
        public int CountText = 0;

        public Lab15()
        {
            InitializeComponent();
        }

       

       
        private bool CheckImageIncrypt(Bitmap scr)
        {
            byte[] rez = new byte[1];
            Color color = scr.GetPixel(0, 0);
            BitArray colorArray = TransferByteToBits(color.R); //получаем байт цвета и преобразуем в массив бит
            BitArray messageArray = TransferByteToBits(color.R); ;//инициализируем результирующий массив бит
            messageArray[0] = colorArray[0];
            messageArray[1] = colorArray[1];

            colorArray = TransferByteToBits(color.G);//получаем байт цвета и преобразуем в массив бит
            messageArray[2] = colorArray[0];
            messageArray[3] = colorArray[1];
            messageArray[4] = colorArray[2];

            colorArray = TransferByteToBits(color.B);//получаем байт цвета и преобразуем в массив бит
            messageArray[5] = colorArray[0];
            messageArray[6] = colorArray[1];
            messageArray[7] = colorArray[2];
            rez[0] = TransferBitsToBytes(messageArray); //получаем байт символа, записанного в 1 пикселе
            string m = Encoding.GetEncoding(1251).GetString(rez);
            if (m == "/")
            {
                return true;
            }
            else return false;
        }

        // Записыает количество символов для шифрования в первые биты картинки
        private void WriteCountText(int count, Bitmap src) {
            byte[] CountSymbols = Encoding.GetEncoding(1251).GetBytes(count.ToString());
            for (int i = 0; i < CountSymbols.Length; i++)
            {
                BitArray bitCount = TransferByteToBits(CountSymbols[i]); 
                Color pColor = src.GetPixel(0, i + 1); 
                BitArray bitsCurColor = TransferByteToBits(pColor.R); 
                bitsCurColor[0] = bitCount[0];
                bitsCurColor[1] = bitCount[1];
                byte nR = TransferBitsToBytes(bitsCurColor); 

                bitsCurColor = TransferByteToBits(pColor.G);
                bitsCurColor[0] = bitCount[2];
                bitsCurColor[1] = bitCount[3];
                bitsCurColor[2] = bitCount[4];
                byte nG = TransferBitsToBytes(bitsCurColor);

                bitsCurColor = TransferByteToBits(pColor.B);
                bitsCurColor[0] = bitCount[5];
                bitsCurColor[1] = bitCount[6];
                bitsCurColor[2] = bitCount[7];
                byte nB = TransferBitsToBytes(bitsCurColor);

                Color nColor = Color.FromArgb(nR, nG, nB); 
                src.SetPixel(0, i + 1, nColor); 
            }
        }

       
        private int GetTextFromImage(Bitmap src) {
            byte[] rez = new byte[3]; //массив на 3 элемента, т.е. максимум 999 символов шифруется
            for (int i = 0; i < rez.Length; i++)
            { 
                Color color = src.GetPixel(0, i + 1); //цвет 1, 2, 3 пикселей 
                BitArray colorArray = TransferByteToBits(color.R); //биты цвета
                BitArray bitCount = TransferByteToBits(color.R); ; //инициализация результирующего массива бит
                bitCount[0] = colorArray[0];
                bitCount[1] = colorArray[1];

                colorArray = TransferByteToBits(color.G);
                bitCount[2] = colorArray[0];
                bitCount[3] = colorArray[1];
                bitCount[4] = colorArray[2];

                colorArray = TransferByteToBits(color.B);
                bitCount[5] = colorArray[0];
                bitCount[6] = colorArray[1];
                bitCount[7] = colorArray[2];
                rez[i] = TransferBitsToBytes(bitCount);
            }
            string m = Encoding.GetEncoding(1251).GetString(rez);

            int res = Convert.ToInt32(m, 16);
           
            return res;
            //return Convert.ToInt32(m, 10);
        }

        // Открыть файл для шифрования
        private void write_Click(object sender, EventArgs e)
        {
            string Picture;
            string textToPicture;
            OpenFileDialog dPic = new OpenFileDialog();
            dPic.Filter = "Файлы изображений (*.bmp)|*.bmp|Все файлы (*.*)|*.*";
            if (dPic.ShowDialog() == DialogResult.OK)
            {
                Picture = dPic.FileName;
            }
            else
            {
                Picture = "";
                return;
            }

            FileStream rFile;
            try
            {
                rFile = new FileStream(Picture, FileMode.Open); //открываем поток
            }
            catch (IOException)
            {
                MessageBox.Show("Ошибка открытия файла", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Bitmap bPic = new Bitmap(rFile);



           

            OpenFileDialog dText = new OpenFileDialog();
            dText.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            if (dText.ShowDialog() == DialogResult.OK)
            {
                textToPicture = dText.FileName;
            }
            else
            {
                textToPicture = "";
                return;
            }

            using (StreamReader sr = new StreamReader(textToPicture))
            {
                string sod = sr.ReadToEnd();
                dlina = sod.Length;
            }

            FileStream rText;
            try
            {
                rText = new FileStream(textToPicture, FileMode.Open); //открываем поток
            }
            catch (IOException)
            {
                MessageBox.Show("Ошибка открытия файла", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            BinaryReader bText = new BinaryReader(rText, Encoding.ASCII);

            List<byte> bList = new List<byte>();
            while (bText.PeekChar() != -1) { //считали весь текстовый файл для шифрования в лист байт
                bList.Add(bText.ReadByte());
            }
            CountText = bList.Count; // в CountText - количество в байтах текста, который нужно закодировать
            bText.Close();
            rFile.Close();

            //проверяем, поместиться ли исходный текст в картинке
            if (CountText > (bPic.Width * bPic.Height)) {
                MessageBox.Show("Выбранная картинка мала для размещения выбранного текста");
                return;
            }

            //проверяем, может быть картинка уже зашифрована
            if (CheckImageIncrypt(bPic))
            {
                MessageBox.Show("Файл уже зашифрован");
                return;
            }

            // записываем в пиксель 0.0 признак зашифрованного файла
            byte [] Symbol = Encoding.GetEncoding(1251).GetBytes("/");
            BitArray ArrBeginSymbol = TransferByteToBits(Symbol[0]);
            Color curColor = bPic.GetPixel(0, 0);
            BitArray tempArray = TransferByteToBits(curColor.R);
            tempArray[0] = ArrBeginSymbol[0];
            tempArray[1] = ArrBeginSymbol[1];
            byte nR = TransferBitsToBytes(tempArray);

            tempArray = TransferByteToBits(curColor.G);
            tempArray[0] = ArrBeginSymbol[2];
            tempArray[1] = ArrBeginSymbol[3];
            tempArray[2] = ArrBeginSymbol[4];
            byte nG = TransferBitsToBytes(tempArray);

            tempArray = TransferByteToBits(curColor.B);
            tempArray[0] = ArrBeginSymbol[5];
            tempArray[1] = ArrBeginSymbol[6];
            tempArray[2] = ArrBeginSymbol[7];
            byte nB = TransferBitsToBytes(tempArray);

            Color nColor = Color.FromArgb(nR, nG, nB);
                      
            bPic.SetPixel(0, 0, nColor);
           
            //записываем количество символов для шифрования
            WriteCountText(CountText, bPic);


            //записываем инфу в контейнер
            int index = 0;
            bool st = false;
            for (int i = 4; i < bPic.Width; i++) { 
                for (int j = 0; j < bPic.Height; j++) {
                    Color pixelColor = bPic.GetPixel(i, j);
                    if (index == bList.Count) {
                        st = true;
                        break;
                    }
                    BitArray colorArray = TransferByteToBits(pixelColor.R);
                    BitArray messageArray = TransferByteToBits(bList[index]);
                    colorArray[0] = messageArray[0]; 
                    colorArray[1] = messageArray[1]; 
                    byte newR = TransferBitsToBytes(colorArray);

                    colorArray = TransferByteToBits(pixelColor.G);
                    colorArray[0] = messageArray[2];
                    colorArray[1] = messageArray[3];
                    colorArray[2] = messageArray[4];
                    byte newG = TransferBitsToBytes(colorArray);

                    colorArray = TransferByteToBits(pixelColor.B);
                    colorArray[0] = messageArray[5];
                    colorArray[1] = messageArray[6];
                    colorArray[2] = messageArray[7];
                    byte newB = TransferBitsToBytes(colorArray);

                    Color newColor = Color.FromArgb(newR, newG, newB);
                    bPic.SetPixel(i, j, newColor);
                    index ++;
                }
                if (st) {
                    break;
                }
            }
           

            String sFilePic;
            SaveFileDialog dSavePic = new SaveFileDialog();
            dSavePic.Filter = "Файлы изображений (*.bmp)|*.bmp|Все файлы (*.*)|*.*";
            if (dSavePic.ShowDialog() == DialogResult.OK)
            {
                sFilePic = dSavePic.FileName;
            }
            else
            {
                sFilePic = "";
                return;
            };

            FileStream savedImageFileStream;
            try
            {
                savedImageFileStream = new FileStream(sFilePic, FileMode.Create); //открываем поток на запись результатов
            }
            catch (IOException)
            {
                MessageBox.Show("Ошибка открытия файла на запись", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bPic.Save(savedImageFileStream, System.Drawing.Imaging.ImageFormat.Bmp);
            savedImageFileStream.Close(); //закрываем поток
        }


        // Открыть файл для дешифрования
        private void read_Click(object sender, EventArgs e)
        {
            string Picture;
            OpenFileDialog dPic = new OpenFileDialog();
            dPic.Filter = "Файлы изображений (*.bmp)|*.bmp|Все файлы (*.*)|*.*";
            if (dPic.ShowDialog() == DialogResult.OK)
            {
                Picture = dPic.FileName;
            }
            else
            {
                Picture = "";
                return;
            }

            FileStream rFile;
            try
            {
                rFile = new FileStream(Picture, FileMode.Open); //открываем поток
            }
            catch (IOException)
            {
                MessageBox.Show("Ошибка открытия файла", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Bitmap bPic = new Bitmap(rFile);
            if (!CheckImageIncrypt(bPic)) {
                MessageBox.Show("В файле нет зашифрованной информации", "Информация", MessageBoxButtons.OK);
                return;
            }

            
            int countSymbol = GetTextFromImage(bPic);

            

            byte[] message = new byte[countSymbol];
            int index = 0;
            bool st = false;
            for (int i = 4; i < bPic.Width; i++) {
                for (int j = 0; j < bPic.Height; j++) {
                    Color pixelColor = bPic.GetPixel(i, j);
                    if (index == message.Length) {
                        st = true;
                        break;
                    }
                    BitArray colorArray = TransferByteToBits(pixelColor.R);
                    BitArray messageArray = TransferByteToBits(pixelColor.R); ;
                    messageArray[0] = colorArray[0];
                    messageArray[1] = colorArray[1];

                    colorArray = TransferByteToBits(pixelColor.G);
                    messageArray[2] = colorArray[0];
                    messageArray[3] = colorArray[1];
                    messageArray[4] = colorArray[2];

                    colorArray = TransferByteToBits(pixelColor.B);
                    messageArray[5] = colorArray[0];
                    messageArray[6] = colorArray[1];
                    messageArray[7] = colorArray[2];
                    message[index] = TransferBitsToBytes(messageArray);
                    index++;
                }
                if (st)  break;
            }
            string strMessage = Encoding.GetEncoding(1251).GetString(message);
            //strMessage = strMessage.Substring(0, dlina*2+2);
            strMessage = strMessage.Substring(0, CountText);

            string sFileText;
            SaveFileDialog dSaveText = new SaveFileDialog();
            dSaveText.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            if (dSaveText.ShowDialog() == DialogResult.OK)
            {
                sFileText = dSaveText.FileName;
            }
            else
            {
                sFileText = "";
                return;
            };

            FileStream savedImageFileStream;
            try
            {
                savedImageFileStream = new FileStream(sFileText, FileMode.Create); 
            }
            catch (IOException)
            {
                MessageBox.Show("Ошибка открытия файла на запись", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            StreamWriter imageBitmap = new StreamWriter(savedImageFileStream, Encoding.Default);
            imageBitmap.Write(strMessage);
            MessageBox.Show("Сообщение извлечено в файл!", "Информация", MessageBoxButtons.OK);
            imageBitmap.Close();
            savedImageFileStream.Close(); 
            
        }

    }
}
