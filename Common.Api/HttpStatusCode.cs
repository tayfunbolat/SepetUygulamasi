using System;
using System.Collections.Generic;
using System.Text;


    public sealed class HttpStatusCode
    {
        /// <summary>
        /// İstek başarılı alınmış ve cevap başarılı verilmiştir.
        /// </summary>
        public const int OK = 200;

        /// <summary>
        /// İstek yapılan kaynağın (veya sayfanın) bulunamadığını belirtir. 
        /// </summary>
        public const int NotFound = 404;

        /// <summary>
        /// İstek hatalı(isteğin yapısı hatalı) olduğu belirtilir.
        /// </summary>
        public const int BadRequest = 400;

        /// <summary>
        /// İstek başarılı alınmış ancak geri içerik döndürülmemektedir.
        /// </summary>
        public const int NoContent = 204;

        /// <summary>
        /// Kaynağın yasaklandığını belirtir.
        /// </summary>
        public const int Forbidden = 403;

        public const int Found = 302;

        /// <summary>
        /// Sunucuda bir hata oluştu ve istek karşılanamadı.
        /// </summary>
        public const int InternalServerError = 500;
    }
