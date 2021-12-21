namespace _2021;

using System.Diagnostics;
using static Utils;


public class Day20
{
    [Fact]
    public async Task Part1()
    {
        var input = await ReadInputLines(nameof(Day20));
        var algorithm = input[0].Select(x => x == '#' ? '1' : '0').ToArray();
        var inputImage = input.Skip(2)
            .Select(line =>
                line.Select(x => x == '#' ? '1' : '0').ToArray())
            .ToArray();

        var total = EnhanceAndCount(inputImage, 2, algorithm);

        Assert.Equal(5573, total);
    }

    [Fact]
    public async Task Part2()
    {
        var input = await ReadInputLines(nameof(Day20));
        var algorithm = input[0].Select(x => x == '#' ? '1' : '0').ToArray();
        var inputImage = input.Skip(2)
            .Select(line =>
                line.Select(x => x == '#' ? '1' : '0').ToArray())
            .ToArray();

        var total = EnhanceAndCount(inputImage, 50, algorithm);
        
        Assert.Equal(20097, total);
    }

    public int EnhanceAndCount(char[][] inputImage, int times, char[] algorithm)
    {
        var imageColumns = inputImage[0].Length;
        var imageRows = inputImage.Length;

        var steps = times;
        var extendedImageColumns = imageColumns + (steps + 1) * 4;
        var extendedImageRows = imageRows + (steps + 1) * 4;
        var border = (steps + 1) * 2;

        var image = new char[extendedImageRows, extendedImageColumns];
        for (var row = 0; row < extendedImageRows; row++)
        {
            for (var column = 0; column < extendedImageColumns; column++)
            {
                if (row < border
                    || column < border
                    || row >= (imageRows + border)
                    || column >= (imageColumns + border))
                {
                    image[row, column] = '0';
                }
                else
                {
                    image[row, column] = inputImage[row - border][column - border];
                }
            }
        }

        var infinity = '0';
        for (var step = 1; step <= steps; step++)
        {
            var stepBorder = (steps - step + 1) * 2;
            var stepImageRows = imageRows + 4 * step;
            var stepImageColumns = imageColumns + 4 * step;
            var newImage = CopyImage(image);
            infinity = infinity == '0' ? algorithm[0] : algorithm[511];
            for (var row = 0; row < extendedImageRows; row++)
            {
                for (var column = 0; column < extendedImageColumns; column++)
                {
                    if (row < stepBorder
                    || column < stepBorder
                    || row >= (stepImageRows + stepBorder)
                    || column >= (stepImageColumns + stepBorder))
                    {
                        newImage[row, column] = infinity;
                    } 
                    else
                    {
                        var pixel = ComputePixel(row, column, image, algorithm);
                        newImage[row, column] = pixel;
                    }
                }
            }
            image = newImage;
            //PrintImage(image);
        }

        var total = 0;
        for (var row = 0; row < extendedImageRows; row++)
            for (var column = 0; column < extendedImageColumns; column++)
                total += image[row, column] == '1' ? 1 : 0;

        return total;
    }

    static char ComputePixel(int row, int column, char[,] image, char[] algorithm)
    {
        var binaryValue = new string(new char[] {
            image[row-1, column - 1], image[row-1, column], image[row-1, column+1],
            image[row, column - 1], image[row, column], image[row, column + 1],
            image[row+1, column-1], image[row+1, column], image[row+1, column + 1]
        });
        var value = Convert.ToUInt16(binaryValue, 2);
        return algorithm[value];
    }

    static char[,] CopyImage(char[,] image)
    {
        var rows = image.GetLength(0);
        var columns = image.GetLength(0);
        var copy = new char[rows, columns];
        for (var row = 0; row < rows; row++)
            for (var column = 0; column < columns; column++)
                copy[row, column] = image[row, column];
        return copy;
    }

    static void PrintImage(char[,] image)
    {
        Console.WriteLine();
        Console.WriteLine();
        for (var row = 0; row < image.GetLength(0); row++)
        {
            Console.WriteLine();
            for (var column = 0; column < image.GetLength(1); column++)
            {
                Console.Write(image[row, column] == '1' ? '#' : '.');
            }
        }
    }
}