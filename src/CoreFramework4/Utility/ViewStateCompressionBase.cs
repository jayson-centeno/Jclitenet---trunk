using System;
using System.IO;
using System.Web.UI;

namespace CoreFramework4
{
    public class ViewStateCompressionBase : Page
    {
        private bool _enableCompression;
        public ViewStateCompressionBase(bool enableCompression = true)
        {
            _enableCompression = enableCompression;
        }

        //protected override object LoadPageStateFromPersistenceMedium()
        //{
        //    string viewState = Request.Form["__VSTATE"];
        //    if (!_enableCompression) return viewState;
        //    byte[] bytes = Convert.FromBase64String(viewState);
        //    var formatter = new LosFormatter();
        //    bytes = Compressor.Decompress(bytes);
        //    return formatter.Deserialize(Convert.ToBase64String(bytes));
        //}

        //protected override void SavePageStateToPersistenceMedium(object viewState)
        //{
        //    if (!_enableCompression) return;

        //    var formatter = new LosFormatter();
        //    var writer = new StringWriter();
        //    formatter.Serialize(writer, viewState);
        //    string viewStateString = writer.ToString();
        //    byte[] originalState = Convert.FromBase64String(viewStateString);
        //    byte[] compressed = Compressor.Compress(originalState);
        //    ClientScript.RegisterHiddenField("__VSTATE",
        //                    compressed.Length < originalState.Length ? Convert.ToBase64String(compressed) : Convert.ToBase64String(originalState));
        //}

        protected override object LoadPageStateFromPersistenceMedium()
        {
            string viewState = Request.Form["__VSTATE"];
            if (!_enableCompression) return viewState;

            byte[] bytes = Convert.FromBase64String(viewState);
            byte header = bytes[0]; //Extract data of the first byte. This data tells whether ViewState has been compressed or not previously

            var tmp = new byte[bytes.Length - 1];
            System.Buffer.BlockCopy(bytes, 1, tmp, 0, bytes.Length - 1);

            bytes = tmp;

            if (header == 1) bytes = Compressor.Decompress(bytes);

            var formatter = new LosFormatter();
            return formatter.Deserialize(Convert.ToBase64String(bytes));
        }

        protected override void SavePageStateToPersistenceMedium(object viewState)
        {
            var formatter = new LosFormatter();
            var writer = new StringWriter();
            formatter.Serialize(writer, viewState);
            string viewStateString = writer.ToString();
            byte[] originalState = Convert.FromBase64String(viewStateString);
            if (_enableCompression)
            {
                byte[] tmp = Compressor.Compress(originalState);
                //If size is really smaller than the original one after compression, the compressed array is preferable
                if (tmp.Length < originalState.Length)
                {
                    var tmp2 = new byte[tmp.Length + 1];
                    System.Buffer.BlockCopy(tmp, 0, tmp2, 1, tmp.Length);
                    tmp2[0] = 1; //Compressed. Save into the first byte
                    originalState = tmp2;
                }
                else
                {
                    var tmp2 = originalState;
                    var tmp3 = new byte[originalState.Length + 1];
                    System.Buffer.BlockCopy(tmp2, 0, tmp3, 1, tmp2.Length);
                    tmp3[0] = 0; //Not Compressed. Save into the first byte

                    originalState = tmp3;
                }
            }
            ClientScript.RegisterHiddenField("__VSTATE", Convert.ToBase64String(originalState));
        }

    }
}
