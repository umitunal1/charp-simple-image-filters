using FilterLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

//Todo: we can get rid of unused usings

namespace _02_BitmapPlayground
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            PopulateFilterPickerByReflection();

        }

        private void PopulateFilterPicker()
        {
            FilterPickerBox.Items.Add(new RedFilter());
            FilterPickerBox.Items.Add(new GrayFilter());
            FilterPickerBox.Items.Add(new AverageFilter());
        }

        /// <summary>
        /// Applies a filter to an image.
        /// </summary>
        /// <param name="filter">The filter to apply. Must not be null.</param>
        /// <param name="image">The image to which the filter is applied.</param>
        /// <returns>A new image with the filter applied.</returns>
        private Image ApplyFilter(IFilterImage filter, Image image)
        {
            // Sanity check input
            if (filter == null)
                throw new ArgumentNullException(nameof(filter));

            if (image == null)
                throw new ArgumentNullException(nameof(image));

            // Create a new bitmap from the existing image
            Bitmap result = new Bitmap(image);

            // Copy the pixel colors of the existing bitmap to a new 2D - color array for further processing.
            Color[,] colors = new Color[result.Width, result.Height];
            for (int x = 0; x < result.Width; x++)
            for (int y = 0; y < result.Height; y++)
                colors[x, y] = result.GetPixel(x, y);

            //Todo: we can pass the width and height to Apply method , so that no need to get the width and height of image one more time.
            // Apply the user defined filter.
            var filteredImageData = filter.Apply(colors);

            // Copy the resulting array back to the bitmap
            for (int x = 0; x < result.Width; x++)
            for (int y = 0; y < result.Height; y++)
                result.SetPixel(x, y, filteredImageData[x, y]);

            return result;
        }

        private void ApplyFilterinThread()
        {
            BeginInvoke((Action) (() =>
                    {
                        if (FilterPickerBox.SelectedItem is IFilterImage selectedFilter)
                            PictureBoxFiltered.Image = ApplyFilter(selectedFilter, PictureBoxOriginal.Image);
                    }
                ));
        }

        private void ApplyFilterButton_Click(object sender, EventArgs e)
        {
            ThreadPool.SetMinThreads(20, 1);
            ThreadPool.SetMaxThreads(20, 1);
            ThreadPool.QueueUserWorkItem(new WaitCallback(Run));
        }

        void Run(object state)
        {
            BeginInvoke((Action) (() =>
                    {
                        if (FilterPickerBox.SelectedItem is IFilterImage selectedFilter)
                            PictureBoxFiltered.Image = ApplyFilter(selectedFilter, PictureBoxOriginal.Image);
                    }
                ));
        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        private void PopulateFilterPickerByReflection()
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath);
            Assembly testAssembly =
                Assembly.LoadFile(path + "\\" + "FilterLib.dll");
            testAssembly.GetType();
            List<Type> types = new List<Type>();
            foreach (var type in testAssembly.GetTypes())
            {
                if (type.IsClass)
                    FilterPickerBox.Items.Add(Activator.CreateInstance(type));
            }
        }
    }
}