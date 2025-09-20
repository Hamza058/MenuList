using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace MenuList.Services
{
    public class ResizeImage
    {
        public void Resize(string path, bool ctgry)
        {
            var (width, height) = GetImageSize(path);
            double ratio = 0.00;
            if (width > height)
            {
                width = ctgry ? 64 : 300;
                while (true)
                {
                    ratio = (double)width / height;
                    if (ctgry)
                    {
                        height = Convert.ToInt32(width / ratio);
                        if (height <= 64)
                        {
                            width += 6;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        height = Convert.ToInt32(width / ratio);
                        if (height <= 120)
                        {
                            width += 30;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            else
            {

                height = ctgry ? 64 : 120;
                while (true)
                {
                    ratio = (double)height / width;
                    if (ctgry)
                    {
                        width = Convert.ToInt32(height / ratio);
                        if (width <= 64)
                        {
                            height += 6;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        width = Convert.ToInt32(height / ratio);
                        if (width <= 180)
                        {
                            height += 10;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            using (var image = Image.Load(path))
            {
                image.Mutate(x => x.Resize(width, height));
                image.Save(path, new JpegEncoder());
            }
        }

        public (int Width, int Height) GetImageSize(string imagePath)
        {
            using (var image = Image.Load(imagePath))
            {
                return (image.Width, image.Height);
            }
        }

    }
}
