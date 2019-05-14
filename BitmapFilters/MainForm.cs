using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace BitmapFilters
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        /*
         * Обработчик кнопки открытия изображения
         */
        private void btnLoadSource_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog(); //Запустить диалоговое окно открытия изображения
            ofd.Title = "Выберите изображение"; //Изменить заголовок диалогового окна открытия изображения 
            ofd.Filter = "Bitmap Images(*.bmp)|*.bmp|Jpeg Images(*.jpg)|*.jpeg|Png Images(*.png)|*.png"; //Определить список форматов изображений, которые можно открыть

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK) //Если на диалоговом окне открытия изображения нажата кнопка "OK"
            {
                StreamReader streamReader = new StreamReader(ofd.FileName); //Открыть поток чтения для открываемого изображения
                Bitmap sourceBitmap = (Bitmap)Bitmap.FromStream(streamReader.BaseStream); //Считать изображение
                streamReader.Close(); //Закрыть поток чтения изображения

                picSource.BackgroundImage = sourceBitmap; //Отобразить исходное изображение
                SetSettings(); //Установить настройки по умолчанию
            }
        }
        /*
         * Обработчик кнопки сохранения изображения
         */
        private void btnSaveResult_Click(object sender, EventArgs e)
        {
            if (picResult.BackgroundImage != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Укажите имя и путь файла";
                sfd.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg|Bitmap Images(*.bmp)|*.bmp";

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fileExtension = Path.GetExtension(sfd.FileName).ToUpper();
                    ImageFormat imgFormat = ImageFormat.Png;

                    if (fileExtension == "BMP")
                    {
                        imgFormat = ImageFormat.Bmp;
                    }
                    else if (fileExtension == "JPG")
                    {
                        imgFormat = ImageFormat.Jpeg;
                    }
                    StreamWriter streamWriter = new StreamWriter(sfd.FileName, false);
                    picResult.BackgroundImage.Save(streamWriter.BaseStream, imgFormat);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
        }
        /*
         * Обработчик кнопки "обрабатывать результат"
         */
        private void btnSetResultAsOutput_Click(object sender, EventArgs e)
        {
            //Если присутствует результирующее изображение
            if (picResult.BackgroundImage != null)
            {
                picOutput.BackgroundImage = picResult.BackgroundImage;
            }
        }
        /*
         * Обработчик кнопки "обрабатывать цветное"
         */
        private void btnSetSourceAsOutput_Click(object sender, EventArgs e)
        {
            //Если присутствует цветное изображение
            if (picSource.BackgroundImage != null)
            {
                picOutput.BackgroundImage = picSource.BackgroundImage;
            }
        }
        /*
         * Обработчик кнопки "Сброс"
         */
        private void btnReset_Click(object sender, EventArgs e)
        {
            //Если присутствует цветное изображение
            if (picSource.BackgroundImage != null)
            {
                SetSettings();
            }
        }
        /*
         * Функция-обработчик изменения преобразования для изображения
         */
        private void OnCheckChangedEventHandler(object sender, EventArgs e)
        {
            if (picSource.BackgroundImage != null) 
            {
                SetSettings(false); //Настроить интерфейс для выбранного преобразования изображения

                Bitmap bitmap = (Bitmap)picOutput.BackgroundImage; //Получить изображение для обработки

                if (cmbEdgeDetection.SelectedIndex == 0) //Если преобразование не выбрано
                {
                    picResult.BackgroundImage = null; //Не отображать результирующее изображение
                }
                else if (cmbEdgeDetection.SelectedIndex == 1) //Если выбран модуль градиента
                {
                    picResult.BackgroundImage = bitmap.GradientModule(); //Применить его к изображению
                }
                else if (cmbEdgeDetection.SelectedIndex == 2) //Если выбран градиент Робертса
                {
                    picResult.BackgroundImage = bitmap.RobertsCrossFilter(); //Применить его к изображению
                }
                else if (cmbEdgeDetection.SelectedIndex == 3) //Если выбрано преобразование вида ∇I = |I(i,j) - I(i+1,j)| + |I(i,j) - I(i,j+1)|
                {
                    picResult.BackgroundImage = bitmap.EdgeDetectionFilter1(); //Применить его к изображению
                }
                else if (cmbEdgeDetection.SelectedIndex == 4) //Если выбрано преобразование вида ∇I = max(|I(i,j) - I(i+1,j)| , |I(i,j) - I(i,j+1)|)
                {
                    picResult.BackgroundImage = bitmap.EdgeDetectionFilter2(); //Применить его к изображению
                }
                else if (cmbEdgeDetection.SelectedIndex == 5) //Если выбрано преобразование вида ∇I = |I(i,j) - I(i+1,j+1)| + |I(i+1,j) - I(i,j+1)|
                {
                    picResult.BackgroundImage = bitmap.EdgeDetectionFilter3(); //Применить его к изображению
                }
                else if (cmbEdgeDetection.SelectedIndex == 6) //Если выбрано преобразование вида ∇I = Σ|I(i,j) - I(i+k,j+L)|
                {
                    picResult.BackgroundImage = bitmap.EdgeDetectionFilter4(); //Применить его к изображению
                }
                else if (cmbEdgeDetection.SelectedIndex == 7) //Если выбрано преобразование вида ∇I = max |I(i,j) - I(i+k,j+L)|
                {
                    picResult.BackgroundImage = bitmap.EdgeDetectionFilter5(); //Применить его к изображению
                }
                else if (cmbEdgeDetection.SelectedIndex == 8) //Если выбрана аппроксимация по оси X
                {
                    int c = 0; //Переменная для хранения значения "с"
                    double divider = 0; //Переменная для хранения делителя маски фильтра
                    bool result = Int32.TryParse(txbCValue.Text, out c); //Попытка преобразовать, введенное пользователем значение "c", в целое число
                    if (result && (c >= 1) && (c <= 10)) //Если преобразование прошло успешно и значение "c" входит в диапазон [1, 2,...,10]
                    {
                        txbCValue.BackColor = Color.White; //Устанавить белый фон в поле ввода значения "c"
                        double[,] matrix = { {  1,  0, -1,  }, 
                                             {  c,  0, -c,  }, 
                                             {  1,  0, -1,  }, }; //Определить маску для аппроксимации
                        for (int i = 0; i < dgvMask.Rows.Count; i++) //Вывести маску на экран
                        {
                            for (int j = 0; j < dgvMask.Columns.Count; j++)
                            {
                                dgvMask[i, j].Value = matrix[i, j];
                            }
                        }
                        divider = Matrix.GetMatrixElementsSum(matrix); //Определить делитель маски
                        if (divider == 0) //Если делитель навен 0
                        {
                            divider++; //Увеличить делитель на 1
                            txbSettingsDivider.Text = divider.ToString(); //Вывести делитель на экран
                        }
                        else 
                        {
                            txbSettingsDivider.Text = divider.ToString(); //Вывести делитель на экран
                        }
                        picResult.BackgroundImage = bitmap.Approximation(matrix, 1.0 / divider); //Применить аппроксимацию к изображению
                    }
                    else 
                    {
                        txbCValue.BackColor = Color.Red; //Устанавить красный фон в поле ввода значения "c"
                        picResult.BackgroundImage = null; //Не выводить результирующее изображение на экран
                        if (txbCValue.Text == "") //Если поле ввода значения "c" пустое
                        {
                            txbCValue.BackColor = Color.White; //Устанавить белый фон в поле ввода значения "c"
                        }
                    }
                }
                else if (cmbEdgeDetection.SelectedIndex == 9) //если выбрана аппроксимация по оси Y
                {
                    int c = 0; //Переменная для хранения значения "с"
                    double divider = 0; //Переменная для хранения делителя маски фильтра
                    bool result = Int32.TryParse(txbCValue.Text, out c); //Попытка преобразовать, введенное пользователем значение "c", в целое число
                    if (result && (c >= 1) && (c <= 10)) //Если преобразование прошло успешно и значение "c" входит в диапазон [1, 2,...,10]
                    {
                        txbCValue.BackColor = Color.White; //Устанавить белый фон в поле ввода значения "c"
                        double[,] matrix = { { -1, -c, -1,  }, 
                                             {  0,  0,  0,  }, 
                                             {  1,  c,  1,  }, }; //Определить маску для аппроксимации
                        for (int i = 0; i < dgvMask.Rows.Count; i++) //Вывести маску на экран
                        {
                            for (int j = 0; j < dgvMask.Columns.Count; j++)
                            {
                                dgvMask[i, j].Value = matrix[i, j];
                            }
                        }
                        divider = Matrix.GetMatrixElementsSum(matrix); //Определить делитель маски
                        if (divider == 0) //Если делитель навен 0
                        {
                            divider++; //Увеличить делитель на 1
                            txbSettingsDivider.Text = divider.ToString(); //Вывести делитель на экран
                        }
                        else
                        {
                            txbSettingsDivider.Text = divider.ToString(); //Вывести делитель на экран
                        }
                        picResult.BackgroundImage = bitmap.Approximation(matrix, 1.0 / divider); //Применить аппроксимацию к изображению
                    }
                    else
                    {
                        txbCValue.BackColor = Color.Red; //Устанавить красный фон в поле ввода значения "c"
                        picResult.BackgroundImage = null; //Не выводить результирующее изображение на экран
                        if (txbCValue.Text == "") //Если поле ввода значения "c" пустое
                        {
                            txbCValue.BackColor = Color.White; //Устанавить белый фон в поле ввода значения "c"
                        }
                    }
                }
                else if (cmbEdgeDetection.SelectedIndex == 10) //Если выбрано повышение резкости 1
                {
                    picResult.BackgroundImage = bitmap.Sharpness1(); //Применить его к изображению
                }
                else if (cmbEdgeDetection.SelectedIndex == 11) //Если выбрано повышение резкости 2
                {
                    picResult.BackgroundImage = bitmap.Sharpness2(); //Применить его к изображению
                }
                else if (cmbEdgeDetection.SelectedIndex == 12) //Если выбрано повышение резкости 3
                {
                    picResult.BackgroundImage = bitmap.Sharpness3(); //Применить его к изображению
                }
                else if (cmbEdgeDetection.SelectedIndex == 13) //Если выбрано повышение резкости 4
                {
                    picResult.BackgroundImage = bitmap.Sharpness4(); //Применить его к изображению
                }
                else if (cmbEdgeDetection.SelectedIndex == 14) //Если выбрано согласование с плоскостью в окне 2x2
                {
                    picResult.BackgroundImage = bitmap.PlaneMatchingIn2x2Window(); //Применить его к изображению
                }
                else if (cmbEdgeDetection.SelectedIndex == 15) //Если выбран оператор Превитта
                {
                    picResult.BackgroundImage = bitmap.PrewittFilter(); //Применить его к изображению
                }
                else if (cmbEdgeDetection.SelectedIndex == 16) //Если выбрана маска согласования с поверхностью второго порядка для оператора Лапласа
                {
                    picResult.BackgroundImage = bitmap.LaplacianSecondOrderPlaneMatching(); //Применить её к изображению
                }
                else if (cmbEdgeDetection.SelectedIndex == 17) //Если выбран Лапласиан Гауссиана в окне 5x5
                {
                    picResult.BackgroundImage = bitmap.Laplacian5x5OfGaussian5x5Filter1(); //Применить его к изображению
                }
                else if (cmbEdgeDetection.SelectedIndex == 18) //Если выбрана проверка точки на локальный максимум в окне 3x3
                {
                    picResult.BackgroundImage = bitmap.LocalMaximum3x3(); //Применить её к изображению
                }
                else if (cmbEdgeDetection.SelectedIndex == 19) //Если выбрана проверка точки на локальный максимум по оси X
                {
                    picResult.BackgroundImage = bitmap.LocalMaximumX(); //Применить её к изображению
                }
                else if (cmbEdgeDetection.SelectedIndex == 20) //Если выбрана проверка точки на локальный максимум по оси Y
                {
                    picResult.BackgroundImage = bitmap.LocalMaximumY(); //Применить её к изображению
                }
            }
        }
        /*
         * Функция-обработчик нажатия кнопкой мыши на исходное изображение
         */
        private void picSource_MouseClick(object sender, MouseEventArgs e)
        {
            if (picSource.BackgroundImage != null) //Если присутствует исходное изображение
            {
                //Создаём модальное окно, помещаем в него исходное изображение и изменяем заголовок этого окна
                ShowImageForm ImageForm = new ShowImageForm(picSource.BackgroundImage, "Исходное изображение");
                ImageForm.ShowDialog(this); //Открывем, созданное модальное окно
            }
        }
        /*
         * Функция-обработчик нажатия кнопкой мыши на полутоновое изображение
         */
        private void picOutput_MouseClick(object sender, MouseEventArgs e)
        {
            if (picOutput.BackgroundImage != null) //Если присутствует полутоновое изображение
            {
                //Создаём модальное окно, помещаем в него полутоновое изображение и изменяем заголовок этого окна
                ShowImageForm ImageForm = new ShowImageForm(picOutput.BackgroundImage, "Полутоновое изображение");
                ImageForm.ShowDialog(this); //Открывем, созданное модальное окно
            }
        }
        /*
         * Функция-обработчик нажатия кнопкой мыши на результирующее изображение
         */
        private void picResult_MouseClick(object sender, MouseEventArgs e)
        {
            if (picResult.BackgroundImage != null) //Если присутствует результирующее изображение
            {
                //Создаём модальное окно, помещаем в него результирующее изображение и изменяем заголовок этого окна
                ShowImageForm ImageForm = new ShowImageForm(picResult.BackgroundImage, "Результирующее изображение");
                ImageForm.ShowDialog(this); //Открывем, созданное модальное окно
            }
        }
        /*
         * Функция, используемая для настройки интерфейса
         */
        private void SetSettings(bool byDefault = true) 
        {
            if (byDefault)
            {
                cmbEdgeDetection.SelectedIndex = 0;
                grbFilterSettings.Visible = false;
                picOutput.BackgroundImage = picSource.BackgroundImage.CopyAsGrayscale();
                picResult.BackgroundImage = null;
            }
            else 
            {
                int item = cmbEdgeDetection.SelectedIndex;

                if (item == 0 || item == 1 || item == 2 || item == 3 || item == 4 || item == 5 || item == 6 || item == 7 || item == 14 || item == 15 || item == 16 || item == 17 || item == 18 || item == 19 || item == 20)
                {
                    grbFilterSettings.Visible = false;
                }
                else if (item == 8)
                {
                    labelC.Visible = true;
                    txbCValue.Visible = true;
                    labelMaxMinCValue.Visible = true;
                    grbFilterSettings.Visible = true;
                    lblSettingsMask.Visible = true;
                    dgvMask.Visible = true;
                    labelDivider.Visible = true;
                    txbSettingsDivider.Visible = true;
                    dgvMask.Rows.Clear();
                    dgvMask.Columns.Clear();
                    for (int i = 0; i < Matrix.ApproximationHorizontal.GetLength(0); i++)
                    {
                        dgvMask.Columns.Add("", "");
                        dgvMask.Rows.Add();
                        dgvMask.Rows[i].Height = dgvMask.Height / Matrix.ApproximationHorizontal.GetLength(0);
                    }
                    for (int i = 0; i < dgvMask.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvMask.Columns.Count; j++)
                        {
                            dgvMask[i, j].Value = Matrix.ApproximationHorizontal[i, j];
                        }
                    }
                    txbSettingsDivider.Text = "";
                }
                else if (item == 9)
                {
                    labelC.Visible = true;
                    txbCValue.Visible = true;
                    labelMaxMinCValue.Visible = true;
                    grbFilterSettings.Visible = true;
                    lblSettingsMask.Visible = true;
                    dgvMask.Visible = true;
                    labelDivider.Visible = true;
                    txbSettingsDivider.Visible = true;
                    dgvMask.Rows.Clear();
                    dgvMask.Columns.Clear();
                    for (int i = 0; i < Matrix.ApproximationVertical.GetLength(0); i++)
                    {
                        dgvMask.Columns.Add("", "");
                        dgvMask.Rows.Add();
                        dgvMask.Rows[i].Height = dgvMask.Height / Matrix.ApproximationVertical.GetLength(0);
                    }
                    for (int i = 0; i < dgvMask.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvMask.Columns.Count; j++)
                        {
                            dgvMask[i, j].Value = Matrix.ApproximationVertical[i, j];
                        }
                    }
                    txbSettingsDivider.Text = "";
                }
                else if (item == 10)
                {
                    labelC.Visible = false;
                    txbCValue.Visible = false;
                    labelMaxMinCValue.Visible = false;
                    grbFilterSettings.Visible = true;
                    lblSettingsMask.Visible = true;
                    dgvMask.Visible = true;
                    labelDivider.Visible = true;
                    txbSettingsDivider.Visible = true;
                    dgvMask.Rows.Clear();
                    dgvMask.Columns.Clear();
                    for (int i = 0; i < Matrix.Laplacian3x3SharpnessType1.GetLength(0); i++)
                    {
                        dgvMask.Columns.Add("", "");
                        dgvMask.Rows.Add();
                        dgvMask.Rows[i].Height = dgvMask.Height / Matrix.Laplacian3x3SharpnessType1.GetLength(0);
                    }
                    for (int i = 0; i < dgvMask.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvMask.Columns.Count; j++)
                        {
                            dgvMask[i, j].Value = Matrix.Laplacian3x3SharpnessType1[i, j];
                        }
                    }
                    txbSettingsDivider.Text = Matrix.GetMatrixElementsSum(Matrix.Laplacian3x3SharpnessType1).ToString();
                }
                else if (item == 11)
                {
                    labelC.Visible = false;
                    txbCValue.Visible = false;
                    labelMaxMinCValue.Visible = false;
                    grbFilterSettings.Visible = true;
                    lblSettingsMask.Visible = true;
                    dgvMask.Visible = true;
                    labelDivider.Visible = true;
                    txbSettingsDivider.Visible = true;
                    dgvMask.Rows.Clear();
                    dgvMask.Columns.Clear();
                    for (int i = 0; i < Matrix.Laplacian3x3SharpnessType2.GetLength(0); i++)
                    {
                        dgvMask.Columns.Add("", "");
                        dgvMask.Rows.Add();
                        dgvMask.Rows[i].Height = dgvMask.Height / Matrix.Laplacian3x3SharpnessType2.GetLength(0);
                    }
                    for (int i = 0; i < dgvMask.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvMask.Columns.Count; j++)
                        {
                            dgvMask[i, j].Value = Matrix.Laplacian3x3SharpnessType2[i, j];
                        }
                    }
                    txbSettingsDivider.Text = Matrix.GetMatrixElementsSum(Matrix.Laplacian3x3SharpnessType2).ToString();
                }
                else if (item == 12)
                {
                    labelC.Visible = false;
                    txbCValue.Visible = false;
                    labelMaxMinCValue.Visible = false;
                    grbFilterSettings.Visible = true;
                    lblSettingsMask.Visible = true;
                    dgvMask.Visible = true;
                    labelDivider.Visible = true;
                    txbSettingsDivider.Visible = true;
                    dgvMask.Rows.Clear();
                    dgvMask.Columns.Clear();

                    for (int i = 0; i < Matrix.Laplacian3x3SharpnessType3.GetLength(0); i++)
                    {
                        dgvMask.Columns.Add("", "");
                        dgvMask.Rows.Add();
                        dgvMask.Rows[i].Height = dgvMask.Height / Matrix.Laplacian3x3SharpnessType3.GetLength(0);
                    }
                    for (int i = 0; i < dgvMask.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvMask.Columns.Count; j++)
                        {
                            dgvMask[i, j].Value = Matrix.Laplacian3x3SharpnessType3[i, j];
                        }
                    }
                    txbSettingsDivider.Text = Matrix.GetMatrixElementsSum(Matrix.Laplacian3x3SharpnessType3).ToString();
                }
                else if (item == 13)
                {
                    labelC.Visible = false;
                    txbCValue.Visible = false;
                    labelMaxMinCValue.Visible = false;
                    grbFilterSettings.Visible = true;
                    lblSettingsMask.Visible = true;
                    dgvMask.Visible = true;
                    labelDivider.Visible = true;
                    txbSettingsDivider.Visible = true;
                    dgvMask.Rows.Clear();
                    dgvMask.Columns.Clear();

                    for (int i = 0; i < Matrix.Laplacian3x3SharpnessType4.GetLength(0); i++)
                    {
                        dgvMask.Columns.Add("", "");
                        dgvMask.Rows.Add();
                        dgvMask.Rows[i].Height = dgvMask.Height / Matrix.Laplacian3x3SharpnessType4.GetLength(0);
                    }
                    for (int i = 0; i < dgvMask.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvMask.Columns.Count; j++)
                        {
                            dgvMask[i, j].Value = Matrix.Laplacian3x3SharpnessType4[i, j];
                        }
                    }
                    txbSettingsDivider.Text = Matrix.GetMatrixElementsSum(Matrix.Laplacian3x3SharpnessType4).ToString();
                }
            }
        }
    }
}