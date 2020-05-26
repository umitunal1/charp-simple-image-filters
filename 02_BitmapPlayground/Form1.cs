using _02_BitmapPlayground.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _02_BitmapPlayground
{
    public partial class Form1 : Form
    {      
        public Form1()
        {
            InitializeComponent();

            PopulateFilterPicker();
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
        private Image ApplyFilter(IFilter filter, Image image)
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
            BeginInvoke((Action)(() =>
                   {
                       if (FilterPickerBox.SelectedItem is IFilter selectedFilter)
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
            BeginInvoke((Action)(() =>
                    {
                        //todo null control
                        if (FilterPickerBox.SelectedItem is IFilter selectedFilter)
                            PictureBoxFiltered.Image = ApplyFilter(selectedFilter, PictureBoxOriginal.Image);
                    }
                ));

        }
    }
}
