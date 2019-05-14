using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BitmapFilters
{
    public partial class ShowImageForm : Form
    {
        public ShowImageForm(Image sourceImage, string formName)
        {
            InitializeComponent();
            picScale.BackgroundImage = sourceImage; //Открыть изображение
            Text = formName; //Изменить заголовок модального окна
        }
    }
}