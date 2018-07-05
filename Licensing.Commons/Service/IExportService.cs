using Licensing.Commons.Data;

namespace Licensing.Commons.Service
{
    public interface IExportService
    {
        /// <summary>
        /// Exports a license as xml format
        /// </summary>
        /// <param name="product"></param>
        /// <param name="license"></param>
        /// <param name="path"></param>
        string Export(Product product, License license);
    }
}