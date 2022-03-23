using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionBirthday.Contracts.AzureAi
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Limitations:
    /// Supported file formats: JPEG, PNG, BMP, PDF, and TIFF
    /// For PDF and TIFF files, up to 2000 pages(only first two pages for the free tier) are processed.
    /// The file size must be less than 50 MB (6 MB for the free tier) and dimensions at least 50 x 50 pixels and at most 10000 x 10000 pixels.
    /// </remarks>
    public interface IOcrService
    {
        Task<OcrResults> ReadAsync(Stream imageStream);
    }
}
